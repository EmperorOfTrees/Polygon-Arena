using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentWeapon
{
    None = 0,
    Sword = 1,
    Hoplite = 2,

}

[RequireComponent(typeof(RotateEquipment))]
public class EquipmentController : MonoBehaviour
{
    [SerializeField] private GameObject support;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject hoplite;

    private CurrentWeapon currentWeapon = CurrentWeapon.Sword;

    private RotateEquipment rotEquip;
    private PlayerStats playerStats;

    public bool isShieldUp;
    public bool isShieldTimedOut;
    private float shieldTimer;

    private void Awake()
    {
        rotEquip = GetComponent<RotateEquipment>();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    void Update()
    {
        ShowEquipment();
        CountdownManager();
    }

    private void FixedUpdate()
    {
        if (isShieldUp)
        {
            playerStats.AdjustShield(0.02f);
        }
    }
    private void ShowEquipment()
    {
        if (currentWeapon == CurrentWeapon.Sword)
        {
            if (hoplite.activeSelf) LeaveHoplite();

            if (Input.GetMouseButton(1))
            {
                if (playerStats.SU > 0)
                {
                    if (!isShieldTimedOut)
                    {
                        ShowShield();
                    }
                }
                else
                {
                    ShieldTimeOut();
                    ShowSword();
                }
            }
            else
            {
                ShowSword();
            }

        }
        else if (currentWeapon == CurrentWeapon.Hoplite)
        {
            ShowHoplite();
        }
    }

    private void ShowHoplite()
    {
        hoplite.SetActive(true);
        weapon.SetActive(false);
        support.SetActive(false);
    }

    private void LeaveHoplite()
    {
        hoplite.SetActive(false);
        weapon.SetActive(true);
        support.SetActive(true);
    }

    private void ShowSword()
    {
        weapon.GetComponent<SpriteRenderer>().enabled = true;
        weapon.GetComponent<BoxCollider2D>().enabled = true;
        weapon.GetComponent<CapsuleCollider2D>().enabled = true;
        support.GetComponent<SpriteRenderer>().enabled = false;
        support.GetComponent<BoxCollider2D>().enabled = false;
        isShieldUp = false;
    }

    private void ShowShield()
    {
        weapon.GetComponent<SpriteRenderer>().enabled = false;
        weapon.GetComponent<BoxCollider2D>().enabled = false;
        weapon.GetComponent<CapsuleCollider2D>().enabled = false;
        support.GetComponent<SpriteRenderer>().enabled = true;
        support.GetComponent<BoxCollider2D>().enabled = true;
        isShieldUp = true;
    }

    private void ShieldTimeOut()
    {
        shieldTimer = 5f;
        isShieldTimedOut = true;
    }

    private void CountdownManager()
    {
        if (shieldTimer > 0)
        {
            shieldTimer -= Time.fixedDeltaTime;
        }
        else if (shieldTimer <= 0)
        {
            isShieldTimedOut = false;
        }
    }

    public void SetCurrentWeapon(CurrentWeapon newWeapon)
    {
        currentWeapon = newWeapon;
    }
}
