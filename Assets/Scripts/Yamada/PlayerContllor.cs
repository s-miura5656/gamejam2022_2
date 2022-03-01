using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのコントローラー
/// </summary>
public class PlayerContllor : MonoBehaviour
{
    //アニメーター
    [SerializeField]
    private Animator animator = null;

    //体力バー
    [SerializeField]
    private Slider hpBar;

    //シールド
    [SerializeField]
    private GameObject shieldObject = null;

    [SerializeField, Header("プレイヤーの最大ライフポイント")]
    private int maxLifePoint = 5;

    [SerializeField, Header("プレイヤーのライフポイント")]
    private int lifePoint = 5;

    [SerializeField, Header("プレイヤーの死亡判定")]
    private bool isDead = false;

    [SerializeField, Header("プレイヤーの移動速度")]
    private float moveSpeed = 50.0f;

    [SerializeField, Header("プレイヤーの回転速度")]
    private float rotateSpeed = 25.0f;

    private Vector3 moveAngle = Vector3.zero;
    #region プレイヤーの移動制限
    [SerializeField]
    private float maxLimitPositionZ = 5.0f;

    [SerializeField]
    private float minLimitPositionZ = -5.0f;

    [SerializeField]
    private float maxLimitPositionX = 5.0f;

    [SerializeField]
    private float minLimitPositionX = -5.0f;
    #endregion

    private void Start()
    {
        hpBar.value = maxLifePoint;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shieldObject.SetActive(true);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveCharacter(Vector3.forward, rotateSpeed, "IsWalk");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveCharacter(Vector3.back, rotateSpeed, "IsWalk");
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveCharacter(Vector3.right, rotateSpeed, "IsWalk");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveCharacter(Vector3.left, rotateSpeed, "IsWalk");

            }
        }
        else
        {
            shieldObject.SetActive(false);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                MoveCharacter(Vector3.forward, rotateSpeed, "IsDash");
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                MoveCharacter(Vector3.back, rotateSpeed, "IsDash");
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveCharacter(Vector3.right, rotateSpeed, "IsDash");
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveCharacter(Vector3.left, rotateSpeed, "IsDash");
            }
        }

        if (!Input.anyKey)
        {
            moveAngle = Vector3.zero;
        }

        if(Vector3.zero == moveAngle)
        {
            animator.SetBool("IsDash", false);
            animator.SetBool("IsWalk", false);
        }

        MoveLimit(maxLimitPositionX, minLimitPositionX, maxLimitPositionZ, minLimitPositionZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            lifePoint--;
            hpBar.value = lifePoint;
        }
    }

    /// <summary>
    /// キャラクターの移動関数
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <param name="rotateSpeed"></param>
    /// <param name="playAnime"></param>
    private void MoveCharacter(Vector3 moveDirection ,float rotateSpeed, string playAnime)
    {
        moveAngle = moveDirection;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        var look = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime * rotateSpeed);

        animator.SetBool(playAnime, true);
    }

    /// <summary>
    /// プレイヤーの移動制限
    /// </summary>
    /// <param name="maxLimitX"></param>
    /// <param name="minLimitX"></param>
    /// <param name="maxLimitZ"></param>
    /// <param name="minLimitZ"></param>
    private void MoveLimit(float maxLimitX, float minLimitX, float maxLimitZ, float minLimitZ)
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minLimitX, maxLimitX),transform.position.y,
                                         Mathf.Clamp(transform.position.z, minLimitZ, maxLimitZ));
    }
}
