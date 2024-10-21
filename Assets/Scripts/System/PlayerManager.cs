using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private int playerCurrentHealth;
    [SerializeField] private int playerCurrentMaxHealth;
    [SerializeField] private int playerCurrentMaxMana;
    [SerializeField] private float playerCurrentMaxStamina;
    [SerializeField] private float playerCurrentMaxShield;

    public int HP
    {
        get => playerCurrentHealth;
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

    void Start()
    {
        playerCurrentMaxHealth = 100;
        playerCurrentMaxMana = 100;
        playerCurrentMaxStamina = 100f;
        playerCurrentMaxShield = 5f;
        playerCurrentHealth = playerCurrentMaxHealth;

    }

    public void GrabStats(PlayerStats playerStats)
    {
        playerCurrentHealth = playerStats.HP;
        playerCurrentMaxHealth = (int)playerStats.GetMaxStat("health");
        playerCurrentMaxMana = (int)playerStats.GetMaxStat("mana");
        playerCurrentMaxStamina = playerStats.GetMaxStat("stamina");
        playerCurrentMaxShield = playerStats.GetMaxStat("shield");
    }
    public void ResetStats()
    {
        playerCurrentMaxHealth = 100;
        playerCurrentMaxMana = 100;
        playerCurrentMaxStamina = 100f;
        playerCurrentMaxShield = 5f;
        playerCurrentHealth = playerCurrentMaxHealth;
    }
}
