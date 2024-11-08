using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    [SerializeField] private GameObject[] menuSections;

    private void Awake()
    {
        GotoSection(0);
    }
    public void GotoSection(int index)
    {
        for (int i = 0; i < menuSections.Length; i++)
        {
            if (i != index)
            {
                menuSections[i].gameObject.SetActive(false);
            }
            else
            {
                menuSections[i].gameObject.SetActive(true);
            }
        }
    }
}
