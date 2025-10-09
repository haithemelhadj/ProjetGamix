using UnityEngine;
using TMPro; // For TextMeshProUGUI
// If not using TextMeshPro, comment out the above and uncomment the next line:
// using UnityEngine.UI;

public class WaveCounterUI : MonoBehaviour
{
    public WaveSpawner waveSpawner; // Reference to the WaveSpawner script
    private TextMeshProUGUI waveText; // TextMeshPro UI element
    // If not using TextMeshPro, comment out the above and uncomment the next line:
    // private Text waveText;

    void Start()
    {
        // Get the TextMeshProUGUI component (or Text for legacy UI)
        waveText = GetComponent<TextMeshProUGUI>();
        // If not using TextMeshPro, use this instead:
        // waveText = GetComponent<Text>();

        if (waveSpawner == null)
        {
            waveSpawner = FindObjectOfType<WaveSpawner>();
        }

        UpdateWaveText();
    }

    void Update()
    {
        UpdateWaveText();
    }

    void UpdateWaveText()
    {
        if (waveSpawner != null && waveText != null)
        {
            int currentWave = waveSpawner.GetCurrentWave() + 1; // 1-based for display
            int totalWaves = waveSpawner.GetTotalWaves();
            waveText.text = $"Wave {currentWave}/{totalWaves}";
        }
    }
}