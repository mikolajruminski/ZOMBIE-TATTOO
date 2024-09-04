using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions playerInputActions;

    public event EventHandler OnInteractButtonPressed;
    public event EventHandler OnSpecialMovePerformed;
    public event EventHandler OnReloadPerformed;
    public event EventHandler OnShopOpened;
    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

    }

    private void Start()
    {
        playerInputActions.Player.Interact.performed += Interact_Performed;
        playerInputActions.Player.SpecialMove.performed += SpecialMove_Performed;
        playerInputActions.Player.Reload.performed += Reload_Performed;
        playerInputActions.Player.OpenShop.performed += Open_Shop;

    }

    private void Open_Shop(InputAction.CallbackContext context)
    {
        OnShopOpened?.Invoke(this, EventArgs.Empty);
    }

    private void Reload_Performed(InputAction.CallbackContext context)
    {
        OnReloadPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void SpecialMove_Performed(InputAction.CallbackContext context)
    {
        OnSpecialMovePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext context)
    {
        OnInteractButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public float GetMouseXFloat()
    {

        var moveX = Mouse.current.delta.x.ReadValue();

        return moveX;
    }

    public float GetMouseYFloat()
    {
        var moveY = Mouse.current.delta.y.ReadValue();

        return moveY;
    }

    public bool GetMouseLeftButtonPressed()
    {
        var mouseLeft = Mouse.current.leftButton.isPressed;

        return mouseLeft;
    }

    public bool GetMouseLeftButton()
    {
        var mouseLeft = Mouse.current.leftButton.wasPressedThisFrame;

        return mouseLeft;
    }



}


