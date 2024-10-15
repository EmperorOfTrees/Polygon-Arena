using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEquipment : MonoBehaviour
{
    public EquipmentInterface.eType myType;
    protected virtual void Awake()
    {

    }

    public PlayerEquipment(EquipmentInterface.eType myType)
    {
        this.myType = myType;
    }

    protected virtual void Update()
    {

    }


}
