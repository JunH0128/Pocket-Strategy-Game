using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI finalWaveText;
    [SerializeField] private UnityEngine.UI.Button restartButton;

    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner;

    
    // Start is called before the first frame update
    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        BaseHealth.onBaseDestroyed.AddListener(ShowGameOver);

        if (restartButton != null)
        {
            
            {
                restartButton.onClick.AddListener(RestartGame);
                
            }
        }
        
    }

    private void OnDestroy()
    {
        BaseHealth.onBaseDestroyed.RemoveListener(ShowGameOver);
    }

    private void ShowGameOver()
    {
        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverText != null)
        {
            gameOverText.text = "Game Over!";
        }

        if (finalWaveText != null && enemySpawner != null)
        {
            finalWaveText.text = "You reached Wave: " + enemySpawner.GetCurrentWave();
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        
    }

   
}
