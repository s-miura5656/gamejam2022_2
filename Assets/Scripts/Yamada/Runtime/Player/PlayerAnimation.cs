using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのアニメーションクラス
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    //プレイヤーのアニメーションコントローラー
    [SerializeField]
    private Animator animator = null;

    //走るアニメーション
    private const string animeDash = "IsDash";

    //ジャンプアニメーション
    private const string animeWalk = "IsWalk";

    //ダッシュアニメのフラグ
    private bool isAnimeDash = false;

    //歩行のアニメのフラグ
    private bool isAnimeWalk = false;

    //ダッシュアニメのフラグのセッター
    public bool SetIsAnimeDash
    {
        set { isAnimeDash = value; }
    }

    //歩行のアニメのフラグのセッター
    public bool SetAnimeWalk
    {
        set { isAnimeWalk = value; }
    }

    private void Update()
    {
        animator.SetBool(animeDash, isAnimeDash);
        animator.SetBool(animeWalk, isAnimeWalk);
    }
}
