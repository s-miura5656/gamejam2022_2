using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class EnemySettings : ScriptableObject
{
    [SerializeField]
    [Header("�������̍U���Ԋu")]
    private float rangeAttackInterval;

    [SerializeField]
    [Header("�������̍U���̃G�t�F�N�g")]
    private GameObject rangeAttackEffect;

    /// <summary>
    /// �������̍U���Ԋu
    /// </summary>
    public float RangeAttackInterval => rangeAttackInterval;

    /// <summary>
    /// �������U���̃G�t�F�N�g
    /// </summary>
    public GameObject RangeAttackEffect => rangeAttackEffect;

    [MenuItem("GameJam/Create/EnemySettings")]
    public static void CreateAsset()
    {
        var asset = CreateInstance<EnemySettings>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/Miura_s/EnemySettings.asset");

        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();
    }
}
