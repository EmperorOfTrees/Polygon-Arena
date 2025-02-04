using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Sword
{
    protected override void Awake()
    {

    }
    protected override void Update()
    {
        base.Update();

    }

    public new float GetSpeed()
    {
        return bladeSpeed;
    }

    public new int GetBladeDamage()
    {
        return (int)(bladeDamage*0.1f);
    }

    public new int GetTipDamage()
    {
        return (int)(tipDamage*1.5f);
    }

}
