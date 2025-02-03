using System;
using System.Collections.Generic;
using UnityEngine;

// TODO: add something for healing pick ups and similar things
public enum OneTimeUps
{
    Jogger = 0,
    
    // definitely add more
}

public enum ExclusiveUps
{
    Hoplite = 0,

    //definitely add more
}

public enum MultiUps
{
    Health = 0,
    Mana = 1,
    Stamina = 2,
    Shield = 3,
    Damage = 4,
    BladeDamage = 5,
    TipDamage = 6,
    
}

public class PlayerManager : Singleton<PlayerManager>
{
    private PlayerStats playerStats;

    private ExperienceBar xPBar;
    private readonly int startingEXP = 0;
    private int currentEXP;
    private int playerLevel;

    private PlayerStatBar playerHealthBar;
    private PlayerStatBar playerStaminaBar;
    private PlayerStatBar playerShieldBar;
    // private PlayerStatBar playerManaBar; TODO: Implement Mana as a resource and make things that use it

    [SerializeField] private readonly int startingLThreshold = 100;
    [SerializeField] private int levelingThreshold = 100;
    [SerializeField] private float growthFactor = 1.1f;

    [SerializeField] private int playerCurrentHealth;
    [SerializeField] private int playerCurrentMaxHealth;
    [SerializeField] private int playerCurrentMaxMana;
    [SerializeField] private float playerCurrentMaxStamina;
    [SerializeField] private float playerCurrentMaxShield;

    [SerializeField] private int baseBladeDamage = 20;
    [SerializeField] private int baseTipDamage = 40;
    [SerializeField] private int curBladeDamage;
    [SerializeField] private int curTipDamage;

    [SerializeField] private int playerStartingHealth;
    [SerializeField] private int playerStartingMaxHealth;
    [SerializeField] private int playerStartingMaxMana;
    [SerializeField] private float playerStartingMaxStamina;
    [SerializeField] private float playerStartingMaxShield;

    [SerializeField] private bool exclusivelyUpgraded;

    [SerializeField] private Dictionary<OneTimeUps, bool> oneTimeUpgrades;
    [SerializeField] private Dictionary<ExclusiveUps, bool> exclusiveUpgrades;
    [SerializeField] private Dictionary<MultiUps, int> multiTimeUpgrades;

    public int HP
    {
        get => playerCurrentHealth;
    }

    public int EXP
    {
        get => currentEXP;
    }

    public int LEVELTHRESHOLD
    {
        get => levelingThreshold;
    }

    public int BD
    {
        get => curBladeDamage;
    }

    public int TD
    {
        get => curTipDamage;
    }

    public int MHP
    {
        get => playerCurrentMaxHealth;
    }

    public float MSTA
    {
        get => playerCurrentMaxStamina;
    }

    public int MMP
    {
        get => playerCurrentMaxMana;
    }

    public float MSU
    {
        get => playerCurrentMaxShield;
    }
    private new void Awake()
    {
        base.Awake();
        oneTimeUpgrades = new Dictionary<OneTimeUps, bool>();
        exclusiveUpgrades = new Dictionary<ExclusiveUps, bool>();
        multiTimeUpgrades = new Dictionary<MultiUps, int>();
        InitialFillDictionaries();
    }

    void Start()
    {
        playerCurrentMaxHealth = playerStartingHealth;
        playerCurrentMaxMana = playerStartingMaxMana;
        playerCurrentMaxStamina = playerStartingMaxStamina;
        playerCurrentMaxShield = playerStartingMaxShield;
        playerCurrentHealth = playerCurrentMaxHealth;

        currentEXP = startingEXP;
        levelingThreshold = startingLThreshold;

        curBladeDamage = baseBladeDamage;
        curTipDamage = baseTipDamage;
    }

    public void GrabStats(PlayerStats playerStats)
    {
        playerCurrentHealth = playerStats.HP;
    }
    public void ResetStats()
    {
        playerCurrentMaxHealth = playerStartingHealth;
        playerCurrentMaxMana = playerStartingMaxMana;
        playerCurrentMaxStamina = playerStartingMaxStamina;
        playerCurrentMaxShield = playerStartingMaxShield;
        playerCurrentHealth = playerCurrentMaxHealth;

        currentEXP = startingEXP;
        levelingThreshold = startingLThreshold;

        curBladeDamage = baseBladeDamage;
        curTipDamage = baseTipDamage;

        ResetUpgrades();
    }

    private void InitialFillDictionaries()
    {
        for (int i = 0; i < Enum.GetValues(typeof(OneTimeUps)).Length; i++)
        {
            oneTimeUpgrades.Add((OneTimeUps)i, false);
        }
        for (int i = 0; i < Enum.GetValues(typeof(ExclusiveUps)).Length; i++)
        {
            exclusiveUpgrades.Add((ExclusiveUps)i, false);
        }
        for (int i = 0; i < Enum.GetValues(typeof(MultiUps)).Length; i++)
        {
            multiTimeUpgrades.Add((MultiUps)i, 0);
        }
    }
    private void ResetUpgrades()
    {
        for (int i = 0; i < oneTimeUpgrades.Count; i++)
        {
            oneTimeUpgrades[(OneTimeUps)i] = false;
        }
        for (int i = 0; i < exclusiveUpgrades.Count; i++)
        {
            exclusiveUpgrades[(ExclusiveUps)i] = false;
        }
        for (int i = 0; i < multiTimeUpgrades.Count; i++)
        {
            multiTimeUpgrades[(MultiUps)i] = 0;
        }
    }

    public void OneTimeUpgrade(OneTimeUps index)
    {
        if (!oneTimeUpgrades[index])
        {
            oneTimeUpgrades[index] = true;
        }
        else
        {
            print("this upgrade is already acquired");
        }
    }

    public void ExclusiveUpgrade(ExclusiveUps index)
    {
        for(int i = 0; i < exclusiveUpgrades.Count; i++)
        {
            if (exclusiveUpgrades[(ExclusiveUps)i]) exclusivelyUpgraded = true;
        }
        if (!exclusivelyUpgraded)
        {
            if (!exclusiveUpgrades[index])
            {
                exclusiveUpgrades[index] = true;
            }
        }
        else return;
    }

    public void ExclusiveOverrideUpgrade(ExclusiveUps index)
    {
        for (int i = 0; i < exclusiveUpgrades.Count; i++)
        {
            exclusiveUpgrades[(ExclusiveUps)i] = false;
        }

        if (!exclusiveUpgrades[index])
        {
            exclusiveUpgrades[index] = true;
        }
        exclusivelyUpgraded = true;
    }

    public void MultiUpgrade(MultiUps index)
    {
        multiTimeUpgrades[index] += 1;
        UpdateStats();
    }
    public void MultiUpgrade(MultiUps index, int amount)
    {
        multiTimeUpgrades[index] += amount;
        UpdateStats();
    }

    private void UpdateStats()
    {
        playerCurrentMaxHealth = playerStartingMaxHealth + (multiTimeUpgrades[MultiUps.Health] * 100);
        playerCurrentMaxMana = playerStartingMaxMana + (multiTimeUpgrades[MultiUps.Mana] * 10);
        playerCurrentMaxStamina = playerStartingMaxStamina + (multiTimeUpgrades[MultiUps.Stamina] * 10);
        playerCurrentMaxShield = playerStartingMaxShield + (multiTimeUpgrades[MultiUps.Shield] * 10);
        curBladeDamage = baseBladeDamage + (multiTimeUpgrades[MultiUps.Damage]) + (multiTimeUpgrades[MultiUps.BladeDamage] * 2);
        curTipDamage = baseTipDamage + (multiTimeUpgrades[MultiUps.Damage] * 2) + (multiTimeUpgrades[MultiUps.TipDamage] * 4);

        playerStats.SetStats();
    }


    public void SetPlayerStatReference(PlayerStats pS)
    {
        playerStats = pS;
    }

    public void Upgrade(UpgradeType type, int index)
    {
        switch (type)
        {
            case UpgradeType.MultiUp:
                MultiUpgrade((MultiUps)index);
                playerHealthBar.StatUpdate();
                playerStaminaBar.StatUpdate();
                playerShieldBar.StatUpdate();
                break;
            case UpgradeType.SingleUp:
                OneTimeUpgrade((OneTimeUps)index);
                break;
            case UpgradeType.ExclusiveUp:
                ExclusiveUpgrade((ExclusiveUps)index);
                break;
            case UpgradeType.ExclusiveUpOverride:
                ExclusiveOverrideUpgrade((ExclusiveUps)index);
                break;

            default: break;
        }
    }

    public void FindStatBar(PlayerStatBar statbar)
    {
        switch (statbar.GetResourceName())
        {
            case "health":
                playerHealthBar = statbar;
                break;
            case "stamina":
                playerStaminaBar = statbar;
                break;
            case "shield":
                playerShieldBar = statbar;
                break;
        }

    }
    public void SetEXPBar(ExperienceBar EBar)
    {
        xPBar = EBar;
    }

    public void GainEXP(int exp)
    {
        currentEXP += exp;
        if (currentEXP >= levelingThreshold)
        {
            int previousThreshold = levelingThreshold;
            LevelUp();
            currentEXP -= previousThreshold;
        }
        xPBar.SetCurrentEXP(currentEXP);
    }

    public void LevelUp()
    {
        playerLevel++;

        levelingThreshold = (int)(levelingThreshold * growthFactor);

        xPBar.UpdateMaxEXP(levelingThreshold);

        UpgradeMenu.Instance.GenerateOptions();
        GameManager.Instance.CurrentState = Game_State.Upgrading;
    }

    public Dictionary<OneTimeUps,bool> GetOnesDictionary()
    {
        return oneTimeUpgrades;
    }

    public Dictionary<MultiUps, int> GetMultiesDictionary()
    {
        return multiTimeUpgrades;
    }

    public Dictionary<ExclusiveUps, bool> GetExculisiveDictionary()
    {
        return exclusiveUpgrades;
    }

    public bool ExclusivelyUpgraded()
    {
        return exclusivelyUpgraded;
    }

    public void LevelUpTest()
    {
        LevelUp();
    }
}
