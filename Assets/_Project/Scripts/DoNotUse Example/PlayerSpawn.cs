using Fusion;
using UnityEngine;


namespace DoNotUse.Example
{
    public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
    {
        [SerializeField] private NetworkPrefabRef _playerPrefab;
        [SerializeField] private Transform[] _spawnPoints;

        public void PlayerJoined(PlayerRef player)
        {
            if (player == Runner.LocalPlayer)   //La referencia del cliente entrante, es igual a la referencia del cliente local?
            {
                // Cantidad de jugadores conectados a la sesion
                var playerCount = Runner.SessionInfo.PlayerCount;
                
                // Spawn point designado por esa cantidad de jugadores
                var spawnPoint = _spawnPoints[playerCount - 1];
                
                Runner.Spawn(_playerPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
        
    }
}
