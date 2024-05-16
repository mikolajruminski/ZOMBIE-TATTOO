using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameCameraScript : MonoBehaviour
{
    public static GameCameraScript Instance { get; private set; }
    public event EventHandler onLookBack, onLookFront;
    private bool isLookingFront = true;
    public KeyCode switchCameraView = KeyCode.Space;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
        
        gameObject.GetComponent<PlayerController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchView();
    }


    private void SwitchView()
    {
        if (GameManager.Instance.IsGameActive() && Input.GetKeyDown(KeyCode.Space))
        {

            if (isLookingFront)
            {
                Debug.Log("turnign back");
                onLookBack?.Invoke(this, EventArgs.Empty);
                isLookingFront = !isLookingFront;
            }
            else
            {
                Debug.Log("turnign front");
                onLookFront?.Invoke(this, EventArgs.Empty);
                isLookingFront = !isLookingFront;
            }
        }
    }

    public bool ReturnIsLookingFront()
    {
        return isLookingFront;
    }
}
