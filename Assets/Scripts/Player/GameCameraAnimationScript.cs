using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameCameraScript gameCameraScript;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameCameraScript = GetComponent<GameCameraScript>();
    }
    void Start()
    {
        animator.enabled = false;
       
    }

    public void TurnOffAnimator()
    {
        animator.enabled = false;
    }
}
