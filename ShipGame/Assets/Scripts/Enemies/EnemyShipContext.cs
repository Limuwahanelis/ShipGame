using UnityEngine;

public class EnemyShipContext : EnemyContext
{
    public Vector2 enemyPos { get { return enemyTran.position; } set {  enemyTran.position = value; } }
    public Transform playerTran;
    public Transform enemyTran;
    public EnemyStats stats;
    public Vector2 circlingMiddlePoint;
}
