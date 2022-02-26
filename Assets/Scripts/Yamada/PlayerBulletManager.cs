using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    //�v���C���[�̋ʂ̊i�[
    [SerializeField]
    public GameObject playerBulletObject = null;

    //�e�̃v�[��
    [SerializeField]
    private PlayerBulletPool playerBulletPool;


    List<PlayerBaseBullet> playerBaseBullets = new List<PlayerBaseBullet>();
    private void Start()
    {
        playerBulletPool = new PlayerBulletPool();

        playerBulletPool.bulletPrefab = playerBulletObject;
    }

    public void CreatePlayerBullet(Vector3 setpos)
    {
        PlayerBaseBullet playerBaseBullet = playerBulletPool.Rent();
        playerBaseBullet.playerpos = setpos;
        playerBaseBullet.transform.position = setpos;
        playerBaseBullets.Add(playerBaseBullet);
    }

    public void Update()
    {
        foreach(PlayerBaseBullet bullet in playerBaseBullets)
        {
            if (bullet.isdestroy)
            {
                playerBulletPool.Return(bullet);
            }
        }
    }
}
