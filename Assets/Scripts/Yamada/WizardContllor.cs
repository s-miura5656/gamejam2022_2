using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardContllor : MonoBehaviour
{
    //�o���b�g�}�l�[�W���[�N���X
    [SerializeField,Header("�o���b�g�}�l�[�W���[�N���X")]
    PlayerBulletManager playerBulletManager;
    [SerializeField,Header("�A�j���[�V�����R���g���[��")]
    private Animator wizardAnimator = null;
    [SerializeField, Header("HPUI")]
    private Slider HPUI;

    [SerializeField, Header("�v���C���[�̍ő僉�C�t�|�C���g")]
    public int maxLifePoint = 5;
    [SerializeField,Header("�v���C���[�̃��C�t�|�C���g")]
    public int lifePoint = 5;
    [SerializeField, Header("�v���C���[�̎��S����")]
    public bool isDead = false;

    //��莞�Ԃ��Ƃɒe�����b��
    [SerializeField,Header("��莞�Ԃ��Ƃɒe�����b��")]
    public float shotTimeTrigger = 5.0f;
    //�e�̎ˏo���o
    [SerializeField, Header("�e�̎ˏo���o")]
    public float shotTimeOut = 5.0f;
    //�E�B�U�[�h�̈ړ����x
    [SerializeField,Header("�E�B�U�[�h�̈ړ����x")]
    private float movePower = 0.02f;
    //�E�B�U�[�h�̈ړ�����
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
