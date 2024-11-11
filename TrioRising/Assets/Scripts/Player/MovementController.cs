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
        public bool IsSprinting {  get; private set; }
        public bool IsCrouching { get; private set; }
        public bool IsJumping { get; private set; }

#endregion

        #region PLAYER_COMPONENTS
        [Header(" ---- Player Components ---- ")]
        [SerializeField] private PlayerController Owner;
        [SerializeField] private CharacterController Controller;
        #endregion

        #region BASIC_MOVEMENT
        // TODO: Add movement parameters
        [Header(" ---- Basic Movement ---- ")]
        [SerializeField] protected float speed;             // Player movemnet speed
        [SerializeField] protected int jumpMax;             // Maximum jump height
        [SerializeField] protected float jumpSpeed;         // Speed of player jump
        [SerializeField] protected float gravity;           // Strength of Gravity
        [SerializeField] protected int sprintModifier;      // Modifier applied to speed when player is sprinting
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
        public float SprintSpeeed;
        public float WalkSpeed;
        public float CrouchSpeed;
        public float JumpForce;
        #endregion

        #region MOVEMENT_DATA
        struct MovementData
        {
            public Vector3 Position;
            public Vector3 Velocity;
            public float ForwardrdMove;
            public float SideMove;
            public float GravityScale;
            public float FrictionFactor;
            public bool IsDucking;
            public bool IsJumping;
            public bool IsSprinting;
            public Vector3 SurfaceNormal;
        }
        // Instance of movement data for the Player's movement
        MovementData playerData = new MovementData();
        // Returns the players current velocity 
        public Vector3 Velocity() { return playerData.Velocity; }
        #endregion

        private bool isGrounded;
        private bool wasGrouned;

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

        // Checks for collision with gound using capsule cast
        // private bool CheckHitFloor(out RaycastHit outhit)
        
    } 
}
