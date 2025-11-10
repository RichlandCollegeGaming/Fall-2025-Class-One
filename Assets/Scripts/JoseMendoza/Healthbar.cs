using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this to use SceneManager

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider _healthbarSlider; // Reference to the slider UI component
    [SerializeField] private Image damageImage; // Reference to the UI Image that will flash on damage
    [SerializeField] private Image healingImage; // Reference to the UI Image that will flash on healing
    [SerializeField] private Image fadeImage; // New image that will fade to red on death
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float flashDuration = 0.5f; // Duration of the flash effect
    public string sceneToLoad = "GameOverScene"; // The scene to load when health reaches 0

    void Start()
    {
        // Initialize the slider's value to the current health
        _healthbarSlider.value = currentHealth;

        // Make sure all images are initially off (disabled)
        if (damageImage != null)
        {
            damageImage.gameObject.SetActive(false); // Disable the damage image at the start
        }

        if (healingImage != null)
        {
            healingImage.gameObject.SetActive(false); // Disable the healing image at the start
        }

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false); // Disable the fade image at the start
            fadeImage.color = new Color(0, 0, 0, 0); // Ensure it's fully transparent initially
        }
    }

    // Method to update the health bar's value
    public void UpdateHealthBar(float currentHealth)
    {
        _healthbarSlider.value = currentHealth; // Directly set the slider value to current health
    }

    void Update()
    {
        // Detect key press for damage (K key) and healing (H key)
        if (Input.GetKeyDown(KeyCode.K))
        {
            DealDamage(10f); // Damage value (10 can be adjusted as needed)
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10f); // Healing value (10 can be adjusted as needed)
        }

        // Check if health reaches 0 and trigger the fade effect + scene load
        if (currentHealth <= 0)
        {
            StartCoroutine(FadeToRedAndLoadScene());
        }
    }

    // Method to deal damage and update health
    public void DealDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Ensure health doesn't go below 0
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        // Update the health bar
        UpdateHealthBar(currentHealth);

        // Trigger damage flash effect
        StartCoroutine(DamageFlashEffect());
    }

    // Method to heal the player and update health
    public void Heal(float healingAmount)
    {
        currentHealth += healingAmount;

        // Ensure health doesn't exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Update the health bar
        UpdateHealthBar(currentHealth);

        // Trigger healing flash effect
        StartCoroutine(HealingFlashEffect());
    }

    // Coroutine to handle the damage flash effect
    private IEnumerator DamageFlashEffect()
    {
        if (damageImage != null)
        {
            // Enable the damage image (turn it on)
            damageImage.gameObject.SetActive(true);

            // Wait for the specified duration
            yield return new WaitForSeconds(flashDuration);

            // Disable the damage image (turn it off)
            damageImage.gameObject.SetActive(false);
        }
    }

    // Coroutine to handle the healing flash effect
    private IEnumerator HealingFlashEffect()
    {
        if (healingImage != null)
        {
            // Enable the healing image (turn it on)
            healingImage.gameObject.SetActive(true);

            // Wait for the specified duration
            yield return new WaitForSeconds(flashDuration);

            // Disable the healing image (turn it off)
            healingImage.gameObject.SetActive(false);
        }
    }

    // Coroutine to fade the fade image to red and load the scene
    private IEnumerator FadeToRedAndLoadScene()
    {
        if (fadeImage != null)
        {
            // Enable the fade image and start fading to red
            fadeImage.gameObject.SetActive(true);

            // Gradually change the color to red (fade effect)
            Color startColor = fadeImage.color;
            for (float t = 0; t <= 1f; t += Time.deltaTime / flashDuration)
            {
                fadeImage.color = Color.Lerp(startColor, Color.red, t);
                yield return null;
            }

            fadeImage.color = Color.red; // Ensure it ends as fully red

            // Wait for a moment before loading the scene
            yield return new WaitForSeconds(1f);

            // Load the scene by its name
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
