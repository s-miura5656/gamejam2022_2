﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾のタイプ
public enum BulletType
{
    NONE,
    PLAYER_TYPE,
}

public class BaseBullet : MonoBehaviour
{
    public virtual BulletType bulletType { get; set; }

    public BaseBullet()
    {
        bulletType = BulletType.NONE;
    }
}


