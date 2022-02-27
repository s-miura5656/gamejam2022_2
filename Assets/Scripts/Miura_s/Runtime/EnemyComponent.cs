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
        protected Animator animator;

        [SerializeField]
        protected EnemySettings enemySettings;

        [SerializeField]
        protected MapSettings mapSettings;

        protected EnemyAnimation animationComponent;

        protected EnemyAttack attackComponent;

        protected CompositeDisposable disposables;

        protected Vector3 movePosition;

        protected int targetIndex;

        protected Subject<Unit> moveEnd;

        public EnemyComponent()
        {
            disposables = new CompositeDisposable();
            IsDeadRp = new BoolReactiveProperty(false);
            IsRemove = false;
            moveEnd = new Subject<Unit>();
            TargetObjects = new List<GameObject>();
        }

        public BoolReactiveProperty IsDeadRp { get; private set; }

        public bool IsDead { get => IsDeadRp.Value; set => IsDeadRp.Value = value; }

        public bool IsRemove { get; private set; }

        public List<GameObject> TargetObjects { get; internal set; }

        public GameObject Target => TargetObjects[targetIndex];

        public EnemySettings EnemySettings => enemySettings;

        public void MoveStart()
        {
            animationComponent.State = AnimationState.Move;
        }

        protected virtual void Update()
        {
            OnMove();
        }

        protected virtual void OnEnable()
        {
            animationComponent = new EnemyAnimation(animator);
            attackComponent = new EnemyAttack(enemySettings);
            transform.position = enemySettings.CreatePosition;
            IsDead = false;
            IsRemove = false;

            RegisterCallbacks();
        }

        protected virtual void OnDisable()
        {
            UnregisterCallbacks();

            animationComponent = null;
            attackComponent = null;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("WizardAttack"))
            {
                IsDead = true;
            }
        }

        protected virtual void RegisterCallbacks()
        {
            attackComponent.OnAttacked
                .Subscribe(_ =>
                {
                    animationComponent.State = AnimationState.Attack;
                })
                .AddTo(disposables);

            IsDeadRp
                .Where(value => value == true)
                .Subscribe(value =>
                {
                    animationComponent.State = AnimationState.Dead;
                })
                .AddTo(disposables);

            animationComponent.StateRp
                .Where(state => state.HasFlag(AnimationState.Move))
                .Subscribe(state =>
                {
                    var randomObj = new System.Random();

                    movePosition = CalculationGoalPos(randomObj);

                    var dir = movePosition - transform.position;
                    var rotation = Quaternion.LookRotation(dir, Vector3.up);

                    transform.rotation = rotation;

                    targetIndex = randomObj.Next(0, TargetObjects.Count);
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

            animationComponent.OnDead
                .Subscribe(_ =>
                {
                    IsRemove = true;
                })
                .AddTo(disposables);
        }

        protected virtual void UnregisterCallbacks()
        {
            disposables.Clear();
        }

        protected virtual void CreateAttackEffect()
        {
        }

        protected virtual void OnMove()
        {
        }

        protected Vector3 CalculationGoalPos(System.Random randomObj)
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
            StateRp = new ReactiveProperty<AnimationState>(AnimationState.Idle);

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
                            StateRp.Dispose();
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
                })
                .AddTo(disposables);

            moveMotionStart
                .Delay(TimeSpan.FromSeconds(1f))
                .Where(_ => State != AnimationState.Dead)
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

            IsAttackRp = new BoolReactiveProperty(false);

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
                    IsAttackRp.Value = true;
                })
                .AddTo(disposables);
        }

        public Subject<Unit> OnAttacked { get; }

        public BoolReactiveProperty IsAttackRp { get; set; }
    }
}