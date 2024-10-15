using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : PlayerEquipment
{
    public Shield() : base(EquipmentInterface.eType.Shield)
    {

    }

    protected override void Update()
    {
        base.Update();
    }
}
