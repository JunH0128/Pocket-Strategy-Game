using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveUIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner; // Reference to the EnemySpawner script
    [SerializeField] private Button startWaveButton; // Reference to the start wave button
    [SerializeField] private TextMeshProUGUI waveText; // Reference to the wave text UI element
    [SerializeField] private TextMeshProUGUI buttonText; // Reference to the button text UI element

    private void Start()
    {
        
        if (startWaveButton != null)
        {
            startWaveButton.onClick.AddListener(OnStartWaveClicked);
        }
        
        UpdateUI();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateUI();
    }

    // Called when the start wave button is clicked
    private void OnStartWaveClicked()
    {
        if (enemySpawner != null)
        {
            enemySpawner.StartNextWave();
        }
    }


    // Updates the UI elements based on the current wave state
    private void UpdateUI()
    {
        if (enemySpawner == null) return;

       
        if (waveText != null)
        {
            if (enemySpawner.IsWaitingToStart())
            {
                waveText.text = "Wave " + enemySpawner.GetCurrentWave();
            }
            else
            {
                waveText.text = "Wave " + enemySpawner.GetCurrentWave();
            }
        }

        // Update start wave button visibility and text
        if (startWaveButton != null)
        {
            startWaveButton.gameObject.SetActive(enemySpawner.IsWaitingToStart());
            
            
            if (buttonText != null)
            {
                buttonText.text = "Start Wave ";
            }
        }
    }
}