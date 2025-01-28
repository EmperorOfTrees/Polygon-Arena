using System;
using System.Collections;
using System.Collections.Generic;
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
    //reference Upgrade Option displays, random generation of upgrades and assignment of such, function for application, contigency if there are no more upgrades to pick(add the MultiUps as an option), add a skip button to the upgrade system, maybe an auto skip function as well
    // when implemented, add usable items as upgrades, usable items being like in Binding of Isaac

    // sort through which options are actually available before generating options, use player manager information
    [SerializeField] private UpgradeOptionDisplay option1;
    [SerializeField] private UpgradeOptionDisplay option2;
    [SerializeField] private UpgradeOptionDisplay option3;

    private UpgradeType uType1;
    private UpgradeType uType2;
    private UpgradeType uType3;

    private int index1;
    private int index2;
    private int index3;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateOptions()
    {

    }
}
