using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference attackInput;

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    private AudioSource audioSource;
    private Camera mainCamera;

    private bool attacking = false;
    private bool readyToAttack = true;
    private int attackCount = 0;  // Track the attack count
    private Rigidbody playerRigidbody; // Reference to the player's Rigidbody for velocity tracking

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        playerRigidbody = GetComponentInParent<Rigidbody>(); // Assuming the player is the parent of the weapon object

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
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
        }
    }

    void HitTarget(Vector3 pos)
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(hitSound);
        }

        if (hitEffect != null)
        {
            GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
            Destroy(GO, 2f);
        }
    }

    void SetAnimations()
    {
        // If player is not attacking
        if (!attacking)
        {
            Vector3 playerVelocity = playerRigidbody.velocity; // Get the player's velocity

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
        Animator animator = GetComponent<Animator>();
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
