using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatusAsset", menuName = "ScriptableObjects/CreatePlayerStatusAsset")]
public class PlayerStatusAsset : CharacterStatus
{
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

    public float GetRotateSpeed => rotateSpeed;

    public float GetMaxLimitPositionZ => maxLimitPositionZ;

    public float GetMinLimitPositionZ => minLimitPositionZ;

    public float GetMaxLimitPositionX => maxLimitPositionX;

    public float GetMinLimitPositionX => minLimitPositionX;
}