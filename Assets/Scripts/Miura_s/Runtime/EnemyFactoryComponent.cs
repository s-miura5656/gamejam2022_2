﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Toolkit;
using UnityEngine;

namespace GameJam.Miura
{
    public class EnemyFactoryComponent : MonoBehaviour
    {
        [SerializeField]
        private EnemySettings enemySettings;

        private EnemyFactory enemyFactory;

        private List<GameObject> targets;

        public List<EnemyComponent> Enemys { get; private set; }

        public ReactiveProperty<int> DownEnemyCount { get; private set; }

        public void InvokeRangeEnemy()
        {
            var enemy = enemyFactory.Rent();
            enemy.TargetObjects = targets;
            enemy.MoveStart();

            Enemys.Add(enemy);
        }

        private void Awake()
        {
            enemyFactory = new EnemyFactory(enemySettings);
            Enemys = new List<EnemyComponent>();
            DownEnemyCount = new ReactiveProperty<int>(0);
            targets = GameObject.FindGameObjectsWithTag("Player").ToList();
        }

        private void Update()
        {
            var removeEnemys = Enemys.Where(enemy => enemy.IsRemove == true);
            DownEnemyCount.Value += removeEnemys.Count();

            while (removeEnemys.Any())
            {
                enemyFactory.Return(removeEnemys.ElementAt(0));
                Enemys.Remove(removeEnemys.ElementAt(0));
            }
        }
    }

    public class EnemyFactory : ObjectPool<EnemyComponent>
    {
        private GameObject rangeEnemyPrefab;

        private EnemySettings enemySettings;

        public EnemyFactory(EnemySettings enemySettings)
        {
            this.enemySettings = enemySettings;
            rangeEnemyPrefab = enemySettings.RangePrefab;
        }

        protected override EnemyComponent CreateInstance()
        {
            return Object.Instantiate(rangeEnemyPrefab).GetComponent<EnemyComponent>();
        }
    }
}