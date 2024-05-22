using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator Instance { get; private set; }
    private Animator animator;


    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
