using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardContllor : MonoBehaviour
{
    //バレットマネージャークラス
    [SerializeField]
    PlayerBulletManager playerBulletManager;
    [SerializeField]
    private Animator wizardAnimator = null;

    //一定時間ごとに弾を撃つ秒数
    [SerializeField]
    public float shotTimeTrigger = 5.0f;
    //弾の射出感覚
    [SerializeField]
    public float shotTimeOut = 5.0f;
    //ウィザードの移動速度
    [SerializeField]
    private float movePower = 0.02f;
    //ウィザードの移動制限
    [SerializeField]
    private float maxLimitPosition = 5.0f;
    [SerializeField]
    private float minLimitPosition = -5.0f;
    void Update()
    {
        if(Time.time > shotTimeTrigger) {

            playerBulletManager.CreatePlayerBullet(this.transform.position);
            shotTimeTrigger = Time.time + shotTimeOut;
            wizardAnimator.SetBool("IsAttack", true);
        }
        else
        {
            wizardAnimator.SetBool("IsAttack", false);
        }
        transform.Translate(movePower, 0.0f, 0.0f);
        if(transform.position.z >= maxLimitPosition)
        {
            movePower = -movePower;
        }
        if (transform.position.z <= minLimitPosition)
        {
            movePower = -movePower;
        }
    }
}
