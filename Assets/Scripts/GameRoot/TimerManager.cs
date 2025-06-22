using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int minutesStart;
    [SerializeField] private int secondsStart;
    private float elapsedTime = 0f;     // This subtracts from startTime and only increases if game is not paused
    private bool isRunning = true;      // This pauses the game when choosing upgrades etc.

    // Have the timer start from the set start time
    void Start()
    {
        timerText.text = $"{minutesStart:00}:{secondsStart:00}";
    }

    // Update is called once per frame
    void Update()
    {
        // Freeze timer on GameOver
        if (!GameManager.Instance.isPlayerAlive)
        {
            return;
        }
        // Only increase elapsedTime when game is not paused
            if (isRunning)
            {
                elapsedTime += Time.deltaTime;
            }

        // Update timer
        float startTimeInSeconds = minutesStart * 60 + secondsStart;
        float timeLeft = startTimeInSeconds - elapsedTime;
        // Make sure timer doesn't go negative
        int minutes = Mathf.Max(Mathf.FloorToInt(timeLeft / 60f), 0);
        int seconds = Mathf.Max(Mathf.FloorToInt(timeLeft % 60f), 0);
        timerText.text = $"{minutes:00}:{seconds:00}";

        // GameOver if timer hits 0
        if (timeLeft <= 0f)
        {
            Debug.Log("Time's up! You lose!");
            GameManager.Instance.OnPlayerDeath();
        }
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}
