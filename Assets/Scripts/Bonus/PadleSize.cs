using System;
using UnityEngine;

public class PadleSize : Bonus
{ 
    public BonusType SizeType;
    protected override void ApplyEffect(GameObject target)
    {
        PaddleController paddle = target.GetComponent<PaddleController>();

        switch (SizeType)
        {
            case BonusType.Increace:
                paddle.ActivateIncreaceBonus();
                break;
            case BonusType.Reduce:
                paddle.ActivateReduceBonus();
                break;
        }
    }
    
}
