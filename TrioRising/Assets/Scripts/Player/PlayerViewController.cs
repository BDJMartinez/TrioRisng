using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.Player
{
	public class PlayerViewController : MonoBehaviour
	{
        #region PUBLIC_PROPERTIES
        public float TargetFOV {  get => targetFOV; private set => targetFOV = value; }
        public float LookSensitivity { get => lookSensitivity; private set => lookSensitivity = value; }
        public bool InvertYAxis { get => invertYAxis; private set => invertYAxis = value; }
        public float MaxPitchAngle { get => maxPitchAgle; private set => maxPitchAgle = value; }
        public Vector2 RotationAngles { get => rotationAngles; }
        #endregion

        #region VIEW_SETTINGS
        [Header(" ---- View Setting ---- ")]
		[Tooltip("Feild of view for the player's camera.")]
		[SerializeField] private float targetFOV = 60f;
		[Tooltip("Sensitivity of the camera's look controls.")]
		[SerializeField] private float lookSensitivity  = 100f;
		[Tooltip("Invert the Y-Axis for looking up and down.")]
		[SerializeField] private bool invertYAxis = false;
		[Tooltip("Reference to the player's camera.")]
		[SerializeField] private Camera playerCamera;
        [Tooltip("The maximum pitch angle for the prevent over-rotation on the Y-Axis.")]
        [SerializeField] private float maxPitchAgle;
        private int lockVerticalMinimum;
        private int lockVerticalMaximum;
		#endregion

		// Stores the camera rotation angles: X for pitch, and Y for yaw
		private Vector2 rotationAngles;

        #region LIFECYCLE_METHODS
        // ---- Lifecycle Methods ----
        private void Start()
        {
            
        }

        private void Update()
        {
            HandleCameraRotation();
        }
        #endregion

        #region VIEW_HANDLING_&_MANIPULATION
        // Locks the cursor to the window and makes it invisible
        private void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Adjust the camera's FOV
        public void AdjustFieldOfView(float fieldOfView, float transtionSpeed)
        {
            if (playerCamera != null)
            {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fieldOfView, transtionSpeed);
            }
            else
                Debug.LogWarning("PlayerViewController: No camera assigned for FOV adjustment.");
        }
        // Process player input to rotate the camera and player model
        public void HandleCameraRotation()
        {
            // Get the mouse movemnt deltas\
            float mouseXDelta = Input.GetAxis("Mouse X") * lookSensitivity *Time.deltaTime;
            float mouseYDelta = Input.GetAxis("Mouse X") * lookSensitivity *Time.deltaTime;
            // Update pitch angle with optional Y-Axis inversion
            rotationAngles.x += invertYAxis ? mouseYDelta : -mouseYDelta;
            // Update your angle
            rotationAngles.x = Mathf.Clamp(rotationAngles.x, -MaxPitchAngle, MaxPitchAngle);
            // Apply pitch to the camera's local X-Axis rotation
            rotationAngles.y += mouseXDelta;
            // If the player camera is assigned
            if (playerCamera != null)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(rotationAngles.x, 0f, 0f);
            }
            // Apply yaw to the player's Y rotation
            transform.rotation = Quaternion.Euler(0f, rotationAngles.y, 0f);
        }
        #endregion
    }
}