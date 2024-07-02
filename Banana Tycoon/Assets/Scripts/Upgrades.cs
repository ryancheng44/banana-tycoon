using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public int upgradeId;
    public TMP_Text levelText;
    public TMP_Text nameText;
    public TMP_Text costText;

    public void BuyClickUpgrade() => UpgradesManager.instance.BuyUpgrade(upgradeId);
}
