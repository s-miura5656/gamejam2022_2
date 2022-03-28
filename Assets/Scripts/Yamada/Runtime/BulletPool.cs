using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

public class BulletPool : ObjectPool<BaseBullet>
{
    public GameObject bulletPrefab = null;

    public BulletType bulletType = BulletType.NONE;

    protected override BaseBullet CreateInstance()
    {
        return GameObject.Instantiate(bulletPrefab).GetComponent<BaseBullet>();
    }
}
