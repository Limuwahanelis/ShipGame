using System;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public Action<int> OnCrewToPillagedChanged;
    [SerializeField] bool _debug;
    [SerializeField] float _cannonBallSpeed=4f;
    [SerializeField] float _gunsRange = 10;
    [SerializeField] int _gunsDamge = 2;
    [SerializeField] GunsComponent _gunsComponent;
    public int CurrentCrew => _currentCrew;
    private int _currentCrew;
    private int _crewToPillage;
    private void Start()
    {
        _currentCrew = 3;
        _gunsComponent.SetUp(_gunsRange,_cannonBallSpeed,_gunsDamge);
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
