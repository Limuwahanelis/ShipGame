using TMPro;
using UnityEngine;

public class IslandDescription : MonoBehaviour
{
    [SerializeField] TMP_Text _lootTextField;
    [SerializeField] TMP_Text _crewTextField;

    public void SetUp(int loot,int minCrew,int currentPlayerCrew)
    {
        _lootTextField.text = loot.ToString();
        _lootTextField.color = Color.yellow;
        if (currentPlayerCrew >= minCrew) _crewTextField.color = Color.green;
        else _crewTextField.color = Color.red;
        _crewTextField.text = $"{currentPlayerCrew}/{minCrew}";

    }

    public void Refresh(int minCrew, int playerCrew)
    {
        _lootTextField.color = Color.yellow;
        if (playerCrew >= minCrew) _crewTextField.color = Color.green;
        else _crewTextField.color = Color.red;
        _crewTextField.text = $"{playerCrew}/{minCrew}";

    }
    public void UpdateLoot(int loot)
    {
        _lootTextField.text = loot.ToString();
    }
}
