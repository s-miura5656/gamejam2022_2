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

        public GameObject RangePrefab => rangePrefab;

        public float RangeAttackInterval => rangeAttackInterval;

        public GameObject RangeAttackEffect => rangeAttackEffect;

        public float MoveSpeed => moveSpeed;

        public float TargetMinDistance => targetMinDistance;

        public Vector3 CreatePosition => createPosition;

    }
}