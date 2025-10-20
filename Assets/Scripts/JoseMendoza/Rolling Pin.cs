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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;

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

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        if (audioSource != null && swordSwing != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(swordSwing);
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
        // Add animation logic here if needed
    }
}
