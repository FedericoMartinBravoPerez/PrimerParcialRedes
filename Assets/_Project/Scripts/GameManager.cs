using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;


public class GameManager : NetworkBehaviour
{
    



    /*
    [SerializeField] private int _minPlayersToStart = 2;
    public static GameManager Instance { get; private set; }

    
    public event Action OnPause;
    public event Action OnResume;
    public event Action OnGameStart;
    public event Action OnGameEnd;
    

    private bool _gameStarted = false;
    private int _currentPlayerCount = 0;
    
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        DebugEvents();
        OnGameStart += OnGameStartMethods;
    }


    private void Start()
    {
        OnPause.Invoke();
    }

    public override void FixedUpdateNetwork()
    {
        if (_gameStarted) return;

        if(Runner.SessionInfo.PlayerCount >= _minPlayersToStart)
        {
            OnGameStart.Invoke();
            OnResume.Invoke();
        }
    }


    private void OnGameStartMethods()
    {
        _gameStarted = true;
        _currentPlayerCount = Runner.ActivePlayers.Count();
    }

    public void PlayerDeath()
    {
        _currentPlayerCount--;
        if (_currentPlayerCount <= 0)
        {
            OnGameEnd.Invoke();
        }
    }
    
    
    
    private void DebugEvents()
    {
        OnPause += () => Debug.Log("OnPause");
        OnResume += () => Debug.Log("OnResume");
        OnGameStart += () => Debug.Log("OnGameStart");
        OnGameEnd += () => Debug.Log("OnGameEnd");
    }
    */
}
