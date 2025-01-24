using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotLogic : MonoBehaviour
{
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip[] deflectSounds;

    [SerializeField] private float force;
    [SerializeField] private Transform Aim;
    [SerializeField] private int damage;
    [SerializeField] private float timeToLive;

    [SerializeField] private float reflectionBonus = 1f;

    private Rigidbody2D rb;
    
    private bool isReflected;
    private float timer;

    private Vector2 lastVelocity;
    private Vector2 direction;
    
    private Color reflectColor = new Color(0f, 0.725f, 1f, 1f);
    private Color reflectColorTrail = new Color(0f, 0.375f, 1f, 1f);
    private Gradient rGrad;
    private TrailRenderer trail;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();

        float alpha = 1.0f;
        rGrad = new Gradient();
        rGrad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(reflectColor, 0.0f), new GradientColorKey(reflectColorTrail, 0.58f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );

        rb = GetComponent<Rigidbody2D>();
        direction = transform.position - Aim.localToWorldMatrix.GetPosition();
        rb.velocity = direction.normalized * -force;
        timer = timeToLive;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject); return;
        }

    }
    private void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isReflected)
            {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(gameObject);
            SFXManager.Instance.PlayRandomSoundFXClip(hitSounds, this.transform, 0.25f);
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (isReflected)
            {
                collision.GetComponent<BaseEnemy>().TakeDamage((int)((float)damage*reflectionBonus));
                SFXManager.Instance.PlayRandomSoundFXClip(deflectSounds, this.transform, 0.1f);
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerObject")
        {
            direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction.normalized * force;
            isReflected = true;
            timer = timeToLive;
            gameObject.GetComponent<SpriteRenderer>().color = reflectColor;
            trail.colorGradient = rGrad;
            SFXManager.Instance.PlayRandomSoundFXClip(deflectSounds, this.transform, 0.25f);
        }

        if(collision.gameObject.layer.Equals("STRUCTURE"))
        {
            direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction.normalized * force;

            SFXManager.Instance.PlayRandomSoundFXClip(deflectSounds, this.transform, 0.05f);
        }
    }
}
