using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacterController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private float speed;
    [SerializeField] private float regularSpeed = 4.0f;
    [SerializeField] private float sprintSpeed = 8.0f;
    [SerializeField] private float dashSpeed = 20.0f;

    [SerializeField] private float regularSpeedModifier = 1f;
    [SerializeField] private float sprintSpeedModifier = 1f;
    [SerializeField] private float dashSpeedModifier = 1f;

    private float joggerModSpeed = 1f;
    private float joggerModCost = 1f;


    [SerializeField] private float sprintCost = 6.0f;
    [SerializeField] private float sprintCostModifier = 1f;

    [SerializeField] private float sprintTimer;
    [SerializeField] private float dashTimer;
    [SerializeField] private float dashTimeOutTimer;

    private bool jogger = false;

    public bool sprintTimeOUT = false;
    public bool dashTimeOUT = false;
    public bool isDashing;
    public bool isSprinting;

    private Vector2 motionVector;

    [SerializeField] private AudioClip[] dashSounds;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        CheckSprint();
    }

    private void Start()
    {
        PlayerManager.Instance.SetPlayerControlReference(this);
    }

    private void Update()
    {
        motionVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Pause();
        }
        if (jogger)
        {
            joggerModCost = 0.5f;
            joggerModSpeed = 1.2f;
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
            if (playerStats.STA > 0)
            {
                if (!sprintTimeOUT)
                {
                speed = sprintSpeed * sprintSpeedModifier;
                playerStats.AdjustStamina((sprintCost * sprintCostModifier * joggerModCost) /50);
                isSprinting = true;
                }
            }
            else
            {
                SprintTimeOut();
                speed = regularSpeed * regularSpeedModifier * joggerModSpeed;
                isSprinting= false;
            }
        }
        else
        {
            speed = regularSpeed * regularSpeedModifier * joggerModSpeed;
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
        else if (dashTimer <= 0)
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
            rb2d.velocity = motionVector * (dashSpeed * dashSpeedModifier);
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

    public void SetRegularSpeedMod(float newMod)
    {
        regularSpeedModifier = newMod;
    }

    public void SetSprintCostMod(float newMod)
    {
        sprintCostModifier = newMod;
    }

    public void SetSprintSpeedMod(float newMod)
    {
        sprintSpeedModifier = newMod;
    }

    public void SetDashSpeedMod(float newMod)
    {
        dashSpeedModifier = newMod;
    }

    public void SetJogger(bool state)
    {
        jogger = state;
    }

    
}
