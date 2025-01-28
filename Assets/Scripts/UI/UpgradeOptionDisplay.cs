using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeOptionDisplay : MonoBehaviour
{
    private UpgradeEntry myEntry;

    [SerializeField] private Image myIcon;
    [SerializeField] private Image myRarity;
    [SerializeField] private Image myBackground;
    [SerializeField] private Image myTextFieldBackground;
    [SerializeField] private TextMeshProUGUI myTitle;
    [SerializeField] private TextMeshProUGUI myDescription;

    public void SetOption(UpgradeType uType, int index)
    {
        myEntry = UIUpgradeRegistry.Instance.GetEntry(uType, index);
        DisplayEntry();
    }

    private void DisplayEntry()
    {
        myTitle.text = myEntry.Title;
        myDescription.text = myEntry.Description;
        myIcon.sprite = myEntry.Icon;
        // Temporary Solution, needs actual sprite and stuff for Rarity
        if (myEntry.Rarity == Rarity.Common)
        {
            myRarity.color = Color.grey;
        } 
        else if (myEntry.Rarity == Rarity.Uncommon)
        {
            myRarity.color = Color.green;
        }
        else if (myEntry.Rarity == Rarity.Rare)
        {
            myRarity.color = Color.blue;
        }
        else if (myEntry.Rarity == Rarity.Epic)
        {
            myRarity.color = Color.black;
        }
        // Might set background based on upgrade type, including textfield background

    }
}
