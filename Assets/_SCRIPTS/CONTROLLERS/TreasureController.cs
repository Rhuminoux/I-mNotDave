using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MovingObject
{
    public int value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
