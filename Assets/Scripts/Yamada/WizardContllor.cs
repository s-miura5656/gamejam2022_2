using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardContllor : MonoBehaviour
{
    //バレットマネージャークラス
    [SerializeField,Header("バレットマネージャークラス")]
    PlayerBulletManager playerBulletManager;
    [SerializeField,Header("アニメーションコントロール")]
    private Animator wizardAnimator = null;
    [SerializeField, Header("HPUI")]
    private Slider HPUI;

    [SerializeField, Header("プレイヤーの最大ライフポイント")]
    public int maxLifePoint = 5;
    [SerializeField,Header("プレイヤーのライフポイント")]
    public int lifePoint = 5;
    [SerializeField, Header("プレイヤーの死亡判定")]
    public bool isDead = false;

    //一定時間ごとに弾を撃つ秒数
    [SerializeField,Header("一定時間ごとに弾を撃つ秒数")]
    public float shotTimeTrigger = 5.0f;
    //弾の射出感覚
    [SerializeField, Header("弾の射出感覚")]
    public float shotTimeOut = 5.0f;
    //ウィザードの移動速度
    [SerializeField,Header("ウィザードの移動速度")]
    private float movePower = 0.02f;
    //ウィザードの移動制限
    [SerializeField]
    private float maxLimitPosition = 5.0f;
    [SerializeField]
    private float minLimitPosition = -5.0f;

    private void Start()
    {
        HPUI.value = maxLifePoint;
    }
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
        if(lifePoint<= 0)
        {
            isDead = true;
        }
        HPUI.value = lifePoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            lifePoint--;
        }
    }
}
