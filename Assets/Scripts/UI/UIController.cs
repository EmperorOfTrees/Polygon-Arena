using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject UpgradeUI;
    
    void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        Game_State state = GameManager.Instance.CurrentState;

        if (state != Game_State.Paused)
        {
            PauseUI.SetActive(false);
        }
        else PauseUI.SetActive(true);

        if(state != Game_State.Upgrading)
        {
            UpgradeUI.GetComponent<UpgradeMenu>().HideGraphics();
        }
        else UpgradeUI.GetComponent<UpgradeMenu>().ShowGraphics();

    }
}
