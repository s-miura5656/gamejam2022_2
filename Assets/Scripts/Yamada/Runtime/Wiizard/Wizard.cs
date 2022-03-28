using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Wizard : MonoBehaviour
{
    [SerializeField,Header("バレットマネージャークラス")]
    private BulletManager bulletManager = null;

    [SerializeField]
    private WizardAnimation animator = null;

    [SerializeField, Header("HPUI")]
    private Slider hpBar = null;

    [SerializeField]
    private WizardStatusAseet wizardStatus = null;

    private float shotTime = 0.0f;

    private void Start()
    {
        hpBar.value = wizardStatus.GetMaxLifePoint;
    }

    void Update()
    {
        shotTime += Time.deltaTime;

        if(shotTime >= wizardStatus.GetShotIntervalTime)
        {
            RandomMove(wizardStatus.GetMaxLimitPosition, wizardStatus.GetMinLimitPosition);

            shotTime = 0.0f;
        }

        if(wizardStatus.LifePoint <= 0)
        {
            wizardStatus.IsDead = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            wizardStatus.LifePoint--;
            hpBar.value = wizardStatus.LifePoint;
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

        seq.Append(this.transform.DOMoveZ(randomPos, wizardStatus.GetMoveSpeed)).
            AppendCallback(()=>
            {
                animator.IsAnimeAttack = true;
            });

        seq.OnStart(() =>
        {
            Debug.Log("testSeqOnStart");
        });

        seq.OnComplete(() =>
        {
            Debug.Log("testSeqOnComplete");
            animator.IsAnimeAttack = false;
        });
    }
}