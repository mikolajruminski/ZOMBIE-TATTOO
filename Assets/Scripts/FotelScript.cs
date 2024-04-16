using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FotelScript : MonoBehaviour, IUseable
{
    public string Name => "Armchair";

    public string Description => "Sit?";

    public void Interact()
    {
        GameManager.Instance.SwitchGameMode();
    }

}
