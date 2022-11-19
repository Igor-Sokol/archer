using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer : Singleton<Timer>
{
    private List<TimerCounter> _timerCounters;
    
    [SerializeField] private int startCounters;

    private void Awake()
    {
        _timerCounters = new List<TimerCounter>();

        for (int i = 0; i < startCounters; i++)
        {
            AddTimerCounter();
        }
    }

    private void Update()
    {
        foreach (var timerCounter in _timerCounters)
        {
            timerCounter.UpdateCounter(Time.deltaTime);
        }
    }

    public void AddTimer(float seconds, Action callback)
    {
        TimerCounter counter = GetTimerCounter();
        counter.OnTime += callback;
        counter.Start(seconds);
    }

    private TimerCounter GetTimerCounter()
    {
        TimerCounter counter = null;

        foreach (var timerCounter in _timerCounters)
        {
            if (!timerCounter.IsWorking)
            {
                counter = timerCounter;
            }
        }

        if (counter is null)
        {
            counter = AddTimerCounter();
        }

        return counter;
    }
    
    private TimerCounter AddTimerCounter()
    {
        TimerCounter counter = new TimerCounter();
        _timerCounters.Add(counter);

        return counter;
    }
}

public class TimerCounter
{
    private float _secondsLeft;
    private Action _onTime;
    private bool _isWorking;

    public bool IsWorking => _isWorking;
    
    public event Action OnTime
    {
        add => _onTime = (Action)Delegate.Combine(_onTime, value);
        remove => _onTime = (Action)Delegate.Remove(_onTime, value);
    }

    public void Start(float seconds)
    {
        _secondsLeft = seconds;
        _isWorking = true;
    }
    
    private void Reset()
    {
        _isWorking = false;
        _onTime = null;
    }
    
    public void UpdateCounter(float deltaTime)
    {
        if (!_isWorking) return;

        _secondsLeft -= deltaTime;

        if (_secondsLeft <= 0)
        {
            _onTime?.Invoke();
            Reset();
        }
    }
}