using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBullet : BaseBullet
{
    public GameObject[] Targets { get; set; }

    public Vector3 playerpos { get; set; }

    public bool isdestroy { get; set; }

    public WizardBullet()
    {
        BulletType = BulletType.WIZARD_TYPE;
        isdestroy = false;
    }

    /// <summary>
    /// 近くの標的を検索
    /// </summary>
    /// <param name="hostpos"></param>
    /// <returns></returns>
    protected GameObject BulletNearTargget(Vector3 hostpos)
    {
        Targets = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject movetarget = null;

        for (int i = 0; i < Targets.Length; i++)
        {
            if (i == 0)
            {
                movetarget = Targets[i];
            }
            float targetdistance = Vector3.Distance(hostpos, Targets[i].transform.position);
            float movetargetdistance = Vector3.Distance(hostpos, movetarget.transform.position);
            if (targetdistance <= movetargetdistance)
            {
                movetarget = Targets[i];
            }
        }
        return movetarget;
    }

    public void Update()
    {
        var target = BulletNearTargget(playerpos).transform.position;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, transform.position.y, target.z), 0.1f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            BulletPool.Return(this);
        }
    }
}
