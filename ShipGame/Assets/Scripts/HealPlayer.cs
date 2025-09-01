using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealPlayer : MonoBehaviour
{
    [SerializeField] UnityEvent<int> OnLootChanged;
    [SerializeField] PlayerHealthSystem _playerHealth;
    [SerializeField] TMP_Text _healCostTextField;
    [SerializeField] Button _healButton;
    [SerializeField] LevelableUpgradeIntSO _maxHPUpgrade;
    [SerializeField] TMP_Text _playerMoneyTextField;
    private void OnEnable()
    {
        if (_playerHealth.MaxHP != _playerHealth.CurrentHP) _healButton.interactable = true ;
        else _healButton.interactable = false;
        _healCostTextField.text =( _playerHealth.MaxHP - _playerHealth.CurrentHP).ToString();
    }
    public void Heal()
    {
        if(PlayerStats.loot>= _playerHealth.MaxHP-_playerHealth.CurrentHP)
        {
            PlayerStats.loot -= _playerHealth.MaxHP - _playerHealth.CurrentHP;
            _playerHealth.SetHP(_playerHealth.MaxHP);
            _playerMoneyTextField.text = PlayerStats.loot.ToString() + " $";
            OnLootChanged?.Invoke(PlayerStats.loot);
        }
        _healCostTextField.text = "0";
        _healButton.interactable=false;
    }
    public void SetToHealed(LevelableUpgradeSO upgrade, int level)
    {
        if (upgrade == _maxHPUpgrade)
        {
            _healCostTextField.text = "0";
            _healButton.interactable=false;
        }
    }
}
