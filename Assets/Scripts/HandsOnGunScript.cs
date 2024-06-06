using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HandsOnGunScript : MonoBehaviour
{
    private TwoBoneIKConstraint twoBoneIKConstraint;
    [SerializeField] RigBuilder rigBuilder;
    // Start is called before the first frame update

    private void Awake()
    {
        twoBoneIKConstraint = GetComponent<TwoBoneIKConstraint>();
    }
    void Start()
    {
        WeaponManagerScript.Instance.onGunChanged += OnGunChanged;
        SetHandsOnGun();
    }

    private void OnGunChanged(object sender, EventArgs e)
    {
        SetHandsOnGun();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetHandsOnGun()
    {
        if (gameObject.tag == "Right")
        {
            twoBoneIKConstraint.data.target = GameManager.Instance.GetActiveGun()._rightHandPos;

            rigBuilder.Build();
        }
        else
        {
            return;
        }

    }


}
