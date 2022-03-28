using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject wizardBulletObject = null;

    private BulletPool[] bulletPool = new BulletPool[(int)BulletType.RANGE];

    private Dictionary<BulletType, BaseBullet> allBullets = new ();

    /// <summary>
    /// 弾の生成処理
    /// </summary>
    /// <param name="setpos"></param>
    public void CreateBullet(BulletType type, Vector3 setpos)
    {
        var pool = bulletPool[(int)type].Rent();
        pool.BulletPool = bulletPool[(int)type];
        switch (type)
        {
            case BulletType.WIZARD_TYPE:
                WizardBullet playerBaseBullet = (WizardBullet)pool;
                playerBaseBullet.playerpos = setpos;
                playerBaseBullet.transform.position = new Vector3(setpos.x, setpos.y + 2.0f, setpos.z);
                allBullets.Add(type, playerBaseBullet);
                return;
            default:
                return;
        }
    }
}
