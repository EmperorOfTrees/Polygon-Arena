using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : PlayerEquipment
{
    public Sword() : base(EquipmentInterface.eType.Sword)
    {

    }

    [SerializeField] private GameObject bladePoint;
    [SerializeField] private CapsuleCollider2D tipCollider;
    [SerializeField] private BoxCollider2D bladeCollider;
    public Vector3 previousPos;
    private float bladeSpeed;
    [SerializeField] private int bladeDamage;
    [SerializeField] private int tipDamage;
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
        return bladeSpeed;
    }

    public int GetBladeDamage()
    {
        return bladeDamage;
    }

    public int GetTipDamage()
    {
        return tipDamage;
    }
}
