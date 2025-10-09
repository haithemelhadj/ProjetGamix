using UnityEngine;
using TMPro; // For TextMeshProUGUI
// If not using TextMeshPro, comment out the above and uncomment the next line:
// using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance { get; private set; }

    public TextMeshProUGUI counterText; // UI for caught enemies counter
    public TextMeshProUGUI timerText; // UI for timer
    public GameObject winScreen; // Win screen panel
    public GameObject loseScreen; // Lose screen panel
    // If not using TextMeshPro, comment out the above TextMeshProUGUI lines and uncomment these:
    // public Text counterText;
    // public Text timerText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Ensure win/lose screens are hidden at start
        if (winScreen != null) winScreen.SetActive(false);
        if (loseScreen != null) loseScreen.SetActive(false);

        UpdateCounterText();
        UpdateTimerText();
    }

    void Update()
    {
        UpdateCounterText();
        UpdateTimerText();
    }

    void UpdateCounterText()
    {
        if (counterText != null && GameManager.Instance != null)
        {
            int caught = GameManager.Instance.GetEnemiesCaught();
            int total = GameManager.Instance.GetTotalEnemies();
            counterText.text = $"Caught: {caught}/{total}";
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null && GameManager.Instance != null)
        {
            float time = GameManager.Instance.GetTimeRemaining();
            int seconds = Mathf.CeilToInt(time);
            timerText.text = $"Time: {seconds}";
        }
    }

    public void ShowWinScreen()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f; // Pause game
        }
    }

    public void ShowLoseScreen()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f; // Pause game
        }
    }
}