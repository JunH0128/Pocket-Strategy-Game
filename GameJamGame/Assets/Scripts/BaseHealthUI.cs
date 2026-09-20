using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseHealthUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        BaseHealth.onHealthChanged.AddListener(UpdateHealthUI);
        BaseHealth.onBaseDestroyed.AddListener(OnBaseDestroyed);
    }

    private void OnDisable()
    {
        BaseHealth.onHealthChanged.RemoveListener(UpdateHealthUI);
        BaseHealth.onBaseDestroyed.RemoveListener(OnBaseDestroyed);
    }


    private void Start()
    {
        BaseHealth baseHealth = FindFirstObjectByType<BaseHealth>();
        if (baseHealth != null)
        {
            UpdateHealthUI(baseHealth.GetCurrentHealth(), baseHealth.GetMaxHealth());
        }
    }

    private void UpdateHealthUI(int current, int max)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = max;
            healthSlider.value = current;
        }

        if (healthText != null)
        {
            healthText.text = current.ToString();
        }
    }

    private void OnBaseDestroyed()
    {
        if (healthText != null)
        {
            healthText.text = "Base Destroyed!";
        }
    }
}
