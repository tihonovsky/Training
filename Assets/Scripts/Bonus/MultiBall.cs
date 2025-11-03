using UnityEngine;

public class MultiBall : Bonus
{
    public int ballCount = 3;
    public GameObject ballPrefab;
    public float spreadAngle = 45f;
    
    protected override void ApplyEffect(GameObject target)
    {
        Vector3 spawnBonus = target.transform.position + Vector3.up * 0.3f;

        for (int i = 0; i < ballCount; i++)
        {
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
           
            GameObject newBall = Instantiate(ballPrefab, spawnBonus, Quaternion.identity, gameManager.transform);
            BallController ball = newBall.GetComponent<BallController>();
            
            if (ball != null)
            {
                ball.Init(gameManager);  
                gameManager.AddBall();
                ball.BallActivate();
            }
            
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Quaternion.Euler(0, 0, angle) * Vector2.up * rb.linearVelocity.magnitude;
           
        }
    }
}
