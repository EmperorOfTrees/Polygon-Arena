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

public class UpgradeMenu : MonoBehaviour
{
    
    // when implemented, add usable items as upgrades, usable items being like in Binding of Isaac

    [SerializeField] private UpgradeOptionDisplay option1;
    [SerializeField] private UpgradeOptionDisplay option2;
    [SerializeField] private UpgradeOptionDisplay option3;

    private Dictionary<UpgradeType, int> avaliableUpgrades = new Dictionary<UpgradeType, int>();

    private UpgradeType uType1;
    private UpgradeType uType2;
    private UpgradeType uType3;

    private int index1;
    private int index2;
    private int index3;

    void Update()
    {
        while (GameManager.Instance.CurrentState == Game_State.Upgrading)
        {

            // turn on the upgrade menu
        }
    }

    public void GenerateOptions()
    {
        FillDictionary();
        GenerateRandoms();

        option1.SetOption(uType1, index1);
        option2.SetOption(uType2, index2);
        option3.SetOption(uType3, index3);

        avaliableUpgrades.Clear();
    }

    public void FillDictionary()
    {
        int oneInFive = UnityEngine.Random.Range(1,6);

        for (int i = 0; i < Enum.GetValues(typeof(OneTimeUps)).Length; i++)
        {
            if (PlayerManager.Instance.GetOnesDictionary().ElementAt(i).Value == false)
            {
                avaliableUpgrades.Add(UpgradeType.SingleUp, i);
            }
        }

        if (!PlayerManager.Instance.ExclusivelyUpgraded())
        {
            for (int i = 0; i < Enum.GetValues(typeof(ExclusiveUps)).Length; i++)
            {
                if (PlayerManager.Instance.GetExculisiveDictionary().ElementAt(i).Value == false)
                {
                    avaliableUpgrades.Add(UpgradeType.ExclusiveUp, i);
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
                        avaliableUpgrades.Add(UpgradeType.ExclusiveUp, i);
                    }
                }
            }
        }

        if (avaliableUpgrades.Count < 3)
        {
            for (int i = 0; i < Enum.GetValues(typeof(MultiUps)).Length; i++)
            {
                    avaliableUpgrades.Add(UpgradeType.MultiUp, i);
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
            a = UnityEngine.Random.Range(0, avaliableUpgrades.Count);
            if (!randoms.Contains(a))
            {
                randoms.SetValue(a, 0);
            }
            else a = 0;
        }

        while (b == 0)
        {
            b = UnityEngine.Random.Range(0, avaliableUpgrades.Count);
            if (!randoms.Contains(b))
            {
                randoms.SetValue(b, 1);
            }
            else b = 0;
        }

        while (c == 0)
        {
            c = UnityEngine.Random.Range(0, avaliableUpgrades.Count);
            if (!randoms.Contains(c))
            {
                randoms.SetValue(c, 2);
            }
            else c = 0;
        }

        index1 = avaliableUpgrades.ElementAt(randoms[0]).Value;
        index2 = avaliableUpgrades.ElementAt(randoms[1]).Value;
        index3 = avaliableUpgrades.ElementAt(randoms[2]).Value;

        uType1 = avaliableUpgrades.ElementAt(randoms[0]).Key;
        uType2 = avaliableUpgrades.ElementAt(randoms[1]).Key;
        uType3 = avaliableUpgrades.ElementAt(randoms[2]).Key;
    }
}
