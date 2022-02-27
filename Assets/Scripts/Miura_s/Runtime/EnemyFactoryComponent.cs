using System.Collections;
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

        public List<EnemyComponent> Enemys { get; private set; }

        public ReactiveProperty<int> DownEnemyCount { get; private set; }

        private void Awake()
        {
            enemyFactory = new EnemyFactory(enemySettings);
            Enemys = new List<EnemyComponent>();
            DownEnemyCount = new ReactiveProperty<int>(0);
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

        public void InvokeRangeEnemy()
        {
            Enemys.Add(enemyFactory.Rent());
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