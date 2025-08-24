using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class IslandLanding : MonoBehaviour,IAmountSettable,IInteractable
{
    [SerializeField] ItemSpawner _crewSpawner;
    [SerializeField] int _minCrew;
    [SerializeField] float _unloadSpeed;
    [SerializeField] float _maxLoot;
    [SerializeField] PlayerShip _ship;
    [SerializeField] GameEventSO _unloadCrewEvent;
    [SerializeField] IslandDescription _description;
    private int _crewToPlunder;
    private bool _isBeingPillaged = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_ship == null) _ship = FindFirstObjectByType<PlayerShip>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private IEnumerator UnloadCor()
    {
        yield return new WaitForSeconds(_crewToPlunder * _unloadSpeed);
        _unloadCrewEvent?.Raise();
        Logger.Log("end unload");
    }
    private void Reset()
    {
        _ship = FindFirstObjectByType<PlayerShip>();
    }

    public void Interact()
    {
        if(_isBeingPillaged)
        {
            _isBeingPillaged = true;
            _unloadCrewEvent?.Raise();
            Logger.Log("End pillage");
        }
        else
        {
            _isBeingPillaged = false;
            _unloadCrewEvent?.Raise();
            StartCoroutine(UnloadCor());
        }

    }
}
