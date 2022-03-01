using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// インプットマネージャークラス
/// </summary>
public class InputManager : SingletonMonoBehaviour<InputManager>
{
    //アップアローキー
    private bool isUpArrowKey = false;

    //ダウンアローキー
    private bool isDownArrowKey = false;

    //ライトアローキー
    private bool isRightArrowKey = false;

    //レフトアローキー
    private bool isLeftArrowKey = false;

    //アップアローキーフラグの取得
    public bool GetIsUpArrowKey
    {
        get { return isUpArrowKey; }
    }

    //ダウンアローキーフラグの取得
    public bool GetIsDownArrowKey
    {
        get { return isDownArrowKey; }
    }

    //ライトアローキーフラグの取得
    public bool GetIsRightArrowKey
    {
        get { return isRightArrowKey; }
    }

    //レフトアローキーフラグの取得
    public bool GetIsLeftArrowKey
    {
        get { return isLeftArrowKey; }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isUpArrowKey = true;
        }
        else
        {
            isUpArrowKey = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDownArrowKey = true;
        }
        else
        {
            isDownArrowKey = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            isRightArrowKey = true;
        }
        else
        {
            isRightArrowKey = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isLeftArrowKey = true;
        }
        else
        {
            isLeftArrowKey = false;
        }
    }
}