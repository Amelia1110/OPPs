using System;
using Unity.Services.Vivox;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TimerScript : MonoBehaviour

{
    private Text text;
    public int setTime;
    private float remainingTime;
    private bool timerStopped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameLogic logic;
    void Start()
    {
        text = GetComponent<Text>();
        remainingTime = setTime;
        logic = FindFirstObjectByType<Grid>().GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerStopped)
        {
            remainingTime -= Time.deltaTime;
        }
        if (remainingTime <= 0 && !timerStopped)
        {
            timerStopped = true;
            remainingTime = 0;
            OnTimerEnd();
        }
        if (!logic.gameActive)
        {
            timerStopped = true;
        }
        text.text = ((int)remainingTime).ToString();
        if (Keyboard.current.rKey.wasReleasedThisFrame)
        {
            Reset();
        }
    }

    void OnTimerEnd()
    {
        logic.OnTimerExpire();
    }

    void Reset()
    {
        remainingTime = setTime;
        timerStopped = false;
    }
}
