using System.Collections;
using System.Collections.Generic;
using Systems.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : StaticInstance<LevelManager>
{
    // Manages a level, how long it has been running and when it ends, might keep tally of how well the player did, keeps track of win conditions
    // Start is called before the first frame update
    [SerializeField] private int scoreCondition;
    private int levelScore;
    [SerializeField] private float timeCondtion;
    [SerializeField] private float timer;
    private PlayerStats playerStats;
    private PlayerManager playerManager;

    void Start()
    {
        levelScore = 0;
        timer = 0;
        playerStats = FindFirstObjectByType<PlayerStats>();
        playerManager = FindFirstObjectByType<PlayerManager>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (playerStats.dead)
        {
            EndLevelLose();
        }
        
        if (timer > timeCondtion)
        {
            if (levelScore > scoreCondition)
            {
                EndLevelWin();
            }
        }
    }
    private void EndLevelLose() //TODO: Death end
    {
        GameManager.ReturnToMenu();
        playerManager.ResetStats();
    }

    private void EndLevelWin() //TODO: if winning elements are secured, show a win screen
    {
        GameManager.NextLevel();
        playerManager.GrabStats(playerStats);
    }
    public static void IncreaseScore(int addScore)
    {
        GameManager.Instance.score += addScore;
        Instance.levelScore += addScore;
    }
}
