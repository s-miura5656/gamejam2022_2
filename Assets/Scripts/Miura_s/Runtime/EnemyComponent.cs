using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace GameJam.Miura
{
    using AnimationState = GameJam.Miura.EnemyAnimation.AnimationState;

    public class EnemyComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private EnemySettings enemySettings;

        private EnemyAnimation enemyAnimation;

        private EnemyAttack enemyAttack;

        private CompositeDisposable disposables;

        public BoolReactiveProperty IsDeadRp { get; private set; }

        public bool IsDead { get => IsDeadRp.Value; set => IsDeadRp.Value = value; }

        private void Start()
        {
            enemyAnimation = new EnemyAnimation(animator);

            enemyAttack = new EnemyAttack(enemySettings);

            disposables = new CompositeDisposable();

            IsDeadRp = new BoolReactiveProperty(false);

            enemyAttack.OnAttacked
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

            // Enemy ‚ðÁ‚·‚Æ‚«‚ÉŽg—p
            enemyAnimation.OnDead
                .Subscribe(_ =>
                {
                })
                .AddTo(disposables);
        }

        private void Update()
        {
            CreateAttackEffect();
        }

        private void CreateAttackEffect()
        {
            if (enemyAttack.IsAttack == false)
            {
                return;
            }

            var forward = transform.forward * 3f;
            var up = transform.up * 1.1f;

            var position = transform.position + forward + up;

            var dir = player.transform.position - position;
            var rotation = Quaternion.LookRotation(dir, Vector3.up);

            var effect = Instantiate(enemySettings.RangeAttackEffect, position, rotation);

            Destroy(effect, 2f);

            enemyAttack.IsAttack = false;
        }
    }

    public class EnemyAnimation
    {
        private CompositeDisposable disposables;

        private float[] endTime = new float[(int)AnimationState.End];

        private Subject<Unit> attackMotionEnd;

        private Subject<Unit> deadMotionEnd;

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
                })
                .AddTo(disposables);

            deadMotionEnd
                .Delay(TimeSpan.FromSeconds(endTime[(int)AnimationState.Dead]))
                .Subscribe(_ =>
                {
                    OnDead.OnNext(Unit.Default);
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

    public class EnemyMove
    {
        public EnemyMove(EnemySettings enemySettings)
        {
        }
    }
}