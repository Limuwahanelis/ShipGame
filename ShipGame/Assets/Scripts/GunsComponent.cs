using System.Collections.Generic;
using UnityEngine;

public class GunsComponent : MonoBehaviour
{
    [SerializeField] List<ShipGun> _guns = new List<ShipGun>();
    [SerializeField] ItemSpawner _cannonBallSpawner;
    [SerializeField] protected AudioEvent _gunFireAudioEvent;
  
    protected AudioEventPlayer _audioEventPlayer;
    private void Start()
    {
        _audioEventPlayer = FindFirstObjectByType<AudioEventPlayer>();
    }
    public void SetUp(float gunsRange,float cannobBallSpeed,int damage,ItemSpawner cannonBallpawner=null)
    {
        if (_cannonBallSpawner == null) _cannonBallSpawner = cannonBallpawner;
        foreach (ShipGun gun in _guns)
        {
            gun.SetUp(_cannonBallSpawner, gunsRange, cannobBallSpeed, damage);
        }
    }
    public virtual void FireGuns()
    {
        _audioEventPlayer.PlayeAudioEvent(_gunFireAudioEvent);
        foreach (ShipGun gun in _guns)
        {
            gun.Fire();
           
        }

    }
    public void LookAt(Vector3 pos)
    {
        transform.up = (pos - transform.position).normalized;
    }
}
