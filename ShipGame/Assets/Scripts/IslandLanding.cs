using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class IslandLanding : MonoBehaviour,IAmountSettable,IInteractable
{
    public Action<int> OnPillaged;
    [SerializeField] ItemSpawner _crewSpawner;
    [SerializeField] int _minCrew;
    [SerializeField] float _unloadSpeed;
    [SerializeField] int _maxLoot;
    [SerializeField] PlayerShip _ship;
    [SerializeField] GameEventSO _unloadCrewEvent;
    [SerializeField] IslandDescription _description;
    [SerializeField] List<Transform> _pillagePoints= new List<Transform>();
    [SerializeField] Transform _crewSpawnPoint;
    private float _time = 0;
    private int _crewToPlunder;
    private int _returnedCrewNum = 0;
    private int _currentLoot;
    private int _pillagedLoot;
    private bool _isBeingPillaged = false;
    List<SpawnableItem> _crewMembers = new List<SpawnableItem>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentLoot = _maxLoot;
        if (_ship == null) _ship = FindFirstObjectByType<PlayerShip>();
        _crewToPlunder = _minCrew;
        _description.SetUp(_maxLoot, _minCrew, _minCrew);
    }
    public void SetUp(PlayerShip ship,ItemSpawner crewSpawner)
    {
        _ship = ship;
        _crewSpawner = crewSpawner;
    }
    // Update is called once per frame
    void Update()
    {
        if(_isBeingPillaged)
        {
            if (_currentLoot == 0) return;
            _time += Time.deltaTime;
            if(_time>=1f)
            {
                int pillagedAmount = _crewToPlunder * PlayerStats.lootperSecond;
                _currentLoot -= pillagedAmount;
                if(_currentLoot<0)
                {
                    pillagedAmount += _currentLoot;
                    _currentLoot = 0;
                }
                _description.UpdateLoot(_currentLoot);
                _pillagedLoot += pillagedAmount;
                _time = 0;
            }
        }
    }

    public void SetPlayerInteractions(Collider2D col)
    {
        PlayerInteractions playerInteractions = col.attachedRigidbody.GetComponent<PlayerInteractions>();
        playerInteractions.SetIslandLanding(this);
    }
    public void RemovePlayerInteractions(Collider2D col)
    {
        PlayerInteractions playerInteractions = col.attachedRigidbody.GetComponent<PlayerInteractions>();
        playerInteractions.SetIslandLanding(null);
    }
    public void IncreaseAmount()
    {
        _crewToPlunder++;
        _crewToPlunder = math.clamp(_crewToPlunder, 0, _ship.CurrentCrew - 1);
        _description.Refresh(_minCrew, _crewToPlunder);
    }

    public void DecreaseAmount()
    {
        _crewToPlunder--;
        if (_crewToPlunder <= 0) _crewToPlunder = 0;
        _description.Refresh(_minCrew, _crewToPlunder);
    }
    public void Interact()
    {
        if (_isBeingPillaged)
        {
            _unloadCrewEvent?.Raise();
            _isBeingPillaged = false;
            for (int i = 0; i < _crewToPlunder; i++)
            {
                _crewMembers[i].GetComponent<CrewMember>().Return();
            }

            Logger.Log("End pillage");
        }
        else
        {
            _isBeingPillaged = true;
            _unloadCrewEvent?.Raise();
            StartCoroutine(UnloadCor());
        }

    }
    private void OnCrewReturned()
    {
        _returnedCrewNum++;
        if(_returnedCrewNum==_crewToPlunder)
        {
            _unloadCrewEvent?.Raise();
            for (int i = 0; i < _crewToPlunder; i++)
            {
                _crewMembers[i].ReturnToPool();
                _crewMembers[i].GetComponent<CrewMember>().OnCrewReturned-=OnCrewReturned;
            }
            _crewMembers.Clear();
            _returnedCrewNum = 0;
            Logger.Log("Crew returned");
        }
    }

    private IEnumerator UnloadCor()
    {

        for (int i = 0; i < _crewToPlunder; i++)
        {
            yield return new WaitForSeconds(_unloadSpeed);
            SpawnableItem crewMember = _crewSpawner.GetItem();
            _crewMembers.Add(crewMember);
            crewMember.GetComponent<CrewMember>().OnCrewReturned += OnCrewReturned;
            crewMember.GetComponent<CrewMember>().SetSpawnPos(_crewSpawnPoint.position);
        }
        for (int i = 0, pillageIndex = 0; i < _crewToPlunder; i++)
        {
            _crewMembers[i].GetComponent<CrewMember>().SetPosToPlunder(_pillagePoints[pillageIndex].position);
            pillageIndex++;
            if (pillageIndex > _crewToPlunder) pillageIndex = 0;
        }


        _unloadCrewEvent?.Raise();

        Logger.Log("end unload");
    }
    private void Reset()
    {
        _ship = FindFirstObjectByType<PlayerShip>();
    }


}
