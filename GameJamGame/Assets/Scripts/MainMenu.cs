using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [Header("Selection Arrow")]
    [SerializeField] private RectTransform selectionArrow;
    [SerializeField] private float arrowOffset = -50f;

    [Header("Settings")]
    [SerializeField] private string gameSceneName = "MainScene";

    private void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        
        // Setup hover events
        AddHoverEvent(playButton);
        AddHoverEvent(quitButton);
    }

    private void AddHoverEvent(Button button)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
        
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { MoveArrowToButton(button); });
        trigger.triggers.Add(entry);
    }

    // Specified arrow movement
    private void MoveArrowToButton(Button button)
    {
        Vector3 newPosition = button.transform.position;
        newPosition.x += arrowOffset;
        selectionArrow.position = newPosition;
    }

    public void PlayGame() => SceneManager.LoadScene(gameSceneName);
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}