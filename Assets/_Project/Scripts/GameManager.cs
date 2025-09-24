using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;


public class GameManager : NetworkBehaviour
{
	[SerializeField] private GameObject _winImage;
	[SerializeField] private GameObject _defeatImage;    
    public static GameManager Instance { get; private set; }


	private List<PlayerRef> _clientsInGame;

    private void Awake()
    {
        Instance = this;
        _clientsInGame = new List<PlayerRef>();
    }

    public void SubscribeToClientsInGame(PlayerRef client)
    {
        
        if (_clientsInGame.Contains(client)) return;
        _clientsInGame.Add(client);
    }

    private void UnsubscribeToClientsInGame(PlayerRef client)
    {
        _clientsInGame.Remove(client);
    }
    
    [Rpc]
    public void RPC_PlayerDefeated(PlayerRef client)
    {
        if (client == Runner.LocalPlayer)
        {
            _defeatImage.SetActive(true);
            _winImage.SetActive(false);
        }
        UnsubscribeToClientsInGame(client);


        if (_clientsInGame.Count == 1 && HasStateAuthority)
        {
            RPC_WinEvent(_clientsInGame[0]);
        }
    }

    [Rpc]
    private void RPC_WinEvent([RpcTarget] PlayerRef client)
    {
        _winImage.SetActive(true);
        _defeatImage.SetActive(false);
    }



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
