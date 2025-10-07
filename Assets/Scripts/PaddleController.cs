using Unity.VisualScripting;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private BallController _ballControl;
    
    public JoystickForMovement Joystick;
    public BallController Ball;

    private float _inputX;
    private Rigidbody _rb;
    private const float _borderPosition = 35f;

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

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.06f, 2.06f), transform.position.y,
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
