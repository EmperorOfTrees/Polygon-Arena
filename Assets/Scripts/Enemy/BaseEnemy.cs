using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private int maxHitPoints;
    [SerializeField] private AudioClip[] swordSounds;
    private int hitPoints;
    private bool tookDamage;
    private float timer;
    public bool dead;
    public Material material;
    public float fadeLevel = 1f;


    protected void Start()
    {
        hitPoints = maxHitPoints;
        material = GetComponent<SpriteRenderer>().material;
    }


    protected void Update()
    {
        material.SetFloat("_Fade", fadeLevel);
        if (hitPoints <= 10)
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
            timer = 0.75f;
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
                TakeDamage((int)collision.GetComponent<Weapon>().GetSpeed() * collision.GetComponent<Weapon>().GetTipDamage());
                print("hit with the tip!");
                SFXManager.Instance.PlayRandomSoundFXClip(swordSounds, collision.gameObject.transform, 0.30f);
            }
            else if (collision.GetType() == typeof(BoxCollider2D))
            {
                TakeDamage((int)collision.GetComponent<Weapon>().GetSpeed() * collision.GetComponent<Weapon>().GetBladeDamage());
                print("hit with the blade!");
                SFXManager.Instance.PlayRandomSoundFXClip(swordSounds, collision.gameObject.transform, 0.20f);
            }
            
        }
    }
}
