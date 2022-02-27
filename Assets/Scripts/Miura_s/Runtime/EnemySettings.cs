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
        //[Header("�f�o�b�O�p���O�̕\��")]
        private bool debugMode;

        [SerializeField]
        //[Header("�W�I�̃I�u�W�F�N�g")]
        private List<GameObject> targets;

        [SerializeField]
        //[Header("�������̓G�̃v���n�u")]
        private GameObject rangePrefab;

        [SerializeField]
        //[Header("�������̍U���Ԋu")]
        private float rangeAttackInterval;

        [SerializeField]
        //[Header("�������̍U���̃G�t�F�N�g")]
        private GameObject rangeAttackEffect;

        [SerializeField]
        //[Header("�������^�C�v�̈ړ����x")]
        private float moveSpeed;

        [SerializeField]
        //[Header("�W�I�Ƃ̍ŏ�����")]
        private float targetMinDistance;

        [SerializeField]
        //[Header("�����n�_�i�X�|�[���ʒu�j")]
        private Vector3 createPosition;

        public bool DebugMode => debugMode;

        public List<GameObject> TargetObjects => targets;

        public GameObject RangePrefab => rangePrefab;

        public float RangeAttackInterval => rangeAttackInterval;

        public GameObject RangeAttackEffect => rangeAttackEffect;

        public float MoveSpeed => moveSpeed;

        public float TargetMinDistance => targetMinDistance;

        public Vector3 CreatePosition => createPosition;

    }
}