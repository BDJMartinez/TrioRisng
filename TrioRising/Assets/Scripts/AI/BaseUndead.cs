using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UndeadWarfare.AI.States;
using System;

namespace UndeadWarfare.AI
{
    public class BaseUndead : BaseAI, UndeadWarfare.AI.IDamage
    {
        // ---- Basic Stats ----
        public int Health { get => health; set => health = value; }
        public int AttackDamage { get; set; }
        public float AttackDelay { get; set; }
        public float AttackCooldown { get; set; }
        public float MovementSpeed { get; set; } 
        public bool IsAttacking { get; set; }
        public bool IsFree { get; set; }
        public float AttackTimer { get; set; }
        public bool IsFleeing { get; set; }

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
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected bool isAttacking;
        [SerializeField] protected bool isFree;
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
        public bool onLand;

        // ---- Cached Components & Variables ----
        int orginalHealth;         // Original HP
        Collider col;
        Vector3 launchSpot;         
        int originalDamage;         // Orginal Attack Damage
        float originalSpeed;        // Original Attack Speed
        float lastTick;             // Timer for status effects
        float lastApply;            // Timer for effect Application
        protected Rigidbody rb;     

        void Start()
        {
            InitializeUndead();
            UpdateState(new RegisterState(this));
        }
        void Update()
        {
            if (IsTargetVisible)
            {

            }

            if (!onLand) { return; }
            if (CurrentState == null)
            {
                throw new Exception("NO CORRESPONDING STATE");
            }

        }
        // Initial State and configuration
        public void InitializeUndead()
        {
            rb = GetComponent<Rigidbody>();
            originalDamage = attackDamage;
            originalSpeed = movementSpeed;
            onLand = true;
            lastTick = Time.deltaTime;
            col = GetComponent<Collider>();
            orginalHealth = health;
            isFree = true;
            navAgent.speed = movementSpeed;
            Target = gamemanager.instance.player.gameObject;
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
            throw new System.NotImplementedException();
        }
    }
}