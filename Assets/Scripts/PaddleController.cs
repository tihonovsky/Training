using Unity.VisualScripting;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private BallController _ballControl;
    [HideInInspector] public float paddleWidth = 250f / 2f;
    
    public JoystickForMovement Joystick;
    public BallController Ball;

    private float _inputX;
    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Joystick.ReturnVectorDirection().x != 0 && !Ball.IsActiveBall)
        {
            _ballControl.BallActivate();
        }
        
        _inputX = Joystick.ReturnVectorDirection().x;

        transform.Translate(_inputX * _speed * Time.deltaTime, 0, 0);
        
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;
        
        float platformWidthWorld = paddleWidth /100f;
        float halfPlatformWidthWorld = platformWidthWorld / 2f;

        float halfScreenWidth = screenWidth / 2f;
        float minX = -halfScreenWidth + halfPlatformWidthWorld;
        float maxX = halfScreenWidth - halfPlatformWidthWorld;
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y,
            transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ball")
        {
            var paddlePosition = transform.position;
            var contactPoint = col.GetContact(0).point;
            var offset = paddlePosition.x - contactPoint.x;
            var width = col.otherCollider.bounds.size.x / 2;
            var rb = col.transform.GameObject().GetComponent<Rigidbody2D>();
            var currentAngle = Vector2.SignedAngle(Vector2.up, rb.linearVelocity);
            var bounceAngle = (offset / width) * 30;
            var newAngle = Mathf.Clamp(currentAngle + bounceAngle, -30, 30);
            var rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            rb.linearVelocity = rotation * Vector2.up * rb.linearVelocity.magnitude;
        }
    }
}
