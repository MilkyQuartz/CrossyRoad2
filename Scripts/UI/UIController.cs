using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public GameObject gameOverUI;
    public TextMeshProUGUI scoreNum;
    public TextMeshProUGUI bestScoreNum;
    public TextMeshProUGUI appleNum;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateUI(0f, PlayerPrefs.GetFloat("HighScore", 0f), 0);
        GameManager.OnTimeUpdated += UpdateUI;
    }

    public void StartGameBtn()
    {
        GameManager.Instance.StartGame();
    }

    public void RetryGameBtn()
    {
        GameManager.Instance.RetryGame();
    }

    public void UpdateUI(float elapsedTime, float highScore, int appleCount)
    {
        scoreNum.text = elapsedTime.ToString("F2");
        bestScoreNum.text = highScore.ToString("F2");
        appleNum.text = appleCount.ToString();
    }

    private void OnDestroy()
    {
        GameManager.OnTimeUpdated -= UpdateUI;
    }

    public void ShowGameOverUI(float elapsedTime, float highScore, int appleCount)
    {
        Time.timeScale = 0.0f;
        gameOverUI.SetActive(true);
        UpdateUI(elapsedTime, highScore, appleCount); 
    }

    public void GameOverUIOut()
    {
        Time.timeScale = 1.0f;
        gameOverUI.SetActive(false);
    }
}
