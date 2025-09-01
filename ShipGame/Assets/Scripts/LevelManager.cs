using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityEvent<int> OnNotorityLevelIncreased;
    public UnityEvent<int> OnLootChanged;
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
        if (_notorityPoints < 35) return ;
        else if (_notorityPoints < 85) OnNotorityLevelIncreased?.Invoke(2);
        else OnNotorityLevelIncreased?.Invoke(3);
        
    }
    public void GoldPillaged(int amount)
    {
        _notorityPoints += (int)(amount / _pillagedGoldToNotority);
        _onNotorityPointsChanged?.Raise(_notorityPoints);
        _onNotorityLevelChanged?.Raise(NotorityLevel);
        PlayerStats.loot +=(int) (amount + amount*(NotorityLevel-1)* 0.2);
        OnLootChanged?.Invoke(amount);
    }
    private void OnDestroy()
    {
        foreach (IslandLanding landing in _islands)
        {
            landing.OnPillaged -= GoldPillaged;
        }
    }
}
