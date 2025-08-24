using UnityEngine;

public class PlayerRaycasts : MonoBehaviour
{

    public bool isHittingWall => _isHittingWall;
    [SerializeField] Transform _frontShipPos;
    [SerializeField] float _frontShipRaycastlength;
    [SerializeField] LayerMask _walls;
    private bool _isHittingWall;

    private void Update()
    {
        _isHittingWall= Physics2D.Raycast(_frontShipPos.position, _frontShipPos.up, _frontShipRaycastlength, _walls);
    }
    private void OnDrawGizmos()
    {
        if (_frontShipPos == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_frontShipPos.position, _frontShipPos.position+  transform.up * _frontShipRaycastlength);
    }
}
