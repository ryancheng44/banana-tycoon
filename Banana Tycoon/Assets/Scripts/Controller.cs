using UnityEngine;
using TMPro;
using BreakInfinity;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    [SerializeField] private TMP_Text bananasText;
    [SerializeField] private TMP_Text bananaClickPowerText;

    public Data data;

    private void Awake() => instance = this;

    // Start is called before the first frame update
    void Start()
    {
        data = new Data();
        UpgradesManager.instance.StartUpgradeManager();
    }

    // Update is called once per frame
    void Update()
    {
        var bananas = data.bananas;
        var clickPower = ClickPower();

        bananasText.text = bananas.ToString("F2") + (bananas == 1 ? " Banana" : " Bananas");
        bananaClickPowerText.text = "+" + clickPower + (clickPower == 1 ? " Banana": " Bananas");
    }

    public void GenerateBananas() => data.bananas += ClickPower();

    private BigDouble ClickPower()
    {
        BigDouble total = 0;
        for (int i = 0; i < data.clickUpgradeLevel.Count; i++)
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        return total;
    }
}
