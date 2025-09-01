using UnityEngine;
using UnityEngine.AI;

public class EnemyShipContext : EnemyContext
{
    public Vector2 enemyPos { get { return enemyTran.position; } set {  enemyTran.position = value; } }
    public Vector2 playeryPos { get { return playerTran.position; }  }
    public Transform playerTran;
    public Transform enemyTran;
    public EnemyStats stats;
    public Vector2 circlingMiddlePoint;
    public GunsComponent guns;
    public float gunsTimer;
    public NavMeshAgent agent;
    public ShipRaycasts shipRaycasts;
    public PlayerMovement2D playerMovement2D;
}
