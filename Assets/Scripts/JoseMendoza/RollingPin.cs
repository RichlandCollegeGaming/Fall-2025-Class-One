using UnityEngine;
using UnityEngine.InputSystem;

public class RollingPin : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference attackInput;

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    private AudioSource audioSource;
    private Camera mainCamera;

    private bool attacking = false;
    private bool readyToAttack = true;
    private int attackCount = 0;  // Track the attack count

    // References to CharacterController and Animator
    [Header("References")]
    public CharacterController playerCharacterController;  // Reference to the player's CharacterController
    public Animator animator;

    public int attackDamage = 10; // Amount of damage to deal// Reference to the Animator

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;

        // Ensure playerCharacterController and animator are assigned
        if (playerCharacterController == null)
        {
            playerCharacterController = GetComponentInParent<CharacterController>(); // Assuming the player is the parent of the weapon object
            if (playerCharacterController == null) Debug.LogWarning("Player CharacterController not found.");
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null) Debug.LogWarning("Animator component not found.");
        }

        if (attackInput != null)
        {
            attackInput.action.Enable();
        }
        else
        {
            Debug.LogWarning("Attack input not assigned.");
        }
    }

    void Update()
    {
        if (attackInput != null && attackInput.action.WasPressedThisFrame())
        {
            Attack();
        }

        SetAnimations();
    }

    void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        // Reset attack logic after the attack speed
        Invoke(nameof(ResetAttack), attackSpeed);

        // Perform raycast and hit detection after the delay
        Invoke(nameof(AttackRaycast), attackDelay);

        // Play the sword swing sound
        if (audioSource != null && swordSwing != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(swordSwing);
        }

        // Alternate between attack animations
        if (attackCount == 0)
        {
            ChangeAnimationState("ATTACK1");
            attackCount++;
        }
        else
        {
            ChangeAnimationState("ATTACK2");
            attackCount = 0; // Reset to start over for the next cycle
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        // Perform the raycast to detect any hits within the attack distance
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            // If the hit object is on the attackLayer, check for EnemyAI and apply damage
            GameObject other = hit.collider.gameObject; // Get the hit object
            if (attackLayer == (attackLayer | (1 << other.layer))) // Check if the layer is in attackLayer
            {
                // Try to get the EnemyAI component
                EnemyAi enemy = other.GetComponent<EnemyAi>();

                // If an EnemyAI is found, call TakeDamage
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                }
                else
                {
                    Debug.LogWarning($"{other.name} is tagged 'Enemy' but has no EnemyAI component.");
                }
            }

            // Spawn hit effect at the point of impact
            if (hitEffect != null)
            {
                GameObject hitEffectInstance = Instantiate(hitEffect, hit.point, Quaternion.identity);
                Destroy(hitEffectInstance, 2f);
            }
        }
    }

    void SetAnimations()
    {
        // If player is not attacking
        if (!attacking)
        {
            Vector3 playerVelocity = playerCharacterController.velocity; // Get the player's velocity from CharacterController

            if (playerVelocity.x == 0 && playerVelocity.z == 0)
            {
                ChangeAnimationState("IDLE");
            }
            else
            {
                ChangeAnimationState("WALK");
            }
        }
    }

    // Method to switch between animation states (using Animator)
    void ChangeAnimationState(string newState)
    {
        // Ensure the Animator component is attached to the weapon
        if (animator != null)
        {
            animator.Play(newState);
        }
        else
        {
            Debug.LogWarning("Animator component not found on the Weapon.");
        }
    }
}
