using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TreasureController : MovingObject
{
    public int value;

    public abstract void DestroyTreasure();
}
