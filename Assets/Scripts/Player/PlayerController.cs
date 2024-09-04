using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{
    public event EventHandler onInteract;
    public static PlayerController Instance { get; private set; }
    private Rigidbody rb;
    #region  Camera
    public Camera playerCamera;
    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float alimentMouseSensitivity = 0.5f;
    public float maxLookAngle = 50f;
    public float maxYawAngle = 50;
    public float gameMaxLookAngle = 30f;
    private bool isCameraFacingFront;
    private float maxYaw = 320f, minYaw = 200f;

    // Crosshair
    public bool lockCursor = true;
    public bool crosshair = true;
    public Sprite crosshairImage;
    public Color crosshairColor = Color.white;

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    #endregion

    #region  movement
    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;
    private bool isGrounded = false;

    #endregion

    // Internal Variables
    [SerializeField] private float interactDistance = 30f;
    [SerializeField] private TextMeshProUGUI interactText;

    #region SpecialMove

    // [SerializeField] private KeyCode specialActivationKey = KeyCode.CapsLock;

    #endregion

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Instance = this;


        rb = GetComponent<Rigidbody>();


        // Set internal variables
        playerCamera.fieldOfView = fov;

    }

    void Start()
    {
        interactText.gameObject.SetActive(false);
        isCameraFacingFront = true;

        GameInput.Instance.OnInteractButtonPressed += GameInput_OnInteractActionPressed;

    }


    private void GameInput_OnInteractActionPressed(object sender, EventArgs e)
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit rayhit, interactDistance))
        {
            if (rayhit.collider.TryGetComponent(out IUseable useable))
            {
                onInteract?.Invoke(this, EventArgs.Empty);
                useable.Interact();

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Control camera movement
        if (cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + GameInput.Instance.GetMouseXFloat() * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * GameInput.Instance.GetMouseYFloat();
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * GameInput.Instance.GetMouseYFloat();
            }

            // Clamp pitch between lookAngle
            if (GameManager.Instance.IsGameActive())
            {
                pitch = Mathf.Clamp(pitch, -gameMaxLookAngle, gameMaxLookAngle);
                yaw = Mathf.Clamp(yaw, minYaw, maxYaw);

            }
            else
            {
                pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
            }
            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        InteractText();
    }

    private void FixedUpdate()
    {
        if (playerCanMove)
        {
            // Calculate how fast we should be moving

            Vector3 targetVelocity = new Vector3(GameInput.Instance.GetMovementVectorNormalized().x, 0, GameInput.Instance.GetMovementVectorNormalized().y);

            // Checks if player is walking and isGrounded
            // Will allow head bob

            targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }


    private void InteractText()
    {

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit rayhit, interactDistance))
        {
            if (rayhit.collider.TryGetComponent(out IUseable useable))
            {
                interactText.gameObject.SetActive(true);
                interactText.text = useable.Description;

            }
            else
            {
                interactText.gameObject.SetActive(false);
            }
        }
    }

    public void ChangeCameraClamp()
    {
        if (isCameraFacingFront)
        {
            minYaw = 30f;
            maxYaw = 150f;
            isCameraFacingFront = !isCameraFacingFront;
        }
        else
        {
            maxYaw = 320f;
            minYaw = 200f;
            isCameraFacingFront = !isCameraFacingFront;
        }
    }

    public void SwitchCameraCanMove(bool state)
    {
        if (state == false)
        {
            cameraCanMove = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        else
        {
            cameraCanMove = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    #region Aliments

    public IEnumerator SlowCameraMovement(int resetTime)
    {
        float ogMouseSens;
        ogMouseSens = mouseSensitivity;
        mouseSensitivity = alimentMouseSensitivity;

        yield return new WaitForSeconds(resetTime);

        mouseSensitivity = ogMouseSens;
    }

    #endregion 





}

