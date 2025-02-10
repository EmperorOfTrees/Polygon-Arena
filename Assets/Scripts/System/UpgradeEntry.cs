using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Upgrades/Upgrade Entry")]

public class UpgradeEntry : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private string title;
    [SerializeField][TextArea(3, 5)] private string description;
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