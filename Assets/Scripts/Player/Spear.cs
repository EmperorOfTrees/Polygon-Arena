using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    [SerializeField] private Animator animator;

    protected override void Awake()
    {
        tipDamageModifier = 1.5f;
        bladeDamageModifier = 0.1f;
        speedDamageModifier = 0.6f;

    }
    protected override void Update()
    {
        base.Update();

    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
