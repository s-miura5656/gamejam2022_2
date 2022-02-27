using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerContllor : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator = null;
    [SerializeField, Header("HPUI")]
    private Slider HPUI;
    [SerializeField]
    private GameObject shieldObject = null;
    [SerializeField, Header("プレイヤーの最大ライフポイント")]
    public int maxLifePoint = 5;
    [SerializeField, Header("プレイヤーのライフポイント")]
    public int lifePoint = 5;
    [SerializeField, Header("プレイヤーの死亡判定")]
    public bool isDead = false;
    //ウィザードの移動制限
    [SerializeField]
    private float maxLimitPositionZ = 5.0f;
    [SerializeField]
    private float minLimitPositionZ = -5.0f;
    [SerializeField]
    private float maxLimitPositionX = 5.0f;
    [SerializeField]
    private float minLimitPositionX = -5.0f;
    private void Start()
    {
        HPUI.value = maxLifePoint;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shieldObject.SetActive(true);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.01f);
                Vector3 angle = this.transform.eulerAngles;
                angle.y = 0.0f;
                this.transform.eulerAngles = angle;
                playerAnimator.SetBool("IsDush", true);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.01f);
                Vector3 angle = this.transform.eulerAngles;
                angle.y = 180.0f;
                this.transform.eulerAngles = angle;
                playerAnimator.SetBool("IsDush", true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.01f);
                Vector3 angle = this.transform.eulerAngles;
                angle.y = 90.0f;
                this.transform.eulerAngles = angle;
                playerAnimator.SetBool("IsDush", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.01f);
                Vector3 angle = this.transform.eulerAngles;
                angle.y = -90.0f;
                this.transform.eulerAngles = angle;
                playerAnimator.SetBool("IsDush", true);
            }
            else
            {
                playerAnimator.SetBool("IsSprint", false);
                playerAnimator.SetBool("IsDush", false);
            }
        }
        else
        {
            shieldObject.SetActive(false);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.02f);
                Vector3 angle = this.transform.eulerAngles;
                angle.y = 0.0f;
                this.transform.eulerAngles = angle;
                playerAnimator.SetBool("IsSprint", true);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.02f);
                Vector3 angle = this.transform.eulerAngles;
                angle.y = 180.0f;
                this.transform.eulerAngles = angle;
                playerAnimator.SetBool("IsSprint", true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.02f);
                Vector3 angle = this.transform.eulerAngles;
                angle.y = 90.0f;
                this.transform.eulerAngles = angle;
                playerAnimator.SetBool("IsSprint", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.02f);
                Vector3 angle = this.transform.eulerAngles;
                angle.y = -90.0f;
                this.transform.eulerAngles = angle;
                playerAnimator.SetBool("IsSprint", true);
            }
            else
            {
                playerAnimator.SetBool("IsSprint", false);
                playerAnimator.SetBool("IsDush", false);
            }
        }
        if (transform.position.x <= minLimitPositionX)
        {
            transform.position = new Vector3(minLimitPositionX, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= maxLimitPositionX)
        {
            transform.position = new Vector3(maxLimitPositionX, transform.position.y, transform.position.z);
        }
        if (transform.position.z <= minLimitPositionZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minLimitPositionZ);
        }
        if (transform.position.z >= maxLimitPositionZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxLimitPositionZ);
        }
        HPUI.value = lifePoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            lifePoint--;
        }
    }
}
