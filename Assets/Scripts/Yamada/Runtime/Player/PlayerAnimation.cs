using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private readonly string AnimeDash = "IsDash";

    private readonly string AnimeWalk = "IsWalk";

    [SerializeField]
    private Animator animator = null;

    public bool IsAnimeDash { get; set; }

    public bool IsAnimeWalk { get; set; }

    private void Update()
    {
        animator.SetBool(AnimeDash, IsAnimeDash);
        animator.SetBool(AnimeWalk, IsAnimeWalk);
    }
}