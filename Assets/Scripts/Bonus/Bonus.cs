using System;
using UnityEngine;
 
public abstract class Bonus : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Paddle")
        {
            this.ApplyEffect();
        }

        if (col.gameObject.tag == "Paddle" || col.gameObject.tag == "LoseBorder")
        {
            Destroy(this.gameObject); 
        }
    }
    protected abstract void ApplyEffect();
}
