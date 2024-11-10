using System.Collections;
using System.Collections.Generic;
using UndeadWarfare.AI;
using UnityEngine;

namespace UndeadWarfare.Player
{
    public class PlayerController : MonoBehaviour, IDamage
    {
        #region PUBLIC_PROPERTIES
        // ---- Public Parameters ----
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Health { get => health ; set => health = value; }
        public float Speed { get => speed ; set => speed = value; }
        public int JumpMax { get => jumpMax ; set => jumpMax = value; }
        public float JumpSoeed { get => jumpSpeed; set => jumpSpeed = value; }
        public float Gravity { get => gravity ; set => gravity = value; }
        public int SprintModifier { get => sprintModifier ; set => sprintModifier = value; }
        public float InteractionDistance { get => interactionDistance ; set => interactionDistance = value; }
        public bool IsOccupied { get => isOccupied; set => isOccupied = value; }
        public int HeldWeaponIndex { get; private set; }
        public int MineCount { get => mineCount; set => mineCount = value; }
        public int GrenadeCount { get => grenadeCount; set => grenadeCount = value; }
        public int ThrowForce { get =>  throwForce; set => throwForce = value; }
        #endregion

        #region PLAYER_PROPERTIES
        // ---- Basic Parameters ----
        [Header(" ---- Player Properties ---- ")]
        [SerializeField] protected int maxHealth;
        [SerializeField] protected int health;
        [SerializeField] protected float speed;
        [SerializeField] protected int jumpMax;
        [SerializeField] protected float jumpSpeed;
        [SerializeField] protected float gravity;
        [SerializeField] protected int sprintModifier;
        #endregion

        #region INTERACTION_PROPERTIES
        [Header(" ---- Interaction ---- ")]
        [SerializeField] private LayerMask interactibleLayer;
        private RaycastHit interactHit;
        private float interactionDistance;
        private bool isOccupied;
        private bool occupiedPrompt;
        #endregion

        #region WEAPONS_&_INVENTORY
        [Header(" ---- Weapons & Inventory ---- ")]
        [SerializeField] private GameObject currentWeapon;
        [SerializeField] private GameObject mine, turret, grenade;
        [SerializeField] private List<GameObject> weapons = new List<GameObject>();
        [SerializeField] private UndeadWarfare.Player.Crafting.Inventory playerInventory;
        [SerializeField] private int mineCount;
        [SerializeField] private int grenadeCount;
        [SerializeField] private int throwForce = 10;
        private int mineCountMax;
        private int grenadeCountMax;
        private Transform shotPosition;
        #endregion

        #region CAMERA_COMPONENTS
        [Header(" ---- Camera Components ---- ")]
        [Tooltip("Controls the player's view port.")]
        public PlayerViewController playerView;
        #endregion

        #region LIFECYCLE_METHODS
        // ---- Lifecycle Methods ----
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (gamemanager.instance.isPaused) { return; }          // Skip update if game is paused

        }
        #endregion
    }
}
