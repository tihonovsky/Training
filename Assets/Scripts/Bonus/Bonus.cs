using System;
using UnityEngine;
using UnityEngine.Experimental.Playables;

public abstract class Bonus : MonoBehaviour
{
    private float _speed = 5f;

    private void Update()
    {
        transform.Translate(Vector2.down * (_speed * Time.deltaTime));
    }
    
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Paddle")
        {
            ApplyEffect(col.gameObject);
        }

        if (col.gameObject.tag == "Paddle" || col.gameObject.tag == "LoseBorder")
        {
            Destroy(gameObject); 
        }
    }
    protected abstract void ApplyEffect(GameObject target);
    
}
