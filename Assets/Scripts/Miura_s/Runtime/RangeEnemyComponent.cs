using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace GameJam.Miura
{
    using AnimationState = EnemyAnimation.AnimationState;

    public class RangeEnemyComponent : EnemyComponent
    {
        private ReactiveProperty<GameObject> effectRp;

        private Subject<Unit> OnEffectDeatroyed;

        public RangeEnemyComponent()
            : base()
        {
            effectRp = new ReactiveProperty<GameObject>();
            OnEffectDeatroyed = new Subject<Unit>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            effectRp.Value = null;
        }

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();

            attackComponent.IsAttackRp
                .Where(value => value)
                .Subscribe(value =>
                {
                    CreateAttackEffect();
                })
                .AddTo(disposables);

            effectRp
                .Delay(TimeSpan.FromSeconds(2f))
                .Where(obj => obj is object)
                .Subscribe(obj =>
                {
                    OnEffectDeatroyed.OnNext(Unit.Default);
                })
                .AddTo(disposables);

            OnEffectDeatroyed
                .Subscribe(_ =>
                {
                    Destroy(effectRp.Value);
                    attackComponent.IsAttackRp.Value = false;
                })
                .AddTo(disposables);
        }

        protected override void CreateAttackEffect()
        {
            var forward = transform.forward * 3f;
            var up = transform.up * 1.1f;

            var position = transform.position + forward + up;

            var targetPos = Target.transform.position + new Vector3(0, Target.transform.localScale.y, 0);
            var dir = targetPos - position;
            var rotation = Quaternion.LookRotation(dir, Vector3.up);

            effectRp.Value = Instantiate(enemySettings.RangeAttackEffect, position, rotation);
            effectRp.Value.tag = "Bullet";
        }

        protected override void OnMove()
        {
            if (animationComponent.State.HasFlag(AnimationState.Dead))
            {
                return;
            }

            if (animationComponent.State.HasFlag(AnimationState.Move))
            {
                transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySettings.MoveSpeed);

                if (transform.position == movePosition)
                {
                    animationComponent.State = AnimationState.Idle;

                    moveEnd.OnNext(Unit.Default);
                }
            }
        }
    }
}