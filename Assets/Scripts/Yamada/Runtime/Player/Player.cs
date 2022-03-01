using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのコントローラー
/// </summary>
public class Player : MonoBehaviour
{
    //アニメーター
    [SerializeField]
    private PlayerAnimation animator = null;

    //体力バー
    [SerializeField]
    private Slider hpBar = null;

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

    /// <summary>
    /// 初期動作
    /// </summary>
    private void Start()
    {
        hpBar.value = maxLifePoint;
    }

    /// <summary>
    /// アップデート
    /// </summary>
    private void Update()
    {
        Move();

        MoveLimit();
    }

    /// <summary>
    /// コリジョンの当たり判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            lifePoint--;
            hpBar.value = lifePoint;
        }
    }

    /// <summary>
    /// プレイヤー移動
    /// </summary>
    private void Move()
    {
        var isLeftShiftKey = Input.GetKey(KeyCode.LeftShift);

        shieldObject.SetActive(isLeftShiftKey);

        var moveDirection = Vector3.zero;

        if (InputManager.Instance.GetIsUpArrowKey)
        {
            moveDirection += Vector3.forward;
        }
        else if (InputManager.Instance.GetIsDownArrowKey)
        {
            moveDirection += Vector3.back;
        }

        if (InputManager.Instance.GetIsRightArrowKey)
        {
            moveDirection += Vector3.right;
        }
        else if (InputManager.Instance.GetIsLeftArrowKey)
        {
            moveDirection += Vector3.left;
        }

        if (Vector3.zero == moveDirection)
        {
            animator.SetAnimeWalk = false;
            animator.SetIsAnimeDash = false;
        }
        else
        {
            MoveCharacter(moveDirection.normalized, rotateSpeed, isLeftShiftKey);
        }
    }

    /// <summary>
    /// キャラクターの移動関数
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <param name="rotateSpeed"></param>
    /// <param name="isLeftShiftKey"></param>
    private void MoveCharacter(Vector3 moveDirection ,float rotateSpeed,bool isLeftShiftKey)
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        var look = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime * rotateSpeed);

        if (isLeftShiftKey)
        {
            animator.SetAnimeWalk = true;
        }
        else
        {
            animator.SetIsAnimeDash = true;
        }
    }

    /// <summary>
    /// プレイヤーの移動制限
    /// </summary>
    private void MoveLimit()
    {
        var limitX = Mathf.Clamp(transform.position.x, minLimitPositionX, maxLimitPositionX);
        var limitZ = Mathf.Clamp(transform.position.z, minLimitPositionZ, maxLimitPositionZ);
        transform.position = new Vector3(limitX, transform.position.y, limitZ);
    }
}
