using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GameJam.Miura
{
    public class EnemySettings : ScriptableObject
    {
        [SerializeField]
        [Header("デバッグ用ログの表示")]
        private bool debugMode;

        [SerializeField]
        [Header("標的のオブジェクト")]
        private List<GameObject> targets;

        [SerializeField]
        [Header("遠距離の敵のプレハブ")]
        private GameObject rangePrefab;

        [SerializeField]
        [Header("遠距離の攻撃間隔")]
        private float rangeAttackInterval;

        [SerializeField]
        [Header("遠距離の攻撃のエフェクト")]
        private GameObject rangeAttackEffect;

        [SerializeField]
        [Header("遠距離タイプの移動速度")]
        private float moveSpeed;

        [SerializeField]
        [Header("標的との最小距離")]
        private float targetMinDistance;

        [SerializeField]
        [Header("生成地点（スポーン位置）")]
        private Vector3 createPosition;

        public bool DebugMode => debugMode;

        /// <summary>
        /// 攻撃対象
        /// </summary>
        public List<GameObject> TargetObjects => targets;

        /// <summary>
        /// 遠距離の敵
        /// </summary>
        public GameObject RangePrefab => rangePrefab;

        /// <summary>
        /// 遠距離の攻撃間隔
        /// </summary>
        public float RangeAttackInterval => rangeAttackInterval;

        /// <summary>
        /// 遠距離攻撃のエフェクト
        /// </summary>
        public GameObject RangeAttackEffect => rangeAttackEffect;

        /// <summary>
        /// 遠距離の移動速度
        /// </summary>
        public float MoveSpeed => moveSpeed;

        /// <summary>
        /// 標的との最小距離
        /// </summary>
        public float TargetMinDistance => targetMinDistance;

        /// <summary>
        /// 生成位置
        /// </summary>
        public Vector3 CreatePosition => createPosition;

        [MenuItem("GameJam/Create/EnemySettings")]
        public static void CreateAsset()
        {
            var asset = CreateInstance<EnemySettings>();

            AssetDatabase.CreateAsset(asset, "Assets/Scripts/Miura_s/EnemySettings.asset");

            AssetDatabase.SaveAssets();

            AssetDatabase.Refresh();
        }
    }
}