using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContllor : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator = null;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            other.gameObject.SetActive(false);
        }
    }
}
