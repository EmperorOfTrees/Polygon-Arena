using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UpgradeOptionDisplay : MonoBehaviour
{
    [SerializeField] private UpgradeEntry myEntry;

    [SerializeField] private Image myIcon;
    [SerializeField] private Image myRarity;
    [SerializeField] private Image myBackground;
    [SerializeField] private Image myTextFieldBackground;
    [SerializeField] private Image myTitleFieldBackground;
    [SerializeField] private TextMeshProUGUI myTitle;
    [SerializeField] private TextMeshProUGUI myDescription;
    [SerializeField] private Button myButton;

    private UpgradeType myType;
    private int myIndex;

    public void SetOption(UpgradeType uType, int index)
    {
        myEntry = UIUpgradeRegistry.Instance.GetEntry(uType, index);

        DisplayEntry();

        myType = uType;
        myIndex = index;
    }

    private void DisplayEntry()
    {
        myTitle.text = myEntry.Title;
        myDescription.text = myEntry.Description;
        myIcon.sprite = myEntry.Icon;

        // Temporary Solution, needs actual sprite and stuff for Rarity
        // just switch it to sprite change when there are images
        switch (myEntry.Rarity)
        {
            case Rarity.Common:
                myRarity.color = Color.grey;
                break;
            case Rarity.Uncommon:
                myRarity.color = Color.green;
                break;
            case Rarity.Rare:
                myRarity.color = Color.blue;
                break;
            case Rarity.Epic:
                myRarity.color = Color.black;
                break;
        }

        // Temporary Solution, needs actual sprite and stuff for Background, need sprite for textfield
        // just switch it to sprite change when there are images
        switch (myEntry.UType)
        {
            case UpgradeType.MultiUp:
                myBackground.color = Color.black;
                break;
            case UpgradeType.SingleUp:
                myBackground.color = Color.red;
                break;
            case UpgradeType.ExclusiveUp:
                myBackground.color = Color.white;
                break;
            case UpgradeType.ExclusiveUpOverride:
                myBackground.color = Color.cyan;
                break;
        }
    }
    public void TurnOffThis()
    {
        myIcon.enabled = false;
        myBackground.enabled = false;
        myRarity.enabled = false;
        myTextFieldBackground.enabled = false;
        myTitleFieldBackground.enabled = false;
        myTitle.enabled = false;
        myDescription.enabled = false;
        myButton.enabled = false;
        myButton.gameObject.GetComponent<Image>().color = new Color(1,1,1,0);
    }
    public void TurnOnThis()
    {
        myIcon.enabled = true;
        myBackground.enabled = true;
        myRarity.enabled = true;
        myTextFieldBackground.enabled = true;
        myTitleFieldBackground.enabled = true;
        myTitle.enabled = true;
        myDescription.enabled = true;
        myButton.enabled = true;
        myButton.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }


    public void OnClickOption()
    {
        PlayerManager.Instance.Upgrade(myType, myIndex);
        GameManager.Resume();
    }
}
