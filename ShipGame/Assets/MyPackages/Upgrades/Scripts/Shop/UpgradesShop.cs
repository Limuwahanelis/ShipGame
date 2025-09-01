using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] UnityEvent<int> OnLootChanged;
    [SerializeField]List<LevelableUpgrade> _levelableUpgrades;
    [SerializeField] List<NonLevelableUpgrade> _nonLevelableUpgrades;
    [SerializeField] TMP_Text _playerMoneyTextField;
    private void OnEnable()
    {
        UpgradesManager.SetLevelAtStart();
        UpgradesManager.SetUnlockAtStart();
        _playerMoneyTextField.text = PlayerStats.loot.ToString( ) + " $";
    }
    public void BuyLeveleableUpgrade(LevelableUpgradeSO upgradeSO,int level)
    {
        UpgradesManager.IncreaseUpgradeLevel(upgradeSO.Id, level);
        _playerMoneyTextField.text = PlayerStats.loot.ToString() + " $";
        OnLootChanged?.Invoke(PlayerStats.loot);
    }
    public void NonLevelableUpgradeBought(NonLevelableUpgradeSO upgradeSO)
    {
        UpgradesManager.UnlockUpgrade(upgradeSO.Id);
        _playerMoneyTextField.text = PlayerStats.loot.ToString() + " $";
        OnLootChanged?.Invoke(PlayerStats.loot);
    }
    public void ResetUpgradesLevel()
    {
        UpgradesManager.ResetLevelAtStart();
        UpgradesManager.ReSetUnlockAtStart();
    }
    private void Start()
    {
        foreach (var upgrade in _levelableUpgrades)
        {
            upgrade.GetCurrentUpgradeLevel();
        }
        foreach (var upgrade in _nonLevelableUpgrades)
        {
            upgrade.CheckStatus();
        }
    }
}
