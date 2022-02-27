using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの弾のクラス
public class PlayerBaseBullet : BaseBullet
{
    public GameObject[] targets { get; set; }

    //プレイヤーの位置
    public Vector3 playerpos { get; set; }
    //自分は破棄すべき化の判定
    public bool isdestroy { get; set; }
    public PlayerBaseBullet()
    {
        bulletType = BulletType.PLAYER_TYPE;
        isdestroy = false;
    }

    //ホストから近くのターゲットを探す
    protected GameObject BulletNearTargget(Vector3 hostpos)
    {
        targets = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject movetarget = null;

        for (int i = 0; i < targets.Length; i++)
        {
            if (i == 0)
            {
                movetarget = targets[i];
            }
            float targetdistance = Vector3.Distance(hostpos, targets[i].transform.position);
            float movetargetdistance = Vector3.Distance(hostpos, movetarget.transform.position);
            if (targetdistance <= movetargetdistance)
            {
                movetarget = targets[i];
            }
        }
        return movetarget;
    }

    public void Update()
    {
        if (BulletNearTargget(playerpos).transform.position == this.transform.position)
        {
            isdestroy = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, BulletNearTargget(playerpos).transform.position, 0.1f);
    }
}
