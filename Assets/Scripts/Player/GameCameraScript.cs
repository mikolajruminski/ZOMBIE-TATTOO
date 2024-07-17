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
    private float lerpDuraiton = 0.3f;
    private Vector3 backLook = new Vector3(0, 90, 0);
    private Vector3 frontLook = new Vector3(0, -90, 0);
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
                // onLookBack?.Invoke(this, EventArgs.Empty);
                isLookingFront = !isLookingFront;
                StartCoroutine(LerpCameraPosition(Quaternion.Euler(backLook)));
            }
            else
            {
                // onLookFront?.Invoke(this, EventArgs.Empty);
                isLookingFront = !isLookingFront;
                StartCoroutine(LerpCameraPosition(Quaternion.Euler(frontLook)));
            }
        }
    }

    public bool ReturnIsLookingFront()
    {
        return isLookingFront;
    }

    private IEnumerator LerpCameraPosition(Quaternion endValue)
    {
        float timeElapsed = 0;

        Quaternion startValue = transform.rotation;

        while (timeElapsed < lerpDuraiton)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, timeElapsed / lerpDuraiton);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = endValue;
        PlayerController.Instance.ChangeCameraClamp();

    }
}
