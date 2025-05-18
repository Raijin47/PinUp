using UnityEngine;
using System;

[Serializable]
public class Timer
{
    public event Action OnCompleted;

    public float CurrentTime { get; private set; }

    public bool IsCompleted { get; private set; }

    public float RequiredTime { private get; set; }

    public Timer(float timer)
    {
        RequiredTime = timer;
        CurrentTime = RequiredTime;
    }

    public void Update()
    {
        if (IsCompleted) return;

        if (CurrentTime > 0f)
        {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime <= 0f)
            {
                CurrentTime = 0;
                IsCompleted = true;
                OnCompleted?.Invoke();
            }
        }
    }

    public void RestartTimer()
    {
        CurrentTime = RequiredTime;
        IsCompleted = false;
    }
}