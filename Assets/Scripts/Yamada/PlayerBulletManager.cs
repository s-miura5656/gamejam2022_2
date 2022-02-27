using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̒e�̃}�l�[�W���[�N���X
public class PlayerBulletManager : MonoBehaviour
{
    //�v���C���[�̋ʂ̊i�[
    [SerializeField]
    private GameObject playerBulletObject = null;

    //�e�̃v�[��
    [SerializeField]
    private PlayerBulletPool playerBulletPool;

    //�v�[���̃��X�g
    List<PlayerBaseBullet> playerBaseBullets = new List<PlayerBaseBullet>();
    private void Start()
    {
        playerBulletPool = new PlayerBulletPool();

        playerBulletPool.bulletPrefab = playerBulletObject;
    }
    //�e�̐���
    public void CreatePlayerBullet(Vector3 setpos)
    {
        PlayerBaseBullet playerBaseBullet = playerBulletPool.Rent();
        playerBaseBullet.isdestroy = false;
        playerBaseBullet.playerpos = setpos;
        playerBaseBullet.transform.position = new Vector3(setpos.x, setpos.y + 2.0f, setpos.z);
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
