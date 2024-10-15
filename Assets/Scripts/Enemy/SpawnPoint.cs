using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject player;
    public float playerDistance;
    public bool active;

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerStats>().gameObject;
        playerDistance = (player.transform.position - transform.position).magnitude;
        if (playerDistance < 20f)
        {
            active = false;
        }
        else active = true;
    }

    private void Update()
    {
        playerDistance = (player.transform.position - transform.position).magnitude;
        if (playerDistance < 20f)
        {
            active = false;
        }
        else active = true;
    }

    public Vector3 Location()
    {
        return transform.position;
    }
}
