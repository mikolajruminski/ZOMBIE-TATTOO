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
        gameCameraScript.onLookFront += OnLookFront;
        gameCameraScript.onLookBack += OnLookBack;
    }

    private void OnLookBack(object sender, EventArgs e)
    {
        animator.Play("GamePlayerAnim");
    }

    private void OnLookFront(object sender, EventArgs e)
    {
        animator.Play("GamePlayerAnimBack");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
