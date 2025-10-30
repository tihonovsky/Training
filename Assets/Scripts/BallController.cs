using TMPro;
using UnityEngine;


public class BallController : MonoBehaviour
{
    [HideInInspector] public bool IsActiveBall = false;
    [SerializeField] private PaddleController _platformControl;
    [SerializeField] public Rigidbody2D _rigitBody;
    [SerializeField] private float _ballSpeed;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _victoryWindow;
    [SerializeField] private GameObject[] _blockQuantity;
    [Space]
    [SerializeField] private TMP_Text _blockCounterText;
    [SerializeField] private TMP_Text _blockQuantityText;
    
    private int _blockCounter;
    private int _blockQuantityCount;
   

    private void Awake()
    {
        _blockQuantityCount = _blockQuantity.Length; 
        _blockQuantityText.text = _blockQuantityCount.ToString();
    }

    private void Start()
    {
        _rigitBody = GetComponent<Rigidbody2D>();
        _rigitBody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void BallActivate()
    {
        IsActiveBall = true;
        _rigitBody.bodyType = RigidbodyType2D.Dynamic;
        _rigitBody.linearVelocity = new Vector2(0, _ballSpeed);
    }

   

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Block"))
        {
            Destroy(coll.gameObject);

            _blockCounter++;

            _blockCounterText.text = _blockCounter.ToString();
            
            if (Random.Range(0, 2) == 0)
            {
                _platformControl.SpawnBonus();
            }

        }

        if (coll.gameObject.CompareTag("LoseBorder"))
        {
            _loseWindow.SetActive(true);

            Time.timeScale = 0f;

            _platformControl.Joystick.gameObject.SetActive(false);
        }

        if (_blockCounterText.text == _blockQuantityText.text)
        {
            _victoryWindow.SetActive(true);

            Time.timeScale = 0f;

            _platformControl.Joystick.gameObject.SetActive(false);
        }
    }

}