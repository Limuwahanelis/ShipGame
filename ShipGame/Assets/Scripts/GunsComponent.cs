using System.Collections.Generic;
using UnityEngine;

public class GunsComponent : MonoBehaviour
{
    [SerializeField] List<ShipGun> _gunLines = new List<ShipGun>();
    [SerializeField] ItemSpawner _cannonBallSpawner;
    public void FireGuns()
    {

    }

    public void LookAtMouse()
    {
        Vector3 tmp = transform.position;
        tmp.z = -1;
        transform.up = Camera.main.ScreenToWorldPoint(HelperClass.MousePos)- tmp;
        Logger.Log($"{Camera.main.ScreenToWorldPoint(HelperClass.MousePos)} a {transform.position}");
        Logger.Log(transform.up);
    }
}
