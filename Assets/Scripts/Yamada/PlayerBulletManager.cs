using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの弾のマネージャークラス
public class PlayerBulletManager : MonoBehaviour
{
    //プレイヤーの玉の格納
    [SerializeField]
    private GameObject playerBulletObject = null;

    //弾のプール
    [SerializeField]
    private PlayerBulletPool playerBulletPool;

    //プールのリスト
    List<PlayerBaseBullet> playerBaseBullets = new List<PlayerBaseBullet>();
    private void Start()
    {
        playerBulletPool = new PlayerBulletPool();

        playerBulletPool.bulletPrefab = playerBulletObject;
    }
    //弾の生成
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
