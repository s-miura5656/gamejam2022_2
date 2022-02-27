using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardContllor : MonoBehaviour
{
    //�o���b�g�}�l�[�W���[�N���X
    [SerializeField]
    PlayerBulletManager playerBulletManager;
    [SerializeField]
    private Animator wizardAnimator = null;

    //��莞�Ԃ��Ƃɒe�����b��
    [SerializeField]
    public float shotTimeTrigger = 5.0f;
    //�e�̎ˏo���o
    [SerializeField]
    public float shotTimeOut = 5.0f;
    //�E�B�U�[�h�̈ړ����x
    [SerializeField]
    private float movePower = 0.02f;
    //�E�B�U�[�h�̈ړ�����
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
