using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigAnimationScript : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController.Instance.onInteract += OnInteract;
    }

    private void OnInteract(object sender, EventArgs e)
    {
        int x = UnityEngine.Random.Range(0, 2);

        if (x == 0)
        {
            animator.SetTrigger("Interact1");
        }
        else
        {
            animator.SetTrigger("Interact2");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
