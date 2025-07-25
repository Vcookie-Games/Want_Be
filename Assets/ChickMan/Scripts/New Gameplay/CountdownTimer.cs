using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 10f;
    [SerializeField] private float speed = 1f;

    private float currentSpeed;

    public GameObject player;
    public TextMeshProUGUI timerText;
    [SerializeField] private bool isRunning = false;

    private int coinMultiplier = 1;

    void Start()
    {
        currentSpeed = speed;
    }
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
        UpdateTimerText();
        isRunning = false;
        if (player != null)
            player.SetActive(true);
    }

    private void Update()
    {
        if (!isRunning) return;

        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime * currentSpeed;
            if (timeRemaining < 0f)
                timeRemaining = 0f;

            UpdateTimerText();
        }
        else
        {
            isRunning = false;
            timerText.text = "Time's up!";
            GameController.Instance.GameOver();
            if (player != null)
                player.SetActive(false);
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = Mathf.Ceil(timeRemaining).ToString() + "s";
    }

    // Thêm time vào bộ đếm 
    public void AddTime(float time)
    {
        timeRemaining += time * coinMultiplier;
        UpdateTimerText();
    }
    public void SetCoinMultiplier(int multiplier)
    {
        if (coinMultiplier > 3 && coinMultiplier < 1) return;
        coinMultiplier = multiplier;
    }
    public void resetCoinMultiplier()
    {
        coinMultiplier = 1;
    }
    // Đổi tốc độ
    public void SetSpeedTimer(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    public void ResetTimer()
    {
        currentSpeed = speed;
    }
}
