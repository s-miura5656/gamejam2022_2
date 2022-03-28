using UnityEngine;

[CreateAssetMenu(fileName = "WizardStatusAseets", menuName = "ScriptableObjects/CreateWizardStatusAsset")]
public class WizardStatusAseet : CharacterStatus
{
    [SerializeField, Header("更新間隔")]
    private float shotIntervalTime = 10.0f;

    #region 移動制限

    [SerializeField]
    private float maxLimitPosition = 5.0f;

    [SerializeField]
    private float minLimitPosition = -5.0f;

    #endregion

    public float GetShotIntervalTime => shotIntervalTime;

    public float GetMaxLimitPosition => maxLimitPosition;

    public float GetMinLimitPosition => minLimitPosition;
}
