using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public int CurrentCrew => _currentCrew;
    private int _currentCrew;

    private void Start()
    {
        _currentCrew = 3;
    }
    public void Pillage(int crewToSent)
    {

    }

}
