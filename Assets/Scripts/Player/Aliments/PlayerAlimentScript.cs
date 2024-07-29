using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlimentScript : MonoBehaviour
{

    public static PlayerAlimentScript Instance { get; private set; }
    private PlayerAliments currentAliment;

    [SerializeField] private int _cannotTurnAlimentTimeCountdown = 3;
    [SerializeField] private int _slowCameraMovementResetTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GivePlayerAliment(PlayerAliments aliment)
    {
        if (currentAliment == PlayerAliments.None)
        {
            switch (aliment)
            {
                case PlayerAliments.Blindness:
                    Debug.Log("blindness");
                    break;

                case PlayerAliments.UnloadWeapon:
                    Debug.Log("unload weapon");
                    break;

                case PlayerAliments.CannotTurn:
                    Debug.Log("cannot turn");
                    StartCoroutine(CannotTurnCorutine());
                    break;

                case PlayerAliments.SlowerCameraMovement:
                    StartCoroutine(PlayerController.Instance.SlowCameraMovement(_slowCameraMovementResetTime));
                    Debug.Log("slower camera movmeent");
                    break;
            }
        }

    }


    public enum PlayerAliments
    {
        None, Blindness, UnloadWeapon, CannotTurn, SlowerCameraMovement
    }

    private IEnumerator CannotTurnCorutine()
    {
        currentAliment = PlayerAliments.CannotTurn;
        GameCameraScript gameCameraScript = GetComponent<GameCameraScript>();
        gameCameraScript.SwitchCanTurn();

        yield return new WaitForSeconds(_cannotTurnAlimentTimeCountdown);

        gameCameraScript.SwitchCanTurn();
        currentAliment = PlayerAliments.None;
    }
}
