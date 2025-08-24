using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private IInteractable _interactable;
    private IAmountSettable _amountSettable;

    public void SetIslandLanding(IslandLanding landing)
    {
        _interactable = landing;
        _amountSettable = landing;
    }

    public void Interact()
    {
        if (_interactable == null) return;
        _interactable.Interact();
    }

    public void IncreaseAmount()
    {
        if (_amountSettable == null) return;
        _amountSettable.IncreaseAmount();
    }
    public void DecreaseAmount()
    {
        if (_amountSettable == null) return;
        _amountSettable.DecreaseAmount();
    }
}
