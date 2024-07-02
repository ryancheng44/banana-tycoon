using UnityEngine;
using BreakInfinity;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;

    [SerializeField] private List<Upgrades> clickUpgrades;
    [SerializeField] private Upgrades clickUpgradePrefab;
    [SerializeField] private string[] clickUpgradeNames;

    [SerializeField] private ScrollRect clickUpgradesScroll;
    [SerializeField] private Transform clickUpgradesPanel;

    private BigDouble[] clickUpgradeBaseCost;
    private BigDouble[] clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;

    private void Awake() => instance = this;
    
    public void StartUpgradeManager()
    {
        clickUpgradeNames = new[] { "Click Power +1", "Click Power +5", "Click Power +10" };
        clickUpgradeBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BigDouble[] { 1.25, 1.35, 1.55 };
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10 };

        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel);
            upgrade.upgradeId = i;
            clickUpgrades.Add(upgrade);
        }
        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);
        UpdateClickUpgradeUI();
    }

    public void BuyUpgrade(int upgradeId)
    {
        var data = Controller.instance.data;
        if (data.bananas >= ClickUpgradeCost(upgradeId))
        {
            data.bananas -= ClickUpgradeCost(upgradeId);
            data.clickUpgradeLevel[upgradeId] += 1;
        }

        UpdateClickUpgradeUI(upgradeId);
    }

    private BigDouble ClickUpgradeCost(int upgradeId) => clickUpgradeBaseCost[upgradeId] * BigDouble.Pow(clickUpgradeCostMult[upgradeId], Controller.instance.data.clickUpgradeLevel[upgradeId]);

    private void UpdateClickUpgradeUI(int upgradeId = -1)
    {
        if (upgradeId == -1)
            for (int i = 0; i < clickUpgrades.Count; i++) UpdateUI(i);
        else UpdateUI(upgradeId);

        void UpdateUI(int id)
        {
            clickUpgrades[id].levelText.text = Controller.instance.data.clickUpgradeLevel[id].ToString();
            clickUpgrades[id].costText.text = $"Cost: {ClickUpgradeCost(id):F2} Bananas";
            clickUpgrades[id].nameText.text = clickUpgradeNames[id];
        }
    }
}
