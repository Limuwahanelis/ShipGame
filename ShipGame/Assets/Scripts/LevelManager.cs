using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public int NotorityLevel 
    {
        get 
        {
            if (_notorityPoints < 35) return 1;
            else if (_notorityPoints < 85) return 2;
            else return 3;
        }
    }
    [SerializeField] List<IslandLanding> _islands = new List<IslandLanding>();
    [SerializeField] ItemSpawner _crewSpawner;
    [SerializeField] PlayerShip _ship;
    [SerializeField] float _pillagedGoldToNotority = 100;
    [SerializeField] GameEventIntSO _onNotorityLevelChanged;
    [SerializeField] GameEventIntSO _onNotorityPointsChanged;
    private int _notorityPoints;
    private void Awake()
    {
        foreach(IslandLanding landing in _islands)
        {
            landing.SetUp(_ship, _crewSpawner);
            landing.OnPillaged += GoldPillaged;
        }
    }
    public void IncreaseseNotorityPoints(int amount)
    {
        _notorityPoints += amount;
        _notorityPoints = math.clamp(_notorityPoints, 0, 200);
        _onNotorityPointsChanged?.Raise(_notorityPoints);
        _onNotorityLevelChanged?.Raise(NotorityLevel);
    }

    public void GoldPillaged(int amount)
    {
        _notorityPoints += (int)(amount / _pillagedGoldToNotority);
        _onNotorityPointsChanged?.Raise(_notorityPoints);
        _onNotorityLevelChanged?.Raise(NotorityLevel);
    }
    private void OnDestroy()
    {
        foreach (IslandLanding landing in _islands)
        {
            landing.OnPillaged -= GoldPillaged;
        }
    }
}
