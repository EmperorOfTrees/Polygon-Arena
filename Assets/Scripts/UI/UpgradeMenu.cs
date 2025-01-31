using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType
{
    MultiUp = 0,
    SingleUp = 1,
    ExclusiveUp = 2,
    ExclusiveUpOverride = 3,
}

public enum Rarity
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Epic = 3,
}

public class LocalUpgradeIdentifier
{

    public UpgradeType upgradeType;
    public int currentIndex;

    public LocalUpgradeIdentifier(UpgradeType uType, int cIndex)
    {
        upgradeType = uType;
        currentIndex = cIndex;
    }
}

public class UpgradeMenu : StaticInstance<UpgradeMenu>
{
    
    // when implemented, add usable items as upgrades, usable items being like in Binding of Isaac

    [SerializeField] private UpgradeOptionDisplay option1;
    [SerializeField] private UpgradeOptionDisplay option2;
    [SerializeField] private UpgradeOptionDisplay option3;

    [SerializeField] private List<LocalUpgradeIdentifier> availableUpgrades1 = new List<LocalUpgradeIdentifier>();

    //private Dictionary<UpgradeType, int> availableUpgrades = new Dictionary<UpgradeType, int>();

    private UpgradeType uType1;
    private UpgradeType uType2;
    private UpgradeType uType3;

    private int index1;
    private int index2;
    private int index3;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            PlayerManager.Instance.LevelUpTest();
        }
    }

    public void GenerateOptions()
    {
        FillDictionary();
        GenerateRandoms();

        option1.SetOption(uType1, index1);
        option2.SetOption(uType2, index2);
        option3.SetOption(uType3, index3);

        availableUpgrades1.Clear();

        //availableUpgrades.Clear();
    }

    public void FillDictionary()
    {
        int oneInFive = UnityEngine.Random.Range(1,6);

        for (int i = 0; i < Enum.GetValues(typeof(OneTimeUps)).Length; i++)
        {
            if (PlayerManager.Instance.GetOnesDictionary().ElementAt(i).Value == false)
            {
                availableUpgrades1.Add(new LocalUpgradeIdentifier(UpgradeType.SingleUp, i));
                //avaliableUpgrades.Add(UpgradeType.SingleUp, i);
            }
        }

        if (!PlayerManager.Instance.ExclusivelyUpgraded())
        {
            for (int i = 0; i < Enum.GetValues(typeof(ExclusiveUps)).Length; i++)
            {
                if (PlayerManager.Instance.GetExculisiveDictionary().ElementAt(i).Value == false)
                {
                    availableUpgrades1.Add(new LocalUpgradeIdentifier(UpgradeType.ExclusiveUp, i));
                    //avaliableUpgrades.Add(UpgradeType.ExclusiveUp, i);
                }
            }
        }
        else if (PlayerManager.Instance.ExclusivelyUpgraded())
        {
            if (oneInFive == 3)
            {
                for (int i = 0; i < Enum.GetValues(typeof(ExclusiveUps)).Length; i++)
                {
                    if (PlayerManager.Instance.GetExculisiveDictionary().ElementAt(i).Value == false)
                    {
                        availableUpgrades1.Add(new LocalUpgradeIdentifier(UpgradeType.ExclusiveUpOverride, i));
                        //avaliableUpgrades.Add(UpgradeType.ExclusiveUpOverride, i);
                    }
                }
            }
        }

        if (availableUpgrades1.Count < 3)
        {
            for (int i = 0; i < Enum.GetValues(typeof(MultiUps)).Length; i++)
            {
                availableUpgrades1.Add(new LocalUpgradeIdentifier(UpgradeType.MultiUp, i));
                //avaliableUpgrades.Add(UpgradeType.MultiUp, i);
            }
        }
    }

    private void GenerateRandoms()
    {
        int[] randoms = new int[3];
        int a = 0;
        int b = 0;
        int c = 0;

        while (a == 0)
        {
            a = UnityEngine.Random.Range(0, availableUpgrades1.Count);
            if (!randoms.Contains(a))
            {
                randoms.SetValue(a, 0);
            }
            else a = 0;
        }

        while (b == 0)
        {
            b = UnityEngine.Random.Range(0, availableUpgrades1.Count);
            if (!randoms.Contains(b))
            {
                randoms.SetValue(b, 1);
            }
            else b = 0;
        }

        while (c == 0)
        {
            c = UnityEngine.Random.Range(0, availableUpgrades1.Count);
            if (!randoms.Contains(c))
            {
                randoms.SetValue(c, 2);
            }
            else c = 0;
        }

        index1 = availableUpgrades1[0].currentIndex;
        index2 = availableUpgrades1[1].currentIndex;
        index3 = availableUpgrades1[2].currentIndex;

        uType1 = availableUpgrades1[0].upgradeType;
        uType2 = availableUpgrades1[1].upgradeType;
        uType3 = availableUpgrades1[2].upgradeType;

        /*index1 = availableUpgrades.ElementAt(randoms[0]).Value;
        index2 = availableUpgrades.ElementAt(randoms[1]).Value;
        index3 = availableUpgrades.ElementAt(randoms[2]).Value;

        uType1 = availableUpgrades.ElementAt(randoms[0]).Key;
        uType2 = availableUpgrades.ElementAt(randoms[1]).Key;
        uType3 = availableUpgrades.ElementAt(randoms[2]).Key;*/
    }

    public void HideGraphics()
    {
        option1.TurnOffThis();
        option2.TurnOffThis();
        option3.TurnOffThis();
    }

    public void ShowGraphics()
    {
        option1.TurnOnThis();
        option2.TurnOnThis();
        option3.TurnOnThis();
    }
}
