using UnityEngine;

public class PlayerGunsComponent : GunsComponent
{
    private void Update()
    {
        Vector3 tmp = transform.position;
        tmp.z = -1;
        transform.up = Camera.main.ScreenToWorldPoint(HelperClass.MousePos) - tmp;
    }
}
