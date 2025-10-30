using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


public class PaddleController : MonoBehaviour
{
    [HideInInspector] public float paddleWidth = 250f / 2f;
    [SerializeField] private float _speed = 8f;
    [SerializeField] private BoxCollider2D _collider2D;
    [SerializeField] private BallController _ballControl;
    [SerializeField] private GameObject _bonusSizeMax;
    [SerializeField] private GameObject _bonusSizeMin;
    [SerializeField] private GameObject _bonusMultiBall;
    [SerializeField] private Transform _scene;
    
    public JoystickForMovement Joystick;
    
    private RectTransform _rectTransform;
    private Rigidbody _rb;
    private Camera _camera;
    private float _inputX;
    private float _screenHeight;
    private Coroutine _sizeRoutine;
    
    
    
    private void Awake()
    {
        _camera =  Camera.main;
    }

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rb = GetComponent<Rigidbody>();
        _bonusSizeMax.GetComponent<PadleSize>();
        
    }

    private void Update()
    {
        if (Joystick.ReturnVectorDirection().x != 0 && !_ballControl.IsActiveBall)
        {
            _ballControl.BallActivate();
        }
        
        _inputX = Joystick.ReturnVectorDirection().x;

        transform.Translate(_inputX * _speed * Time.deltaTime, 0, 0);
        
        _screenHeight = _camera.orthographicSize * 2f;
        float screenWidth = _screenHeight * _camera.aspect;
        
        float platformWidthWorld = paddleWidth /100f;
        float halfPlatformWidthWorld = platformWidthWorld / 2f;

        float halfScreenWidth = screenWidth / 2f;
        float minX = -halfScreenWidth + halfPlatformWidthWorld;
        float maxX = halfScreenWidth - halfPlatformWidthWorld;
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y,
            transform.position.z);
    }

    public void SetSize(PaddleSizes size)
    {
        switch (size)
        {
           case PaddleSizes.Max :
               paddleWidth = (float) size / 2;
               _rectTransform.sizeDelta = new Vector2((float)size, 50);
               _collider2D.size = new Vector2((float)size, 50);
               break;
           case PaddleSizes.Min :
               paddleWidth = (float) size / 2;
               _rectTransform.sizeDelta = new Vector2((float)size, 50);
               _collider2D.size = new Vector2((float)size, 50);
               break;
           case PaddleSizes.Normal :
               paddleWidth = (float) size / 2;
               _rectTransform.sizeDelta = new Vector2((float)size, 50);
               _collider2D.size = new Vector2((float)size, 50);
               break;
        }
        
    }

    public void SpawnBonus()
    {
        int type = Random.Range(0, 3);
        GameObject prefabToSpawn;
        switch (type)
        {
            case 0:
                prefabToSpawn = _bonusMultiBall;
                break;
            case 1:
                prefabToSpawn = _bonusSizeMax;
                break;
            case 2:
                prefabToSpawn = _bonusSizeMin;
                break;
            default:
                return;
        }

        Instantiate(prefabToSpawn, _ballControl.transform.position, Quaternion.identity, _scene.transform);
    }

    private void RestartSizeRimer(float duration)
    {
        if (_sizeRoutine != null)
        {
            StopCoroutine(_sizeRoutine);
        }

        _sizeRoutine = StartCoroutine(SizeResetCor(duration));
    }

    public void ActivateIncreaceBonus(float duration = 5f)
    {
        SetSize(PaddleSizes.Max);
        RestartSizeRimer(duration);
    }
    
    public void ActivateReduceBonus(float duration = 5f)
    {
        SetSize(PaddleSizes.Min);
        RestartSizeRimer(duration);
    }

    private IEnumerator SizeResetCor(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetSize(PaddleSizes.Normal);
        _sizeRoutine = null;
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
