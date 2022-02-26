using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class EnemySettings : ScriptableObject
{
    [SerializeField]
    [Header("‰“‹——£‚ÌUŒ‚ŠÔŠu")]
    private float rangeAttackInterval;

    [SerializeField]
    [Header("‰“‹——£‚ÌUŒ‚‚ÌƒGƒtƒFƒNƒg")]
    private GameObject rangeAttackEffect;

    /// <summary>
    /// ‰“‹——£‚ÌUŒ‚ŠÔŠu
    /// </summary>
    public float RangeAttackInterval => rangeAttackInterval;

    /// <summary>
    /// ‰“‹——£UŒ‚‚ÌƒGƒtƒFƒNƒg
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
