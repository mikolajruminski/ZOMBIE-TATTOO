using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotelScript : MonoBehaviour, IUseable
{
    public string Name => "Armchair";

    public string Description => "Sit?";
    [SerializeField] private Collider gameCollider, interactCollider;

    public void Start()
    {
        gameCollider.enabled = false;
        interactCollider.enabled = true;
    }

    public void Interact()
    {
        GameManager.Instance.SwitchGameMode();
        interactCollider.enabled = false;
        gameCollider.enabled = true;
    }

}
