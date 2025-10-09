using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float gameTime = 60f; // Total time in seconds for the game
    private float timeRemaining;
    private int enemiesCaught = 0;
    private int totalEnemies;
    private bool gameEnded = false;

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
        // Find all enemies with EnemyFlee script in the scene
        EnemyFlee[] enemies = FindObjectsOfType<EnemyFlee>();
        totalEnemies = enemies.Length;
        timeRemaining = gameTime;
    }

    void Update()
    {
        if (gameEnded) return;

        // Update timer
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            gameEnded = true;
            GameUI.Instance?.ShowLoseScreen();
        }
    }

    public void EnemyCaught()
    {
        if (gameEnded) return;

        enemiesCaught++;
        if (enemiesCaught >= totalEnemies)
        {
            gameEnded = true;
            GameUI.Instance?.ShowWinScreen();
        }
    }

    public int GetEnemiesCaught()
    {
        return enemiesCaught;
    }

    public int GetTotalEnemies()
    {
        return totalEnemies;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}