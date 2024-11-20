using System.Collections;
using System.Collections.Generic;
using UndeadWarfare.AI;
using UndeadWarfare.Interact;
using UnityEngine;

namespace UndeadWarfare.Player
{
    public class PlayerController : MonoBehaviour, IDamage
    {
        #region PUBLIC_PROPERTIES
        // ---- Public Parameters ----
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Health { get => health ; set => health = value; }
        public float InteractionDistance { get => interactionDistance ; set => interactionDistance = value; }
        public Vector3 Forward { get => forward ; set => forward = value; }
        public Vector3 Origin { get => origin ; set => origin = value; }
        public bool IsOccupied { get => isOccupied; set => isOccupied = value; }
        public int HeldWeaponIndex { get; private set; }
        public int MineCount { get => mineCount; set => mineCount = value; }
        public int GrenadeCount { get => grenadeCount; set => grenadeCount = value; }
        public int ThrowForce { get =>  throwForce; set => throwForce = value; }
        #endregion

        #region PLAYER_PROPERTIES
        // ---- Basic Parameters ----
        [Header(" ---- Player Properties ---- ")]
        [SerializeField] protected int maxHealth;           // Maximum health value
        [SerializeField] protected int health;              // Health 
        #endregion

        #region INTERACTION_PROPERTIES
        [Header(" ---- Interaction ---- ")]
        [SerializeField] private LayerMask interactable = 1 << 0;           // Interactable layer mask
        [SerializeField] Transform interactPoint;
        private RaycastHit interactHit;         // Stores raycast hit data for interactions
        private float interactionDistance;      // Distance for interaction ray
        private Vector3 forward;        // Direction of the player's forward view
        private Vector3 origin;         // Origin of the player's view ray                                    
        private bool isOccupied;        // Flag for occupied interaction state
        private bool occupiedPrompt;    // Flag to show/hide interaction prompt
        #endregion

        #region WEAPONS_&_INVENTORY
        [Header(" ---- Weapons & Inventory ---- ")]
        [SerializeField] Transform ViewModel;       // Player's view model
        [SerializeField] Animator ViewModelAnimator;        // Animator for view model
        [SerializeField] private GameObject currentWeapon;      // Player's current weapon
        [SerializeField] private GameObject mine;       // Deployable item 
        [SerializeField] private GameObject grenade;        // Throwable item
        [SerializeField] private List<GameObject> Weapons = new List<GameObject>();         // List of weapons a player can use
        [SerializeField] private UndeadWarfare.Player.Crafting.Inventory playerInventory;       // Player's inventory
        [SerializeField] private int mineCount;         // Deployable item count
        [SerializeField] private int grenadeCount;      // Throwable item count 
        [SerializeField] private int throwForce = 10;   // Force applied to thrown items 
        //public int HeldWeaponIndex;
        public GameObject HeldWeapon => Weapons[HeldWeaponIndex];       // Reference to currently held weapon
        private int mineCountMax;       // Maximum number of deployable items
        private int grenadeCountMax;        // Maximum number of throwable items 
        #endregion

        #region CAMERA_COMPONENTS
        [Header(" ---- Camera Components ---- ")]
        [Tooltip("Controls the player's view port.")]
        public PlayerViewController playerView;
        #endregion

        #region PARTICLE_&_AUDIO
        [Header(" ---- Particle & Audio ---- ")]
        public ParticleSystem Particles;
        private Transform shotPosition;     // Origin of the shot in the view model
        public AudioSource Audio;       // Audio source for sound effect
        public AudioClip[] FootstepSounds;      // Array of footstep sounds
        public float FootstepVolume;        // Volume of footstep sounds
        #endregion

        #region LIFECYCLE_METHODS
        // ---- Lifecycle Methods ----
        // Start is called before the first frame update
        void Start()
        {
            InitializePlayer();
        }

        // Update is called once per frame
        void Update()
        {
            if (gamemanager.instance.isPaused) { return; }          // Skip update if game is paused

        }
        
        private void FixedUpdate()
        {

        }
        #endregion

        private void InitializePlayer()
        {
            Health = MaxHealth;
            interactionDistance = Vector3.Distance(ViewModel.position, interactPoint.position);
            SpawnPlayer();
        }
        // Spawns the player at a specified point
        private void SpawnPlayer() { transform.position = gamemanager.instance.playerSpawnPOS.transform.position; }        // Set the spawn 
        // Handles playeer interaction with objects in the world
        private void UpdateInteraction()
        {
            // Relapse interaction whne button is no longer pressed
            if(!Input.GetButton("Interact"))            
                IsOccupied = false;
            // Stop if occupied with another interaction
            if (IsOccupied) { return; }
            // Perform a raycast to check for interactable objects
            if (Physics.Raycast(ViewModel.transform.position, ViewModel.transform.forward, out interactHit, interactionDistance, interactable))
                if (interactHit.collider.gameObject.TryGetComponent<InteractableObject>(out var o))
                {
                    // Show prompt if interact
                    gamemanager.instance.PromptBackground.SetActive(true);
                    if (!occupiedPrompt)
                        o.Prompt("To Interact");
                }
        }
        // Load and displays the player's active weapon
        public void LoadViewModel()
        {

        }
        // Updates player's weapon status and HUD
        private void UpdateWeapons()
        {

        }
        // Player takes damage from enemy attacks
        public void TakeDamage(int _amount, GameObject _source)
        {

        }
    }
}
