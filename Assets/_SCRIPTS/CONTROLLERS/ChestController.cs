using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : TreasureController
{

    public override void DestroyTreasure()
    {
        GetComponent<Animator>().SetTrigger("OpenChest");  
    }

    public void FinishAnimation()
    {
        Destroy(gameObject);
    }

    protected override void Move()
    {
    }

   
}
