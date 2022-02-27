using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Toolkit;

//�v���C���[�̒e��pool�N���X
public class PlayerBulletPool : ObjectPool<PlayerBaseBullet>
{
    public GameObject bulletPrefab;

    protected override PlayerBaseBullet CreateInstance()
    {
        return GameObject.Instantiate(bulletPrefab).GetComponent<PlayerBaseBullet>();
    }
}
