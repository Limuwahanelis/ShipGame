using System.Collections.Generic;
using UnityEngine;

public class GunsComponent : MonoBehaviour
{
    [SerializeField] List<ShipGun> _guns = new List<ShipGun>();
    [SerializeField] ItemSpawner _cannonBallSpawner;
    public void SetUp(float gunsRange,float cannobBallSpeed,int damage)
    {
        foreach (ShipGun gun in _guns)
        {
            gun.SetUp(_cannonBallSpawner, gunsRange, cannobBallSpeed, damage);
        }
    }
    public void FireGuns()
    {
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
