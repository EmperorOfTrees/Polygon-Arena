using System.Collections;
using System.Collections.Generic;
using Systems.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum Game_State
{
    MainMenu = 0,
    Playing = 1,
    Paused = 2,
    Upgrading = 3,
}

public class GameManager : PersistentSingleton<GameManager>
{
    public int score;

    public Game_State CurrentState = Game_State.MainMenu;

    private SceneLoader sceneLoader;

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

    public static void Upgrading()
    {
        Instance.CurrentState = Game_State.Upgrading;
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
