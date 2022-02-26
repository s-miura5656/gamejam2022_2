using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class EnemySettings : ScriptableObject
{
    [SerializeField]
    [Header("�f�o�b�O�p���O�̕\��")]
    private bool debugMode;

    [SerializeField]
    [Header("�W�I�̃I�u�W�F�N�g")]
    private List<GameObject> targets;

    [SerializeField]
    [Header("�������̍U���Ԋu")]
    private float rangeAttackInterval;

    [SerializeField]
    [Header("�������̍U���̃G�t�F�N�g")]
    private GameObject rangeAttackEffect;

    [SerializeField]
    [Header("�������^�C�v�̈ړ����x")]
    private float moveSpeed;

    [SerializeField]
    [Header("�W�I�Ƃ̍ŏ�����")]
    private float targetMinDistance;

    public bool DebugMode => debugMode;

    /// <summary>
    /// �U���Ώ�
    /// </summary>
    public List<GameObject> TargetObjects => targets;

    /// <summary>
    /// �������̍U���Ԋu
    /// </summary>
    public float RangeAttackInterval => rangeAttackInterval;

    /// <summary>
    /// �������U���̃G�t�F�N�g
    /// </summary>
    public GameObject RangeAttackEffect => rangeAttackEffect;

    /// <summary>
    /// �������̈ړ����x
    /// </summary>
    public float MoveSpeed => moveSpeed;

    /// <summary>
    /// �W�I�Ƃ̍ŏ�����
    /// </summary>
    public float TargetMinDistance => targetMinDistance;

    [MenuItem("GameJam/Create/EnemySettings")]
    public static void CreateAsset()
    {
        var asset = CreateInstance<EnemySettings>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/Miura_s/EnemySettings.asset");

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();
    }
}
