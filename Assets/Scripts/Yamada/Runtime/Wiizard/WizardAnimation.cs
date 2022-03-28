using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimation : MonoBehaviour
{
    private readonly string AnimeWalk = "IsAttack";

    [SerializeField]
    private Animator animator = null;

    public bool IsAnimeAttack { get; set; }

    private void Update()
    {
        animator.SetBool(AnimeWalk, IsAnimeAttack);
    }
}
