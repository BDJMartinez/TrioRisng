using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UndeadWarfare.AI.Type;
using System;

namespace UndeadWarfare.AI
{
    public class BaseUndead : BaseAI, UndeadWarfare.AI.IDamage
    {
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

        [Header(" ---- Base Undead ---- ")]
        public UndeadType Type;
        public string AttackType;

        // ---- State & AI Control ----
        public BaseAIState CurrentState;        // Current state of the object
        public EnemyState CurrentStateName;     // Name of the current state
        public AttackArea AttackArea;           // Area where the AI can attack
        public AttackArea AreaOfEffect;         // Area for larger area-based effects

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

        // ---- Attack Phase ----
        public enum AttackPhases { IDLE, PRIMED, Attack, RECOVERY }     // Enum definitons
        public AttackPhases AttackPhase;        // Current Attack Phase

        // ---- Statuses & Effects ----
        public GameObject HitEffect;            // Effect on hit
        public GameObject DeathEffect;          // Effect on Death
        public Transform shootPosition;         // Position for shooting projectiles
        public bool isGrounded;                 // Is AI Grounded at the moment

        // ---- Cached Components & Variables ----
        int orginalHealth;         // Original HP
        Collider col;
        Vector3 launchSpot;
        int originalDamage;         // Orginal Attack Damage
        float originalSpeed;        // Original Attack Speed
        float lastTimestamp;             // Timer for status effects
        float lastApplication;            // Timer for effect Application
        protected Rigidbody rb;

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
        public void TakeDamage(int amount, Vector3 impulsePosition, GameObject source = null, bool weakspot = false)
        {
            amount *= DamageMultiplier;
            health -= amount;

            if (amount > 0)
            {
                if (CurrentStateName == EnemyState.Dead) { return; }        // If AI state is DEAD, do nothing
            }
        }
    }
}