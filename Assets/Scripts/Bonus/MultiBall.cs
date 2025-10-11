using UnityEngine;

public class MultiBall : Bonus
{
    protected override void ApplyEffect()
    {
        foreach (BallController ballController in transform)
        {
            
        }
    }
}
