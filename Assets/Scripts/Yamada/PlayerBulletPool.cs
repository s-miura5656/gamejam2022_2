using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

public class PlayerBulletPool : ObjectPool<PlayerBaseBullet>
{
    public GameObject bulletPrefab;

    protected override PlayerBaseBullet CreateInstance()
    {
        return GameObject.Instantiate(bulletPrefab).GetComponent<PlayerBaseBullet>();
    }
}
