using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardContllor : MonoBehaviour
{
    [SerializeField]
    PlayerBulletManager playerBulletManager;

    //一定時間ごとに弾を撃つ秒数
    [SerializeField]
    public float timeTrigger = 5.0f;
    void Update()
    {
        if(Time.time > timeTrigger) {

            playerBulletManager.CreatePlayerBullet(this.transform.position);
            timeTrigger = Time.time + timeTrigger;
        }
    }
}
