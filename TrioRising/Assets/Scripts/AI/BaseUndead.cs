using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UndeadWarfare.AI.Type;
using System;
using UndeadWarfare.AI;
using Unity.VisualScripting;

namespace UndeadWarfare.AI
{
    public class BaseUndead : BaseAI, UndeadWarfare.AI.IDamage
    {
        #region BASIC_STATS_&_PROPERTIES
        // ---- Basic Stats ----
        public int Health { get => health; set => health = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
        public float AttackDelay { get => attackDelay; set => attackDelay = value; }
        public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }
        public int DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public float MovementSpeedMultiplier { get => movementSpeedMultiplier; set => movementSpeedMultiplier = value; }
        public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public int AttackTimer { get => attackDamage; set => attackDamage = value; }
        public bool IsFleeing { get => isFleeing; set => isFleeing = value; }
        #endregion

        #region BASE_UNDEAD 
        [Header(" ---- Base Undead ---- ")]
        #region RELEVANT_TYPES
        public UndeadType Type;
        public string AttackType;
        #endregion

        #region STATE_&_CONTROL
        // ---- State & AI Control ----
        public BaseAIState CurrentState;        // Current state of the object
        public EnemyState CurrentStateName;     // Name of the current state
        public AttackArea AttackArea;           // Area where the AI can attack
        public AttackArea AreaOfEffect;         // Area for larger area-based effects
        #endregion

        #region PROTECTED_FIELDS
        // ---- Protected Fields ----
        [SerializeField] protected int health;
        [SerializeField] protected int attackDamage;
        [SerializeField] protected float attackDelay;
        [SerializeField] protected float attackCooldown;
        [SerializeField] protected int damageMultiplier;
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected float movementSpeedMultiplier;
        [SerializeField] protected bool isAttacking;
        [SerializeField] protected bool isAvailable;
        [SerializeField] protected float attackTimer;
        [SerializeField] protected bool isFleeing;
        [SerializeField] protected Vector3 maxDistance;
        #endregion

        #region ATTACK_PHASE_PROPERTIES
        // ---- Attack Phase ----
        public enum AttackPhases { IDLE, PRIMED, Attack, RECOVERY }     // Enum definitons
        public AttackPhases AttackPhase;        // Current Attack Phase
        #endregion

        #region STATUSES_&_EFFECTS
        // ---- Statuses & Effects ----
        public GameObject HotSpot;              // Weakpoint for critical hit effects
        public GameObject HitEffect;            // Effect on hit
        public GameObject DeathEffect;          // Effect on Death
        public Transform shootPosition;         // Position for shooting projectiles
        public bool isGrounded;                 // Is AI Grounded at the moment
        #endregion

        #region CACHED_COMPONENTS_&_VARIABLES
        // ---- Cached Components & Variables ----
        int orginalHealth;         // Original HP
        Collider col;
        Vector3 launchSpot;
        int originalDamage;         // Orginal Attack Damage
        float originalSpeed;        // Original Attack Speed
        float lastTimestamp;             // Timer for status effects
        float lastApplication;            // Timer for effect Application
        protected Rigidbody rb;         // RigidBody for physics interactions
        #endregion
        #endregion

        #region LIFECYCLE_METHODS
        void Start()
        {
            InitializeUndead();
            lastTimestamp = Time.deltaTime;
            UpdateState(new RegisterState(this));
        }
        void Update()
        {
            if (IsTargetVisible)
            {

            }
            if (!isGrounded) { return; }
            if (CurrentState == null)
            {
                throw new Exception("NO CORRESPONDING STATE");
            }

        }
        #endregion

        #region INITIAL_CONFIGURATION_&_STATE_HANDLING
        // Initial State and configuration
        public void InitializeUndead()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<Collider>();
            navAgent.speed = movementSpeed;
            Target = gamemanager.instance.player.gameObject;
            CacheStartingValues();
        }

        // Caches the starting values of relevant variables
        public void CacheStartingValues()
        {
            originalDamage = attackDamage;
            originalSpeed = movementSpeed;
            isGrounded = true;
            orginalHealth = health;
            isAvailable = true;
            StoppingDistanceOriginal = NavAgent.stoppingDistance;
        }

        // Updates the AI State
        public void UpdateState(BaseAIState aiState)
        {
            CurrentState = aiState;
            CurrentStateName = aiState.Name;
        }
        #endregion

        #region INTERFACE_METHODS
        // ---- IDamage interface methods ----

        public void TakeDamage(int _amount, GameObject _source)
        {

        }
        public void TakeDamage(int _amount, GameObject _source, bool _weakspot)
        {

        }
        public void TakeDamage(int _amount, Vector3 _impulsePosition, GameObject _source = null, bool _weakspot = false)
        {
            _amount *= DamageMultiplier;
            health -= _amount;

            if (_amount > 0)
            {
                if (CurrentStateName == EnemyState.Dead) { return; }        // If AI state is DEAD, do nothing
            }
        }
        #endregion
    }
}