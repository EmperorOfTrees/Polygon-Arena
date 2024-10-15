using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShotLogic : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private int damage;
    [SerializeField] private Transform Aim;
    [SerializeField] private float timeToLive;
    private TrailRenderer trail;
    private bool isReflected;
    private float timer;
    private Vector2 lastVelocity;
    private Vector2 direction;
    private Color reflectColor = new Color(0f, 0.725f, 1f, 1f);
    private Color reflectColorTrail = new Color(0f, 0.375f, 1f, 1f);
    private Gradient rGrad;
    
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
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (isReflected)
            {
                collision.GetComponent<BaseEnemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MCircle")
        {
            direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction.normalized * force;
            isReflected = true;
            timer = timeToLive;
            gameObject.GetComponent<SpriteRenderer>().color = reflectColor;
            trail.colorGradient = rGrad;
        }
    }
}
