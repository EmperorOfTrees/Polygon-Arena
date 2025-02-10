using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : PlayerEquipment
{
    public Weapon() : base(EquipmentInterface.eType.Sword)
    {

    }

    [SerializeField] protected GameObject bladePoint;
    [SerializeField] protected CapsuleCollider2D tipCollider;
    [SerializeField] protected BoxCollider2D bladeCollider;

    [SerializeField] protected float tipDamageModifier = 1;
    [SerializeField] protected float bladeDamageModifier = 1;
    [SerializeField] protected float speedDamageModifier = 1;

    protected Vector3 previousPos;
    protected float bladeSpeed;
    [SerializeField] protected int bladeDamage;
    [SerializeField] protected int tipDamage;
    protected override void Awake()
    {
        tipCollider = GetComponent<CapsuleCollider2D>();
        bladeCollider = GetComponent<BoxCollider2D>();
    }


    protected override void Update()
    {
        base.Update();
        bladeSpeed = ((bladePoint.transform.position - previousPos).magnitude) / Time.deltaTime;
        previousPos = bladePoint.transform.position;

    }

    public float GetSpeed()
    {
        return bladeSpeed * speedDamageModifier;
    }

    public int GetBladeDamage()
    {
        return (int)(bladeDamage * bladeDamageModifier);
    }

    public int GetTipDamage()
    {
        return (int)(tipDamage * tipDamageModifier);
    }


    public void SetTipDamage(int dmg)
    {
        tipDamage = dmg;
    }

    public void SetBladeDamage(int dmg)
    {
        bladeDamage = dmg;
    }
}
