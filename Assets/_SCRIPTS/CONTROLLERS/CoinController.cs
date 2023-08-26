using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CoinController : TreasureController
{
    public override void DestroyTreasure()
    {
        Destroy(gameObject);
    }

    protected override void Move()
    {
        transform.position += Vector3.up * Time.deltaTime * speed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
