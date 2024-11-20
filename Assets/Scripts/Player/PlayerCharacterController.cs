using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerCharacterController : MonoBehaviour
{
    private PlayerStats playerStats;
    private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float regularSpeed = 4.0f;
    [SerializeField] private float sprintSpeed = 8.0f;
    [SerializeField] private float dashSpeed = 20.0f;
    [SerializeField] private float sprintCost = 6.0f;
    [SerializeField] private float sprintTimer;
    [SerializeField] private float dashTimer;
    [SerializeField] private float dashTimeOutTimer;
    public bool sprintTimeOUT = false;
    public bool dashTimeOUT = false;
    public bool isDashing;
    public bool isSprinting;

    private Vector2 motionVector;

    [SerializeField] private AudioClip[] dashSounds;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        rb2d = GetComponent<Rigidbody2D>();
        CheckSprint();
    }

    private void Update()
    {
        motionVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Pause();
        }
        
        Dash();
    }

    private void FixedUpdate()
    {
        Move();
        CheckSprint();
        CountdownManager();
    }

    private void CheckSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (playerStats.STA > 0 && !sprintTimeOUT)
            {
                speed = sprintSpeed;
                playerStats.AdjustStamina(sprintCost/50);
                isSprinting = true;
            }
            else
            {
                SprintTimeOut();
                speed = regularSpeed;
                isSprinting= false;
            }
        }
        else
        {
            speed = regularSpeed;
            isSprinting = false;
        }
    }
    private void SprintTimeOut()
    {
        sprintTimer = 5f;
        sprintTimeOUT = true;
    }

    private void CountdownManager()
    {
        if (sprintTimer > 0) 
        {
            sprintTimer -= Time.fixedDeltaTime;
        }
        else if (sprintTimer <= 0) 
        {
            sprintTimeOUT = false;
        }

        if (dashTimer > 0)
        {
            dashTimer -= Time.fixedDeltaTime;
        }
        else if (sprintTimer <= 0)
        {
            isDashing = false;
        }

        if (dashTimeOutTimer > 0)
        {
            dashTimeOutTimer -= Time.fixedDeltaTime;
        }
        else if (dashTimeOutTimer <= 0)
        {
            dashTimeOUT = false;
        }
    }

    private void Move()
    {
        if (!isDashing)
        {
            rb2d.velocity = motionVector * speed;
        }
        else if (isDashing)
        {
            rb2d.velocity = motionVector * dashSpeed;
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && !dashTimeOUT) 
        {
            isDashing = true;
            dashTimeOUT = true;
            dashTimer = 0.20f;
            dashTimeOutTimer = 5f;
            SFXManager.Instance.PlayRandomSoundFXClip(dashSounds, transform, 1f);
        }
    }
}
