using System;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _blockCounterText;
    [SerializeField] private TMP_Text _blockQuantityText;

    [Header("Windows")]
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _victoryWindow;
    
    [Header("GameManager")]
    [SerializeField] private GameObject[] _blockQuantity;
    [SerializeField] private PaddleController _platformControl;
    [SerializeField] private GameObject _ballPrefab;  
   
    [SerializeField] public GameObject _spawnBallParent;
    [SerializeField] public GameObject _ballSpawnPoint;
    [SerializeField] private MultiBall _multiBall;
    
    private int _blockCounter;
    private int _blockQuantityCount;
    private BallController _ballController;
    private int _activeBalls = 1;
    
    private void Awake()
    {
        _blockQuantityCount = _blockQuantity.Length; 
        _blockQuantityText.text = _blockQuantityCount.ToString();
    }

    private void Start()
    {
        SpawnBall();
    }

    public void AddBall()
    {
        _activeBalls++;
    }
    
    private void SpawnBall()
    {
        GameObject ballObj = Instantiate(_ballPrefab, _ballSpawnPoint.transform.position + new Vector3(0, 0.2f,0), Quaternion.identity, _spawnBallParent.transform);
        BallController ball = ballObj.GetComponent<BallController>();
        
        ball.Init(this);
        
        _ballController = ball;
        _platformControl.SetBall(ball);
    }
   
    public void HandleBallCollision(Collision2D coll, BallController ball)
    {
        if (coll.gameObject.CompareTag("Block"))
        {
            Destroy(coll.gameObject);

            _blockCounter++;

            _blockCounterText.text = _blockCounter.ToString();
            
            if (Random.Range(0, 2) == 0)
            {
                _platformControl.SpawnBonus(ball);
            }
        }

        if (coll.gameObject.CompareTag("LoseBorder"))
        {
            if (ball != null)
            {
                ball.DestroyBall();
            }

            _activeBalls--;

            if (_activeBalls <= 0)
            {
                _loseWindow.SetActive(true);
                Time.timeScale = 0f;
                _platformControl.Joystick.gameObject.SetActive(false);
            }
        }

        if (_blockCounterText.text == _blockQuantityText.text)
        {
            _victoryWindow.SetActive(true);

            Time.timeScale = 0f;

            _platformControl.Joystick.gameObject.SetActive(false);
        }
    }
}
