using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10f;
    public GameObject player;
    public TextMeshProUGUI timerText;
    public bool isRunning = false;

    public void StartTimer()
    {
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        timerText.text = timeRemaining.ToString();
    }

    void Update()
    {
        if (isRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timeRemaining).ToString() + "s";
        }
        if (timeRemaining <= 0)
        {
            isRunning = false;
            timerText.text = "Time's up!";
            player.SetActive(false); // Disable player when time is up
        }
    }

    public void AddTime(float time)
    {
        timeRemaining += time;

    }
}
