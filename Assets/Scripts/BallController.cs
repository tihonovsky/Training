using UnityEngine;


public class BallController : MonoBehaviour
{
    [HideInInspector] public bool IsActiveBall = false;
    [SerializeField] public Rigidbody2D _rigitBody;
    [SerializeField] private float _ballSpeed;
    
    private GameManager _gameManager;
    
    public void Init(GameManager gm)    
    {
        _gameManager = gm;
    }
    private void Awake()
    {
        _rigitBody = GetComponent<Rigidbody2D>();
        _rigitBody.bodyType = RigidbodyType2D.Kinematic;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (_gameManager != null)
        {
            _gameManager.HandleBallCollision(coll, this);
        }
    }
    
    public void DestroyBall()
    {
        if (_rigitBody != null)
        {
            _rigitBody.bodyType = RigidbodyType2D.Kinematic;
        }

        IsActiveBall = false;

        gameObject.SetActive(false);
        Destroy(gameObject, 0.05f);
    }

    public void BallActivate()
    {
        IsActiveBall = true;

        if (_rigitBody != null)
        {
            _rigitBody.bodyType = RigidbodyType2D.Dynamic;
            _rigitBody.linearVelocity = new Vector2(0, _ballSpeed);
        }
    }
}