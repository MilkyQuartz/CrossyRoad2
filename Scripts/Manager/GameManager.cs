using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameCheck = false;
    public bool isStartCheck = false;
    private float gameStartTime;
    private float elapsedTime = 0f;
    private float highScore = 0f;
    private AudioSource audioSource;

    public static event Action<float, float, int> OnTimeUpdated;

    private void Awake()
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
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        gameStartTime = Time.time;
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
    }

    private void Update()
    {
        Time.timeScale = isStartCheck ? 1.0f : 0.0f;
        elapsedTime = Time.time - gameStartTime;

        OnTimeUpdated?.Invoke(elapsedTime, highScore, GetAppleCount());

        // 게임 오버
        if (isGameCheck)
        {
            audioSource.Play();
            SavePlayerData();
            StartCoroutine(StopTimeAfterDelay(0.7f));
            isGameCheck = false;
        }
    }

    IEnumerator StopTimeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isStartCheck = false;

        if (elapsedTime > highScore)
        {
            highScore = elapsedTime;
            SavePlayerData();
        }

        UIController.Instance.ShowGameOverUI(elapsedTime, highScore, GetAppleCount());

        yield return null;
    }

    public void StartGame()
    {
        isStartCheck = true;
        gameStartTime = Time.time;
        elapsedTime = 0f;
        UIController.Instance.UpdateUI(elapsedTime, highScore, GetAppleCount());
        SceneManager.LoadScene("MainScene");
    }

    public void RetryGame()
    {
        UIController.Instance.GameOverUIOut();
        isStartCheck = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameStartTime = Time.time;
        elapsedTime = 0f;

        UIController.Instance.UpdateUI(elapsedTime, highScore, GetAppleCount());
    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetFloat("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public int GetAppleCount()
    {
        return PlayerPrefs.GetInt("AppleCount", 0);
    }

    public void AddAppleCount(int amount)
    {
        int currentAppleCount = GetAppleCount();
        currentAppleCount += amount;
        PlayerPrefs.SetInt("AppleCount", currentAppleCount);
        PlayerPrefs.Save();
    }
}
