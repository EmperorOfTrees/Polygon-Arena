using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject shot;
    [SerializeField] private GameObject Pewer;
    [SerializeField] private Transform shotTran;

    [SerializeField] private LayerMask IgnoreMe;

    [SerializeField] private bool canFire;
    [SerializeField] private bool seesPlayer;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float raylength = 10f;

    [SerializeField] private int hitPoints;

    [SerializeField] private AudioClip[] shootSounds;

    private PolygonEnemy parent;
    private float timer;


    void Start()
    {
        parent = GetComponentInParent<PolygonEnemy>();
        canFire = true;
    }

    void Update()
    {
        if (!parent.dead)
        {
            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer > timeBetweenShots)
                {
                    canFire = true;
                    timer = 0;
                }
            }
            if (canFire)
            {
                if (seesPlayer)
                {
                    canFire = false;
                    Instantiate(shot, shotTran.position, Pewer.transform.rotation);
                    SFXManager.Instance.PlayRandomSoundFXClip(shootSounds, transform, 0.5f);
                }
            }
        }
        
    }
    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, shotTran.transform.position - transform.position, raylength, ~IgnoreMe);


        if (ray.collider != null)
        {
            seesPlayer = ray.collider.CompareTag("Player");

            if (seesPlayer)
            {
                Debug.DrawRay(transform.position, ray.point, Color.green);
            }
            else Debug.DrawRay(transform.position, ray.point, Color.red);
        }
        else seesPlayer = false;
    }
}
