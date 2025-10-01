using Fusion;
using UnityEngine;

public class PickUpSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkPrefabRef[] _pickUps = default;

    [SerializeField, Min(0.05f)] private float _spawnIntervalSeconds = 3f;
    [SerializeField] private bool _autoStart = true;

    [SerializeField] private Transform[] _spawnPoint = default;


    [Networked] private TickTimer _spawnTimer { get; set; }
    [Networked] private NetworkBool _isRunning { get; set; }

    public override void Spawned()
    {
        if (HasStateAuthority && _autoStart)
        {
            _isRunning = true;
            ArmTimer();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!_isRunning)
            return;

        if (_spawnTimer.Expired(Runner))
        {
            TrySpawnOne();
            ArmTimer();
        }
    }
    public void StartSpawning()
    {
        if (!HasStateAuthority)
            return;

        if (!_isRunning)
        {
            _isRunning = true;
            ArmTimer();
        }
    }

    public void StopSpawning()
    {
        if (!HasStateAuthority)
            return;

        _isRunning = false;
        _spawnTimer = TickTimer.None;
    }

    private void ArmTimer()
    {
        var interval = Mathf.Max(0.05f, _spawnIntervalSeconds);
        _spawnTimer = TickTimer.CreateFromSeconds(Runner, interval);
    }

    private void TrySpawnOne()
    {
        if (_pickUps == null || _pickUps.Length == 0)
        {
            Debug.LogWarning("PickupSpawner:  has no pickups assigned.");
            return;
        }

        var prefabRef = _pickUps[Random.Range(0, _pickUps.Length)];

        var t = _spawnPoint[Random.Range(0, _spawnPoint.Length)];
        var pos = t.position;
        var rot = t.rotation;

        var obj = Runner.Spawn(prefabRef, pos, rot);

    }
}
