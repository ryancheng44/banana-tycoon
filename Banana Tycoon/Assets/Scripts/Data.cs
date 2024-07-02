using BreakInfinity;
using System.Collections.Generic;

public class Data
{
    public BigDouble bananas;
    public List<BigDouble> clickUpgradeLevel;

    public Data()
    {
        bananas = 0;

        clickUpgradeLevel = Methods.CreateList<BigDouble>(3);
    }
}
