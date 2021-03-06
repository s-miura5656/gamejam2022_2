using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace GameJam.Miura
{
    using AnimationState = EnemyAnimation.AnimationState;

    public class EnemyComponent : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private EnemySettings enemySettings;

        [SerializeField]
        private MapSettings mapSettings;

        private EnemyAnimation enemyAnimation;

        private EnemyAttack enemyAttack;

        private CompositeDisposable disposables;

        private Vector3 goalPosition;

        private int targetIndex;

        private Subject<Unit> moveEnd;

        public BoolReactiveProperty IsDeadRp { get; private set; }

        public bool IsDead { get => IsDeadRp.Value; set => IsDeadRp.Value = value; }

        public bool IsRemove { get; private set; }

        public GameObject Target => enemySettings.TargetObjects[targetIndex];

        public EnemySettings EnemySettings => enemySettings;

        private void Awake()
        {
            disposables = new CompositeDisposable();
            IsDeadRp = new BoolReactiveProperty(false);
            IsRemove = false;
            moveEnd = new Subject<Unit>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                IsDead = true;
            }

            OnMove();
            CreateAttackEffect();
        }

        private void OnEnable()
        {
            enemyAnimation = new EnemyAnimation(animator);
            enemyAttack = new EnemyAttack(enemySettings);
            transform.position = enemySettings.CreatePosition;
            IsDead = false;
            IsRemove = false;
            RegisterCallbacks();
        }

        private void OnDisable()
        {
            UnregisterCallbacks();

            enemyAnimation = null;
            enemyAttack = null;
        }

        private void RegisterCallbacks()
        {
            enemyAttack.OnAttacked
                .Where(_ => enemyAnimation.State != AnimationState.Dead)
                .Subscribe(_ =>
                {
                    enemyAnimation.State = AnimationState.Attack;
                })
                .AddTo(disposables);

            IsDeadRp
                .Where(value => value == true)
                .Subscribe(value =>
                {
                    enemyAnimation.State = AnimationState.Dead;
                })
                .AddTo(disposables);

            enemyAnimation.StateRp
                .Where(state => state.HasFlag(AnimationState.Move))
                .Subscribe(state =>
                {
                    var randomObj = new System.Random();

                    goalPosition = CalculationGoalPos(randomObj);

                    var dir = goalPosition - transform.position;
                    var rotation = Quaternion.LookRotation(dir, Vector3.up);

                    transform.rotation = rotation;

                    targetIndex = randomObj.Next(0, enemySettings.TargetObjects.Count);
                })
                .AddTo(disposables);

            moveEnd
                .Subscribe(_ =>
                {
                    var dir = Target.transform.position - transform.position;
                    var rotation = Quaternion.LookRotation(dir, Vector3.up);

                    transform.rotation = rotation;
                })
                .AddTo(disposables);

            enemyAnimation.OnDead
                .Subscribe(_ =>
                {
                    IsRemove = true;
                })
                .AddTo(disposables);
        }

        private void UnregisterCallbacks()
        {
            disposables.Clear();
        }

        private void CreateAttackEffect()
        {
            if (enemyAnimation.State.HasFlag(AnimationState.Dead))
            {
                return;
            }

            if (enemyAttack.IsAttack == false)
            {
                return;
            }

            var forward = transform.forward * 3f;
            var up = transform.up * 1.1f;

            var position = transform.position + forward + up;

            var targetPos = Target.transform.position + new Vector3(0, Target.transform.localScale.y, 0);
            var dir = targetPos - position;
            var rotation = Quaternion.LookRotation(dir, Vector3.up);

            var effect = Instantiate(enemySettings.RangeAttackEffect, position, rotation);

            Destroy(effect, 2f);

            enemyAttack.IsAttack = false;
        }

        private void OnMove()
        {
            if (enemyAnimation.State.HasFlag(AnimationState.Dead))
            {
                return;
            }

            if (enemyAnimation.State.HasFlag(AnimationState.Move))
            {
                float distance = (transform.position - Target.transform.position).sqrMagnitude;

                var minDist = enemySettings.TargetMinDistance * 10;

                transform.position = Vector3.MoveTowards(transform.position, goalPosition, enemySettings.MoveSpeed);

#if UNITY_EDITOR
                if (enemySettings.DebugMode)
                {
                    Debug.Log($"?W?I{Target.name}????????({distance * 0.1f})");
                }
#endif

                if (transform.position == goalPosition)
                {
                    enemyAnimation.State = AnimationState.Idle;

                    moveEnd.OnNext(Unit.Default);
                }
            }
        }

        private Vector3 CalculationGoalPos(System.Random randomObj)
        {
            var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            float x = randomObj.Next((int)mapSettings.OriginPosition.x, (int)mapSettings.EndPosition.x);
            x = (float)x + (float)randomObj.NextDouble();

            pos.x = Mathf.Clamp(x, mapSettings.OriginPosition.x, mapSettings.EndPosition.x);

            float z = randomObj.Next((int)mapSettings.OriginPosition.z, (int)mapSettings.EndPosition.z);
            z = (float)z + (float)randomObj.NextDouble();

            pos.z = Mathf.Clamp(z, mapSettings.OriginPosition.z, mapSettings.EndPosition.z);

            float distance = (pos - Target.transform.position).sqrMagnitude;

            var minDist = enemySettings.TargetMinDistance * 10;

            if (distance < minDist)
            {
                return CalculationGoalPos(randomObj);
            }

            return pos;
        }
    }

    public class EnemyAnimation
    {
        private CompositeDisposable disposables;

        private float[] endTime = new float[(int)AnimationState.End];

        private Subject<Unit> attackMotionEnd;

        private Subject<Unit> deadMotionEnd;

        private Subject<Unit> moveMotionStart;

        public enum AnimationState : byte
        {
            Idle,
            Attack,
            Move,
            Dead,
            End,
        }

        public ReactiveProperty<AnimationState> StateRp { get; }

        public AnimationState State { get => StateRp.Value; set => StateRp.Value = value; }

        public Subject<Unit> OnDead { get; }

        public EnemyAnimation(Animator animator)
        {
            StateRp = new ReactiveProperty<AnimationState>(AnimationState.Move);

            attackMotionEnd = new Subject<Unit>();

            deadMotionEnd = new Subject<Unit>();

            moveMotionStart = new Subject<Unit>();

            OnDead = new Subject<Unit>();

            disposables = new CompositeDisposable();

            var sw = new System.Diagnostics.Stopwatch();

            for (AnimationState i = 0; i < AnimationState.End; ++i)
            {
                endTime[(int)i] = animator.runtimeAnimatorController.animationClips[(int)i].length;
            }

            StateRp
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case AnimationState.Idle:
                            animator.Play("Idle");
                            break;
                        case AnimationState.Move:
                            animator.Play("Move");
                            break;
                        case AnimationState.Attack:
                            animator.Play("Attack");
                            attackMotionEnd.OnNext(Unit.Default);
                            break;
                        case AnimationState.Dead:
                            animator.Play("Dead");
                            deadMotionEnd.OnNext(Unit.Default);
                            break;
                        default:
                            break;
                    }
                })
                .AddTo(disposables);

            attackMotionEnd
                .Delay(TimeSpan.FromSeconds(endTime[(int)AnimationState.Attack]))
                .Subscribe(_ =>
                {
                    State = AnimationState.Idle;
                    moveMotionStart.OnNext(Unit.Default);
                })
                .AddTo(disposables);

            deadMotionEnd
                .Delay(TimeSpan.FromSeconds(endTime[(int)AnimationState.Dead]))
                .Subscribe(_ =>
                {
                    OnDead.OnNext(Unit.Default);
                    StateRp.Dispose();
                })
                .AddTo(disposables);

            moveMotionStart
                .Delay(TimeSpan.FromSeconds(1f))
                .Subscribe(_ =>
                {
                    State = AnimationState.Move;
                })
                .AddTo(disposables);
        }
    }

    public class EnemyAttack
    {
        private CompositeDisposable disposables;

        public EnemyAttack(EnemySettings enemySettings)
        {
            disposables = new CompositeDisposable();

            OnAttacked = new Subject<Unit>();

            IsAttack = false;

            Observable
                .Interval(TimeSpan.FromSeconds(enemySettings.RangeAttackInterval))
                .Subscribe(_ =>
                {
                    OnAttacked.OnNext(Unit.Default);
                })
                .AddTo(disposables);

            OnAttacked
                .Delay(TimeSpan.FromSeconds(0.8f))
                .Subscribe(_ =>
                {
                    IsAttack = true;
                })
                .AddTo(disposables);
        }

        public Subject<Unit> OnAttacked { get; }

        public bool IsAttack { get; set; }
    }
}