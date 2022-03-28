using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatus : ScriptableObject
{
    [SerializeField, Header("最大ライフポイント")]
    private int maxLifePoint = 5;

    [SerializeField, Header("ライフポイント")]
    private int lifePoint = 5;

    [SerializeField, Header("移動速度")]
    private float moveSpeed = 50.0f;

    [SerializeField, Header("死亡判定")]
    private bool isDead = false;

    public int GetMaxLifePoint => maxLifePoint;

    public float GetMoveSpeed => moveSpeed;

    public int LifePoint
    {
        get { return lifePoint; }
        set { lifePoint = value; }
    }

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }
}