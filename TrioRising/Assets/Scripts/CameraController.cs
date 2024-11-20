using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] int sensitivity;
    [SerializeField] int lockVertMin;
    [SerializeField] int lockVertMax;
    [SerializeField] bool invertY;

    [SerializeField] float bobFrequency = 5f;   // Frequency of the bobbing effect
    [SerializeField] float bobAmplitude = 0.05f; // Height of the bobbing effect
    [SerializeField] float bobSway = 0.05f;     // Side-to-side sway of the bobbing
    [SerializeField] PlayerController playerController;  // Reference to PlayerController

    private float rotx;
    private Vector3 initialPosition;
    private float bobTimer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialPosition = transform.localPosition;
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update()
    {
        // Handle mouse input
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        if (!invertY)
            rotx -= mouseY;
        else
            rotx += mouseY;
        
        rotx = Mathf.Clamp(rotx, lockVertMin, lockVertMax);
        transform.localRotation = Quaternion.Euler(rotx, 0, 0);
        transform.parent.Rotate(Vector3.up * mouseX);

        // Get whether the player is sprinting
        //bool isSprinting = playerController.IsSprinting;

        // Modify bobbing based on sprinting status
        if (playerController != null && playerController.IsMoving())
        {
            bobTimer += Time.deltaTime * bobFrequency;

            // Intensify bobbing when sprinting
            float intensityMultiplier = playerController.IsSprinting ? 2.5f : 1f; // Increase intensity by 1.5x when sprinting

            float bobOffsetY = Mathf.Sin(bobTimer) * bobAmplitude * intensityMultiplier;
            float bobOffsetX = Mathf.Cos(bobTimer * 0.5f) * bobSway * intensityMultiplier;
            transform.localPosition = initialPosition + new Vector3(bobOffsetX, bobOffsetY, 0);
        }
        else
        {
            // Reset to the initial position if the player stops moving
            bobTimer = 0;
            transform.localPosition = initialPosition;
        }
    }
}