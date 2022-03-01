using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 魔法使いのコントローラー
/// </summary>
public class WizardContllor : MonoBehaviour
{
    //バレットマネージャークラス
    [SerializeField,Header("バレットマネージャークラス")]
    PlayerBulletManager playerBulletManager = null;

    [SerializeField,Header("アニメーションコントロール")]
    private Animator animator = null;

    [SerializeField, Header("HPUI")]
    private Slider hpBar = null;

    [SerializeField, Header("プレイヤーの最大ライフポイント")]
    private int maxLifePoint = 5;

    [SerializeField,Header("プレイヤーのライフポイント")]
    private int lifePoint = 5;

    [SerializeField, Header("プレイヤーの死亡判定")]
    private bool isDead = false;

    //プレイヤー死亡判定
    public bool IsDead
    {
        get { return isDead; }
    }

    //弾の射出用の時間
    private float shotTime = 0.0f;

    [SerializeField, Header("更新間隔")]
    private float shotIntervalTime = 10.0f;

    [SerializeField,Header("ウィザードの移動速度")]
    private float moveSpeed = 0.02f;

    #region 移動制限

    [SerializeField]
    private float maxLimitPosition = 5.0f;

    [SerializeField]
    private float minLimitPosition = -5.0f;

    #endregion

    private void Start()
    {
        hpBar.value = maxLifePoint;
    }
    void Update()
    {
        shotTime += Time.deltaTime;

        if(shotTime >= shotIntervalTime)
        {
            RandomMove(maxLimitPosition, minLimitPosition);
            animator.SetBool("IsAttack", true);
            shotTime = 0.0f;
        }
        else
        {
            animator.SetBool("IsAttack", false);
        }


        if(lifePoint<= 0)
        {
            isDead = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            lifePoint--;
            hpBar.value = lifePoint;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="maxLimitZ"></param>
    /// <param name="minLimitZ"></param>
    private void RandomMove(float maxLimitZ, float minLimitZ)
    {
        var randomPos = Random.Range(minLimitZ, maxLimitZ);
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMoveZ(randomPos, moveSpeed)).
            AppendCallback(() => {
                playerBulletManager.CreatePlayerBullet(this.transform.position);
            });
    }
}