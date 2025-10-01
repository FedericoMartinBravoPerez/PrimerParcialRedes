using System;
using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private int _playerToStart = 2;

    private event Action OnAllPlayersJoined = delegate { };

    
    public void PlayerJoined(PlayerRef player) // metodo de la interfaz, se ejecuta en todos los clientes cada vez que se une un player
    {
        var playerCount = Runner.SessionInfo.PlayerCount;

        if (playerCount >= _playerToStart && OnAllPlayersJoined != null)
        {
            OnAllPlayersJoined?.Invoke();
            OnAllPlayersJoined = null;
        }
        
        if (player == Runner.LocalPlayer)   //La referencia del cliente entrante, es igual a la referencia del cliente local?
        {
            // Cantidad de jugadores conectados a la sesion

            if (playerCount < _playerToStart)
            {
                //if (OnAllPlayersJoined != null) return;
                OnAllPlayersJoined += () => { SpawnPlayer(playerCount); };
            }
            else
            {
                SpawnPlayer(playerCount);
            }
        }
    }

    private void SpawnPlayer(int playerCount)
    {
        // Spawn point designado por cantidad de jugadores
        var spawnPoint = _spawnPoints[playerCount - 1];
        Runner.Spawn(_playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    
}
