using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    NONE = 0,
    WIZARD_TYPE = 1,
    RANGE = 2
}

public class BaseBullet : MonoBehaviour
{
    public virtual BulletType BulletType { get; set; }

    public virtual BulletPool BulletPool { get; set; }

    public BaseBullet()
    {
        BulletType = BulletType.NONE;
    }
}