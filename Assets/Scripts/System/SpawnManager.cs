using System.Collections;
using System.Collections.Generic;
using Systems.SceneManagement;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnManager : StaticInstance<SpawnManager>
{
    [SerializeField] private SpawnPoint[] spawnPoints;
    [SerializeField] private GameObject[] availableEnemies;
    [SerializeField] private int spawnCount;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float timer;
    [SerializeField] private float spawnInterval;
    private bool spawnPossible;
    public float difficulty;
    private int maxDifficulty; //TODO: implement, low priority
    private int nextSpawnPointIndex;
    private SpawnPoint nextSpawnPoint;
    private int nextEnemyIndex;
    private GameObject nextEnemy;
    // handle how many spawns are in the scene, randomly spawn enemies at active spawn points when there are less then max enemies
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(difficulty > availableEnemies.Length) difficulty = availableEnemies.Length;

        if (Instance.spawnCount >= maxEnemies)
        {
            spawnPossible = false;
        }
        else spawnPossible = true;
        if (timer >= spawnInterval)
        {
            SelectSpawnPoint();
            SelectNextEnemy();
            SpawnEnemy(nextSpawnPoint, nextEnemy);
        } 
    }

    private void SelectNextEnemy()
    {
        nextEnemyIndex = Random.Range(0,(int)Instance.difficulty);
        nextEnemy = availableEnemies[nextEnemyIndex];
    }

    private void SelectSpawnPoint()
    {
        nextSpawnPointIndex = Random.Range(0, spawnPoints.Length);
        if (!spawnPoints[nextSpawnPointIndex].active)
        {
            SelectSpawnPoint();
            return;
        }
        else if (spawnPoints[nextSpawnPointIndex].active)
        {
            nextSpawnPoint = spawnPoints[nextSpawnPointIndex];
            return;
        }
    }

    private void SpawnEnemy(SpawnPoint location, GameObject enemy)
    {
        if (spawnPossible)
        {
            Instantiate(enemy, location.Location(), location.transform.rotation);
            AddToCount();
        }
        timer = 0;
    }

    public static void AddToCount()
    {
        if (Instance.spawnCount < Instance.maxEnemies)
        {
            Instance.spawnCount++;
        }
        else return;
    }
    public static void SubtractFromCount()
    {
        if (Instance.spawnCount > 0)
        {
            Instance.spawnCount--;
            Instance.difficulty += 0.05f;
        }
        else return;
    }
}