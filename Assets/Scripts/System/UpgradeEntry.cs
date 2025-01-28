using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(menuName = "Upgrades/Upgrade Entry")]

public class UpgradeEntry : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private Rarity rarity;
    [SerializeField] private UpgradeType uType;
    [SerializeField] private int index;

    public Sprite Icon => icon;
    public string Title => title;
    public string Description => description;
    public Rarity Rarity => rarity;
    public UpgradeType UType => uType;
    public int Index => index;
}