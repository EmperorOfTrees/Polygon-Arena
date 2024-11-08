using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonEnemy : BaseEnemy
{
    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (!dead)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        
    }
}
