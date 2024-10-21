using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private GameObject PauseUI;
    
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


    }
}
