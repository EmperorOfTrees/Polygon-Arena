using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
    {
        MultiUp = 0,
        SingleUp = 1,
        ExclusiveUp = 2,
        ExclusiveUpOverride = 3,
    }

public class PowerUpTest : MonoBehaviour
{
    private void Start()
    {
        SetUpgrade(UpgradeType.MultiUp);
    }

    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private int specificUpgradeIndex = 0;

    private void SetUpgrade(UpgradeType type)
    {
        upgradeType = type;
        SetSpecificUpgrade(upgradeType);
    }

    public void SetUpgradeRandom()
    {
        int index = UnityEngine.Random.Range(0, Enum.GetValues(typeof(UpgradeType)).Length);
        SetUpgrade((UpgradeType)index);
    }

    private void SetSpecificUpgrade(UpgradeType type)
    {
        if (type == UpgradeType.MultiUp)
        {
            specificUpgradeIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(MultiUps)).Length);
        }
        else if (type == UpgradeType.SingleUp)
        {
            specificUpgradeIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(OneTimeUps)).Length);
        }
        else if (type == UpgradeType.ExclusiveUp)
        {
           specificUpgradeIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ExclusiveUps)).Length);
        }
        else if (type == UpgradeType.ExclusiveUpOverride)
        {
            specificUpgradeIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ExclusiveUps)).Length);
        }
    }

    public void Upgrade(UpgradeType type, int index)
    {
        PlayerManager.Instance.Upgrade(type, index);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Upgrade(upgradeType, specificUpgradeIndex);
            Destroy(gameObject);
        }
    }
}
