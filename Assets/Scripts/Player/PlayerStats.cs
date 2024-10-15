using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacterController))]
public class PlayerStats : MonoBehaviour
{
    private PlayerCharacterController playerCharacterController;
    private EquipmentController equipmentController;
    [SerializeField] private int currentHealth;
    [SerializeField] private float currentStamina;
    [SerializeField] private int currentMana;
    [SerializeField] private float currentShieldUp;
    [SerializeField] private int maxHP = 100;
    [SerializeField] private float maxSTA = 100f;
    [SerializeField] private int maxMP = 100;
    [SerializeField] private float shieldUpMax = 5f;
    public bool isShieldRecharging;
    public bool dead = false;
    PlayerManager pManager;

    public int HP
    {
        get => currentHealth;
    }

    public float STA
    {
        get => currentStamina;
    }

    public int MP
    {
        get => currentMana;
    }

    public float SU
    {
        get => currentShieldUp;
    }

    public float GetMaxStat(string resource)
    {
        if (resource.Equals("health"))
        {
            return maxHP;
        }
        else if (resource.Equals("mana"))
        {
            return maxMP;
        }
        else if (resource.Equals("stamina"))
        {
            return maxSTA;
        }

        else if (resource.Equals("shield"))
        {
            return shieldUpMax;
        }
        else return 0;
    }

    public float GetCurrentStat(string resource)
    {
        if (resource.Equals("health"))
        {
            return currentHealth;
        }
        else if (resource.Equals("mana"))
        {
            return currentMana;
        }
        else if (resource.Equals("stamina"))
        {
            return currentStamina;
        }
        else if (resource.Equals("shield"))
        {
            return currentShieldUp;
        }
        else return 0;
    }

    private void Awake()
    {
        playerCharacterController = GetComponent<PlayerCharacterController>();
        pManager = FindFirstObjectByType<PlayerManager>();
        equipmentController = GetComponentInChildren<EquipmentController>();
        maxHP = pManager.MHP;  currentHealth = pManager.HP; currentStamina = pManager.MSTA; currentMana = pManager.MMP; currentShieldUp = pManager.MSU;
    }
    private void FixedUpdate()
    {
        StaminaRecovery();
        ShieldRecovery();
        if (currentHealth > 0)
        {
            dead = true;
        }
    }
    public void AdjustHealth(int change)
    {
        currentHealth -= change;
    }

    public void AdjustStamina(float change)
    {
        currentStamina -= change;
    }

    public void AdjustMana(int change)
    {
        currentMana -= change;
    }

    private void StaminaRecovery()
    {
        if (!playerCharacterController.isSprinting)
        {
            if (currentStamina < maxSTA)
            {
                AdjustStamina(-0.08f);  
            }
        }
    }

    public void AdjustShield(float change)
    {
        currentShieldUp -= change;
    }

    private void ShieldRecovery()
    {
        if (!equipmentController.isShieldUp)
        {
            if (currentShieldUp < shieldUpMax)
            {
                AdjustShield(-0.02f);
                isShieldRecharging = true;
            }
            else isShieldRecharging = false;
        }
        else isShieldRecharging = false;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void RecoverDamage(int damage)
    {
        currentHealth += damage;
    }
}
