using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShip : MonoBehaviour
{
    public UnityEvent<int> OnCrewOnShipCountChanged;
    public Action<int> OnCrewToPillagedChanged;
    [SerializeField] bool _debug;
    [SerializeField] float _cannonBallSpeed=4f;
    [SerializeField] float _gunsRange = 10;
    [SerializeField] int _gunsDamge = 2;
    [SerializeField] PlayerGunsComponent _gunsComponent;
    [SerializeField] LevelableUpgradeIntSO _crewUpgrade;
    [SerializeField] int _currentCrew;
    //[SerializeField] TMP_Text _currentCrewTextField;
    public int CurrentCrew => _currentCrew;
    private int _crewToPillage;
    private int _crewFromUpgrades=0;
    private void Start()
    {
        _gunsComponent.SetUp(_gunsRange,_cannonBallSpeed,_gunsDamge);
        OnCrewToPillagedChanged?.Invoke(0);
        OnCrewOnShipCountChanged?.Invoke(_currentCrew);
    }
    public void IncreaseCrewToPillage()
    {
        _crewToPillage++;
        _crewToPillage = Math.Clamp(_crewToPillage, 0, _currentCrew);
        OnCrewToPillagedChanged?.Invoke(_crewToPillage);
    }
    public void DecreaserewToPillage()
    {
        _crewToPillage--;
        _crewToPillage = Math.Clamp(_crewToPillage, 0, _currentCrew);
        OnCrewToPillagedChanged?.Invoke(_crewToPillage);
    }
    public void Pillage(int crewToSent)
    {
        _currentCrew-=crewToSent;
        if(_currentCrew<_crewToPillage)_crewToPillage = _currentCrew;
        OnCrewToPillagedChanged?.Invoke(_crewToPillage);
        OnCrewOnShipCountChanged?.Invoke(_currentCrew);

    }
    public void CrewReturns(int crewToReturn)
    {
        _currentCrew += crewToReturn;
        OnCrewOnShipCountChanged?.Invoke(_currentCrew);
    }
    public void IncreaseCrewSize(UpgradeSO upgrade,int level)
    {
        if (upgrade != _crewUpgrade) return;
        int diff = _crewUpgrade.PerLevelIncrease * level - _crewFromUpgrades;
        _crewFromUpgrades = _crewUpgrade.PerLevelIncrease * level;

        _currentCrew += diff;
        OnCrewOnShipCountChanged?.Invoke(_currentCrew);
    }
    public void FireGuns()
    {
        _gunsComponent.FireGuns();
    }
    private void OnDrawGizmos()
    {
        if(_debug)
        {
            if (_gunsComponent != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(_gunsComponent.transform.position, _gunsRange);
            }
        }
    }

}
