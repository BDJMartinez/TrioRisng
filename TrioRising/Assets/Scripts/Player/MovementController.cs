using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UndeadWarfare.Player
{
    public class MovementController : MonoBehaviour
    {
        #region PUBLIC_PROPERTIES
#if false
        public float Speed { get => speed; set => speed = value; }
        public int JumpMax { get => jumpMax; set => jumpMax = value; }
        public float JumpSoeed { get => jumpSpeed; set => jumpSpeed = value; }
        public float Gravity { get => gravity; set => gravity = value; }
        public int SprintModifier { get => sprintModifier; set => sprintModifier = value; }
#endif
        public bool IsSprinting { get; private set; }
        public bool IsCrouching { get; private set; }
        public bool IsJumping { get; private set; }
        public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
        public bool WasGrounded { get => wasGrouned; set => wasGrouned = value; }

        #endregion

        #region PLAYER_COMPONENTS
        [Header(" ---- Player Components ---- ")]
        [SerializeField] private PlayerController Owner;
        [SerializeField] private CharacterController Controller;
        #endregion

        #region BASIC_MOVEMENT
#if fasle
        // TODO: Add movement parameters
        [Header(" ---- Basic Movement ---- ")]
        [SerializeField] protected float speed;             // Player movemnet speed
        [SerializeField] protected int jumpMax;             // Maximum jump height
        [SerializeField] protected float jumpSpeed;         // Speed of player jump
        [SerializeField] protected float gravity;           // Strength of Gravity
        [SerializeField] protected int sprintModifier;      // Modifier applied to speed when player is sprinting
#endif
        #endregion

        #region MOVEMENT_SETTINGS
        [Header(" ---- Height Settings ---- ")]
        public float CrouchHeight;
        public float StandHeight;

        // ---- Gravity Settings ---- 
        [Header(" ---- Gravity Settings ---- ")]
        public float GravityAmount;

        // ---- Movement Parameters ----
        [Header(" ---- Movement Parameters ---- ")]
        public float Friction;
        public float Acceleration;
        public float Deceleration;
        public float AirAcceleration;

        // ---- Crouch movement parameters
        [Header(" ---- Crouch Movement Parameters ---- ")]
        public float CrouchFriction;
        public float CrouchAcceleration;
        public float CrouchDeceleration;

        // ---- Speed settings
        [Header(" ---- Speed Settings ---- ")]
        public float SprintSpeed;
        public float WalkSpeed;
        public float CrouchSpeed;
        public float JumpForce;
        #endregion

        #region MOVEMENT_DATA
        struct MovementData
        {
            public Vector3 Position;
            public Vector3 Velocity;
            public float VerticalAxis;     // Raw input on the vertical axis
            public float HorizontalAxis;   // Raw input on the horizontal axis
            public float ForwardMove;
            public float SideMove;
            public float GravityScale;
            public float FrictionFactor;
            public bool IsDucking;
            public bool IsJumping;
            public bool IsSprinting;
            public Vector3 SurfaceNormal;
        }

        // Air movement limitations
        [Header(" ---- Air Movement ---- ")]
        [SerializeField] bool ClampAirSpeed;
        [SerializeField] float MaxSpeed;

        // Instance of movement data for the Player's movement
        MovementData playerData = new MovementData();
        // Returns the players current velocity 
        public Vector3 Velocity() { return playerData.Velocity; }

        private bool isGrounded;
        private bool wasGrouned;
        #endregion

        #region LIFECYCLE_METHODS
        // Start is called before the first frame update
        void Start()
        {
            playerData.GravityScale = 1f;
            playerData.FrictionFactor = 1f;
            playerData.Position = transform.position;
        }
        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        #region CHECKS_&_CALCULATIONS
        private bool CheckHitFloor(out RaycastHit outHit)
        {
            var start = playerData.Position;
            var end = new Vector3(playerData.Position.x, playerData.Position.y - 0.2f, playerData.Position.z);

            // Calculate the top and bottom points of the capsule
            var distance = (Controller.height * 0.5f) - Controller.radius;
            var top = start + Controller.center + Vector3.up * distance;
            var bottom = start + Controller.center - Vector3.up * distance;

            // Calculate extended collision detection distance
            var longSide = Mathf.Sqrt(Controller.contactOffset * Controller.contactOffset + Controller.contactOffset * Controller.contactOffset);
            var delta = end - start;
            var direction = delta.normalized;
            var maxDistance = delta.magnitude + longSide;

            // Perform capsule cast to detect ground
            if (Physics.CapsuleCast(top, bottom, Controller.radius, direction, out RaycastHit hit, maxDistance))
            {
                outHit = hit;  // Set output parameter with hit information
                return true;   // Ground detected
            }

            outHit = default;  // No ground detected
            return false;
        }

        // Checks if the player is grounded by evaluating the ground hit
        bool CheckGrounded()
        {
            bool onGround = CheckHitFloor(out RaycastHit hit);
            return onGround;
        }

        // Calculates the direction the player wants to move in
        Vector3 CalculateMoveDirection()
        {
            return new Vector3(playerData.SideMove, 0, playerData.ForwardMove).normalized;
        }
        #endregion

        #region HANDLER_METHODS
        // Updates movement input based on player actions
        void UpdateMoveInput()
        {
            playerData.VerticalAxis = Input.GetAxisRaw("Vertical");
            playerData.HorizontalAxis = Input.GetAxisRaw("Horizontal");
            playerData.IsSprinting = Input.GetButton("Sprint");
            playerData.IsDucking = Input.GetButton("Crouch");

            // Jump input handling with state management
            playerData.IsJumping = Input.GetButtonDown("Jump");
            if (!Input.GetButton("Jump"))
                playerData.IsJumping = false;

            // Setup movement direction based on input axes
            bool inLeft = playerData.HorizontalAxis < 0f;
            bool inRight = playerData.HorizontalAxis > 0f;
            bool inForward = playerData.VerticalAxis > 0f;
            bool inBack = playerData.VerticalAxis < 0f;

            playerData.SideMove = (!inLeft && !inRight) ? 0f : (inLeft ? -Acceleration : inRight ? Acceleration : 0f);
            playerData.ForwardMove = (!inForward && !inBack) ? 0f : (inForward ? Acceleration : inBack ? -Acceleration : 0f);
        }

        // Main function handling movement and gravity application
        void HandleMove()
        {
            playerData.Position = transform.position;

            if (playerData.Velocity.y <= 0f)
                IsJumping = false;

            // Apply gravity if not grounded
            if (!isGrounded)
                playerData.Velocity.y -= (playerData.GravityScale * GravityAmount * Time.deltaTime);

            isGrounded = CheckGrounded();  // Check if grounded and update velocity
            SetupVelocity();

            // Apply movement using the character controller
            Controller.Move(playerData.Velocity * Time.deltaTime);

            WasGrounded = isGrounded;  // Update grounded state
        }

        // Applies friction based on the friction amount and ground state
        void ApplyFriction(float amount, bool isGrounded)
        {
            Vector3 vel = playerData.Velocity;
            vel.y = 0.0f;
            float speed = vel.magnitude;
            float drop = 0f;
            float friction = playerData.IsDucking ? CrouchFriction : Friction;
            float deceleration = playerData.IsDucking ? CrouchDeceleration : Deceleration;

            if (isGrounded)
            {
                float control = Mathf.Max(deceleration, speed);
                drop = control * friction * Time.deltaTime * amount;
            }

            float newSpeed = Mathf.Max(speed - drop, 0f);
            if (newSpeed > 0f)
            {
                newSpeed /= speed;
                playerData.Velocity.x *= newSpeed;
                playerData.Velocity.z *= newSpeed;
            }
        }

        // Sets up velocity based on movement type and speed
        void SetupVelocity()
        {
            if (!isGrounded)
            {
                HandleAirMove();  // Apply air movement if in the air
                return;
            }

            float accel = playerData.IsDucking ? CrouchAcceleration : Acceleration;
            float speed = playerData.IsSprinting ? SprintSpeed : WalkSpeed;
            if (playerData.IsDucking)
                speed = CrouchSpeed;

            IsCrouching = playerData.IsDucking;
            IsSprinting = playerData.IsSprinting;
            // Apply friction and jump if requested
            if (playerData.IsJumping && !IsJumping)
            {
                ApplyFriction(0.0f, true);
                HandleJump();
                return;
            }
            ApplyFriction(playerData.FrictionFactor, true);
            // Calculate desired direction and play footstep sounds when moving
            var wishdir = CalculateMoveDirection();
            float wishSpeed = wishdir.magnitude * speed;
            // Backup and restore Y velocity for clamping
            float backupY = playerData.Velocity.y;
            ApplyAcceleration(wishdir, wishSpeed, accel, false);
            playerData.Velocity = Vector3.ClampMagnitude(new Vector3(playerData.Velocity.x, 0f, playerData.Velocity.z), 50f);
            playerData.Velocity.y = backupY;
        }

        // Method to apply acceleration in a specified direction
        void ApplyAcceleration(Vector3 wishDir, float wishSpeed, float acceleration, bool affectY)
        {
            float currentSpeed = Vector3.Dot(playerData.Velocity, wishDir);
            float delta = wishSpeed - currentSpeed;
            if (delta <= 0)
                return;

            float newSpeed = Mathf.Min(acceleration * Time.deltaTime * wishSpeed, delta);

            playerData.Velocity.x += newSpeed * wishDir.x;
            playerData.Velocity.z += newSpeed * wishDir.z;

            if (affectY)
                playerData.Velocity.y += newSpeed * wishDir.y;
        }

        // Handles air movement based on input
        void HandleAirMove()
        {
            var wishdir = CalculateMoveDirection();
            float wishSpeed = wishdir.magnitude * WalkSpeed;

            if (ClampAirSpeed)
                wishSpeed = Mathf.Min(wishSpeed, MaxSpeed);

            ApplyAcceleration(wishdir, wishSpeed, AirAcceleration, false);
        }

        // Handles jump action with Y-axis velocity change
        void HandleJump()
        {
            playerData.Velocity.y = JumpForce;
            IsJumping = true;
        }
        #endregion

        // ---- TODO: Add Handlers  for audio. ----
    }
}
