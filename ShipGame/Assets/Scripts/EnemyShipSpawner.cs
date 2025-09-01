using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    [SerializeField] LevelManager _levelManger;

    [SerializeField] ItemSpawner _easyShipSpawner;
    [SerializeField] ItemSpawner _mediumShipSpawner;
    [SerializeField] ItemSpawner _hardShipSpawner;
    [SerializeField] PlayerMovement2D _playerMovement2D;
    [SerializeField] ItemSpawner _enemyCannonBallSpawner;
    [SerializeField] Transform _playerMainBody;
    [SerializeField] int _maximumNumberOfShips;
    [SerializeField] List<Transform> _spawnPoints = new List<Transform>();

    private int _numberOfShipsSpawned;

    private void Start()
    {
        StartSpawning();
    }
    public void NotorityIncreased(int notority)
    {
        _maximumNumberOfShips++;
    }
    public void ShipDestroyed(int loot)
    {
        EnemyShipController ship=null;
        switch (_levelManger.NotorityLevel)
        {
            case 1: ship = _easyShipSpawner.GetItem().GetComponent<EnemyShipController>(); break;
            case 2: ship = _mediumShipSpawner.GetItem().GetComponent<EnemyShipController>(); break;
            case 3: ship = _hardShipSpawner.GetItem().GetComponent<EnemyShipController>(); break;
            default: _easyShipSpawner.GetItem().GetComponent<EnemyShipController>(); break;
        }
        if(ship==null)
        {
            Logger.Error("Ship was not correctly spawned");
        }
        else ship.SetUp(_spawnPoints[Random.Range(0, _spawnPoints.Count)].position + new Vector3(Random.Range(-3, 3), Random.Range(-3, 3)), _playerMovement2D, _playerMainBody, _enemyCannonBallSpawner);


    }

    public void StartSpawning()
    {
        for(int i=0;i<_maximumNumberOfShips;i++)
        {
            EnemyShipController ship = _easyShipSpawner.GetItem().GetComponent<EnemyShipController>();
            ship.SetUp(_spawnPoints[Random.Range(0,_spawnPoints.Count)].position+new Vector3(Random.Range(-3,3), Random.Range(-3, 3)), _playerMovement2D, _playerMainBody, _enemyCannonBallSpawner);
        }
    }
}
