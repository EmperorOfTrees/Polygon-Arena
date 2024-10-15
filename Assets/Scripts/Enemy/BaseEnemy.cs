using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private int maxHitPoints;
    private int hitPoints;
    private bool tookDamage;
    private float timer;
    public bool dead;

    protected void Start()
    {
        hitPoints = maxHitPoints;
    }


    protected void Update()
    {
        if (hitPoints <= 0)
        {
            dead = true;
        }
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            tookDamage = false;
        }

    }
    public void TakeDamage(int damage)
    {
        if(!tookDamage)
        {
            hitPoints -= damage;
            tookDamage = true;
            timer = 1;

        }
    }

    public float GetMaxHP()
    {
        return maxHitPoints;
    }

    public float GetHP()
    {
        return hitPoints;
    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            if (collision.GetType() == typeof(CapsuleCollider2D))
            {
                TakeDamage((int)collision.GetComponent<Sword>().GetSpeed() * collision.GetComponent<Sword>().GetTipDamage());
                print("hit with the tip!");
            }
            else if (collision.GetType() == typeof(BoxCollider2D))
            {
                TakeDamage((int)collision.GetComponent<Sword>().GetSpeed() * collision.GetComponent<Sword>().GetBladeDamage());
                print("hit with the blade!");
            }
        }
    }
}
