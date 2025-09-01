using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LevelableUpgradeLevelUI))]
public abstract class LevelableUpgrade : MonoBehaviour
{
    public GameEventUpgradeSO OnUpgradeBoughtSO;
    public UnityEvent<LevelableUpgradeSO, int> OnUpgradeBought;
    [SerializeField] protected LevelableUpgradeLevelUI _upgradeLevellUI;
    protected int _upgradelevelToBuy=0;
    protected int _upgradeCurrentLevel = 0;
    protected int toPay = 0;
    public void IncreaseLevelToBuy(LevelableUpgradeSO upgrade)
    {
        if (_upgradeCurrentLevel >= upgrade.MaxLevel) return;
        _upgradelevelToBuy++;
        _upgradelevelToBuy = math.clamp(_upgradelevelToBuy, _upgradeCurrentLevel + 1, upgrade.MaxLevel);

        int pay = 0;
        for (int i = _upgradeCurrentLevel + 1; i <= _upgradelevelToBuy; i++)
        {
            pay += i * (int)upgrade.CostPerLevel;
        }
        toPay = pay;
        _upgradeLevellUI.SetPreviewLevel(_upgradelevelToBuy);
        _upgradeLevellUI.SetPrice(toPay);
    }
    public void DecreaseLevelToBuy(LevelableUpgradeSO upgrade)
    {
        if (_upgradeCurrentLevel >= upgrade.MaxLevel) return;
        _upgradelevelToBuy--;
        _upgradelevelToBuy = math.clamp(_upgradelevelToBuy, _upgradeCurrentLevel + 1, upgrade.MaxLevel);
        int pay = 0;
        for (int i = _upgradeCurrentLevel + 1; i <= _upgradelevelToBuy; i++)
        {
            pay += i * (int)upgrade.CostPerLevel;
        }
        toPay = pay;
        _upgradeLevellUI.SetPreviewLevel(_upgradelevelToBuy);
        _upgradeLevellUI.SetPrice(toPay);
    }
    protected void BuyUpgrade(LevelableUpgradeSO upgrade)
    {
        if (_upgradeCurrentLevel >= upgrade.MaxLevel) return;
        if (toPay >= PlayerStats.loot) return;
        _upgradeCurrentLevel = _upgradelevelToBuy;
        PlayerStats.loot -= toPay;
        _upgradeLevellUI.SetUpgradeBuyLevel(_upgradeCurrentLevel);
        OnUpgradeBought?.Invoke(upgrade, _upgradeCurrentLevel);
        OnUpgradeBoughtSO?.Raise(upgrade, _upgradeCurrentLevel);
        IncreaseLevelToBuy(upgrade);
    }
    public abstract void GetCurrentUpgradeLevel();
}
