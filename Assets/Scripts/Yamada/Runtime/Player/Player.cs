using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimation animator = null;

    [SerializeField]
    private Slider hpBar = null;

    [SerializeField]
    private GameObject shieldObject = null;

    [SerializeField]
    private PlayerStatusAsset playerStatus = null;

    private void Start()
    {
        hpBar.value = playerStatus.GetMaxLifePoint;
    }

    private void Update()
    {
        var direction = GetPlayerInputDirection();
        var isLeftShiftKey = InputManager.Instance.IsShieldKey;
        shieldObject.SetActive(isLeftShiftKey);

        if (Vector3.zero == direction)
        {
            animator.IsAnimeWalk = false;
            animator.IsAnimeDash = false;

            return;
        }

        MovePlayer(direction, isLeftShiftKey);
        MoveLimit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            playerStatus.LifePoint--;
            hpBar.value = playerStatus.LifePoint;
        }
    }

    /// <summary>
    /// プレイヤーのインプット方向
    /// </summary>
    private Vector3 GetPlayerInputDirection()
    {

        var moveDirection = Vector3.zero;

        if (InputManager.Instance.IsUpMoveKey)
        {
            moveDirection += Vector3.forward;
        }
        else if (InputManager.Instance.IsDownMoveKey)
        {
            moveDirection += Vector3.back;
        }

        if (InputManager.Instance.IsRightMoveKey)
        {
            moveDirection += Vector3.right;
        }
        else if (InputManager.Instance.IsLeftMoveKey)
        {
            moveDirection += Vector3.left;
        }

        return moveDirection.normalized;
    }

    /// <summary>
    /// キャラクターの移動関数
    /// </summary>
    /// <param name="moveDirection"></param>
    /// <param name="isLeftShiftKey"></param>
    private void MovePlayer(Vector3 moveDirection ,bool isLeftShiftKey)
    {
        transform.position += moveDirection * playerStatus.GetMoveSpeed * Time.deltaTime;

        var look = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime * playerStatus.GetRotateSpeed);

        if (isLeftShiftKey)
        {
            animator.IsAnimeWalk = true;
        }
        else
        {
            animator.IsAnimeDash = true;
        }
    }

    /// <summary>
    /// プレイヤーの移動制限
    /// </summary>
    private void MoveLimit()
    {
        var limitX = Mathf.Clamp(transform.position.x, playerStatus.GetMinLimitPositionX, playerStatus.GetMaxLimitPositionX);
        var limitZ = Mathf.Clamp(transform.position.z, playerStatus.GetMinLimitPositionZ, playerStatus.GetMaxLimitPositionZ);
        transform.position = new Vector3(limitX, transform.position.y, limitZ);
    }
}