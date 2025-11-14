using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider _healthbarSlider;
    [SerializeField] private Image damageImage;
    [SerializeField] private Image healingImage;
    [SerializeField] private Image fadeImage;

    public int maxHealth = 100;
    public int currentHealth = 100;

    public float flashDuration = 0.5f;
    public string sceneToLoad = "GameOverScene";

    void Start()
    {
        _healthbarSlider.value = currentHealth;

        if (damageImage != null) damageImage.gameObject.SetActive(false);
        if (healingImage != null) healingImage.gameObject.SetActive(false);

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false);
            fadeImage.color = new Color(0, 0, 0, 0);
        }
    }

    public void UpdateHealthBar(int currentHealth)
    {
        _healthbarSlider.value = currentHealth;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(FadeToRedAndLoadScene());
        }
    }

    public void DealDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Clamp to 0
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthBar(currentHealth);

        StartCoroutine(DamageFlashEffect());
    }

    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;

        // Clamp to maxHealth
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar(currentHealth);

        StartCoroutine(HealingFlashEffect());
    }

    private IEnumerator DamageFlashEffect()
    {
        if (damageImage != null)
        {
            damageImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            damageImage.gameObject.SetActive(false);
        }
    }

    private IEnumerator HealingFlashEffect()
    {
        if (healingImage != null)
        {
            healingImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(flashDuration);
            healingImage.gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeToRedAndLoadScene()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);

            Color startColor = fadeImage.color;
            for (float t = 0; t <= 1f; t += Time.deltaTime / flashDuration)
            {
                fadeImage.color = Color.Lerp(startColor, Color.red, t);
                yield return null;
            }

            fadeImage.color = Color.red;

            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
