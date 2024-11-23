using System.Collections;
using System.Collections.Generic;
using Systems.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum Game_State
{
    MainMenu,
    Playing,
    Paused,
}

public class GameManager : PersistentSingleton<GameManager>
{
    public int score;
    //SceneLoader(which scene are we in, going to the next scene, going back to menu), LevelManager(Handles level elements, duration, spawn rules, etc),
    //SpawnManager(takes care of spawn rules), PlayerManager(manage persistent data about the player, upgrades, health, etc)
    public Game_State CurrentState = Game_State.MainMenu;
    private SceneLoader sceneLoader;
    // Start is called before the first frame update

    public static void StartGame()
    {
        Instance.score = 0;
        Time.timeScale = 1;
        GameLoadSceneGroup(1);
    }

    public static void NextLevel()
    {
        GameLoadSceneGroup(Instance.sceneLoader.currentSceneIndex + 1);
    }

    public static void Pause()
    {
        Instance.CurrentState = Game_State.Paused;
        Time.timeScale = 0;
    }

    public static void Resume()
    {
        Instance.CurrentState = Game_State.Playing;
        Time.timeScale = 1;
    }

    public static void ReturnToMenu()
    {
        Instance.CurrentState = Game_State.MainMenu;
        GameLoadSceneGroup(0);
    }

    public static void ExitGame()
    {
#if !UNITY_EDITOR
            Application.Quit();
#else
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    static async void GameLoadSceneGroup(int index)
    {
        await Instance.sceneLoader.LoadSceneGroup(index);
    }


    void Start()
    {
        sceneLoader = FindAnyObjectByType<SceneLoader>();
    }

    
}
