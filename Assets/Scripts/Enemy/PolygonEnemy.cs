using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonEnemy : BaseEnemy
{
    [SerializeField] float rotationSpeed;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
        if (!dead)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        
    }
}
