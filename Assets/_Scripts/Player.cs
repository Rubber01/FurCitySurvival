using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{   //zona attacco player
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] bool alreadyAttacked = false;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool enemyInAttackRange = false;

    //nemico
    [SerializeField] GameObject enemy;
    private GameObject lastCollisionObject; // Ultimo oggetto che ha colliso
    private bool collisonOccured = false;

    //alleati
    [SerializeField] private GameObject[] allies;

    //UI
    private HealthBar healthBar;

    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _sellTime;
    [SerializeField] private float _coinTime;
    [SerializeField] private GameObject _weaponAttack;
    [SerializeField] private GameObject _weaponIdle;
    [SerializeField] private Transform _stackBlocksPoint;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _cameraLimitDistance;
    [SerializeField] private float _cameraStep;
    private CinemachineFramingTransposer _cameraSettings;
    private Stack<Transform> _stackedBlocks;
    private bool _isMoving = false;
    private Vector3 _movePosition;
    private Rigidbody _ridigbody;
    private Animator _animator;
    private Transform _model;   //animazioni varie body
    private const float _delta = 0.001f;
    private PlayerState _state;
    private bool _sellBlocks;
    private int _coinsToSpawn = 0;
    private AudioSource _audioSource;
    private GameData _gameData;
    public GameData GameData
    {
        set => _gameData = value;
    }

    private LevelManager _levelManager;
    public LevelManager LevelManager
    {
        set => _levelManager = value;
    }
    
    enum PlayerState
    {
        Idle,
        Moving
    }
    void Awake()
    {
        _ridigbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _model = _animator.gameObject.transform;
        _state = PlayerState.Idle;
        _stackedBlocks = new Stack<Transform>();
        _sellBlocks = false;
        _cameraSettings = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void CheckAllies()
    {
        allies = GameObject.FindGameObjectsWithTag("Ally");
    }
    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.UpdateHealthBar(maxHealth, health);
        _audioSource = GetComponent<AudioSource>();
    }

    public void Move(Vector3 newPosition)
    {
        //lock Y from changes
        _movePosition = new Vector3(newPosition.x, 0, newPosition.y);
        if (!Mathf.Approximately(_movePosition.sqrMagnitude, 0))
        {
            MoveAnim(_movePosition.sqrMagnitude);
            _isMoving = true;
        }
        //Debug.Log("Move pos: " + _movePosition);     
    }

    private void MoveAnim(float strength)
    {
        _state = PlayerState.Moving;
        if (Mathf.Approximately(strength, 1) && !_animator.GetBool("IsAttacking"))
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isRunning", false);
        }
    }

    private void AttackAnim(bool value)
    {
        _animator.SetBool("IsAttacking", value);
        _weaponAttack.SetActive(value);
        _weaponIdle.SetActive(!value);
    }

    private void StopAnim()
    {
        _state = PlayerState.Idle;
        _animator.SetBool("isWalking", false);
        _animator.SetBool("isRunning", false);
    }
    void FixedUpdate()
    {
        if (_isMoving)
        {
            _ridigbody.velocity = _movePosition * _moveSpeed;
            _model.rotation = Quaternion.Slerp(_model.rotation, Quaternion.LookRotation(_movePosition), _turnSpeed * Time.fixedDeltaTime);
            _isMoving = false;
        }
        else
        {
            if (_state != PlayerState.Idle) StopAnim();
        }
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        /*
        Debug.Log("Sto collidendo con: " + other.name);
        switch (other.tag)
        {
            case "Field":
                AttackAnim(true);
                Debug.Log("attacco");
                break;
            case "Stone":
                other.GetComponent<Resource>().Cut();
                Debug.Log("Stone");
                break;
            case "Block":
                CollectBlock(other.gameObject);
                Debug.Log("Block");
                break;
            case "Sell":
                SellBlocks(other.gameObject);
                Debug.Log("Sell");
                break;
            default:
                Debug.Log("Trigger enter not implemented: " + other.tag);
                break;
        }
        */
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Field":
                AttackAnim(false);
                break;
            case "Sell":
                _sellBlocks = false;
                break;
            default:
                Debug.Log("Trigger exit not implemented: " + other.tag);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
            //if (collisonOccured)
            //    return;

            // Controlla se l'oggetto colliso ha il tag "Enemy"
            if (collision.gameObject.CompareTag("Enemy"))
            {
            CheckAllies();
            foreach (GameObject ally in allies)
            {
                Debug.Log("alleati " + ally.gameObject.name);
                ally.GetComponent<AllyAI>().SetTarget(collision.gameObject);
            }
            //collisonOccured = true;
            }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Oggetto " + gameObject.name);
            Attack(collision.gameObject);
        }
    }
    /* switch (collision.gameObject.tag)
     {
         case "MetalScrap":
             _audioSource.Play();
             PlayerManager.metalScrapNumber++;
             Destroy(collision.gameObject);
             break;
         case "PlasticWaste":
             _audioSource.Play();
             PlayerManager.plasticWasteNumber++;
             Destroy(collision.gameObject);
             break;
         case "Metal":
             _audioSource.Play();
             PlayerManager.metalNumber++;
             Destroy(collision.gameObject);
             break;
         case "Plastic":
             _audioSource.Play();
             PlayerManager.plasticNumber++;
             Destroy(collision.gameObject);
             break;
         case "Chip":
             _audioSource.Play();
             PlayerManager.chipNumber++;
             Destroy(collision.gameObject);
             break;
     }*/


    /* Resource collection
    private void CollectBlock(GameObject block)
    {
        
        
        }

     
        if (_stackedBlocks.Count < _gameData.MaxStacked)
        {
            _levelManager.AddStone(1);
            _stackedBlocks.Push(block.transform);
            block.GetComponent<Block>().Stack(_stackBlocksPoint, _stackedBlocks.Count);
            SetCameraDistance(_stackedBlocks.Count);
        }
        else
        {
            block.GetComponent<Block>().SetLimitTexture();
        }
        
    }
    */

    private void SellBlocks(GameObject block)
    {
        _sellBlocks = true;
        StartCoroutine(SellBlock(_sellTime, block.GetComponentsInChildren<Transform>()[1]));
    }

    IEnumerator SellBlock(float time, Transform block)
    {
        _coinsToSpawn = 0;
        while (_sellBlocks)
        {
            if (_stackedBlocks.Count > 0)
            {
                _coinsToSpawn++;
                _levelManager.AddStone(-1);
                _stackedBlocks.Pop().GetComponent<Block>().Unstack(block);
                SetCameraDistance(_stackedBlocks.Count);
            }
            else
            {
                _sellBlocks = false;
            }
            yield return new WaitForSeconds(time);
        }
        if (_coinsToSpawn > 0)
        {
            StartCoroutine(SpawnCoins(_coinTime, block.position));
        }
    }

    IEnumerator SpawnCoins(float time, Vector3 startPoint)
    {
        yield return new WaitForSeconds(time);

        while (_coinsToSpawn > 0)
        {
            _coinsToSpawn--;
            _levelManager.SpawnCoin(startPoint);
            yield return new WaitForSeconds(time);
        }
        
    }
    private void SetCameraDistance(int blocksCount)
    {
        float newCameraDistance = blocksCount / _cameraLimitDistance + _cameraStep;
        if (newCameraDistance > _cameraLimitDistance) _cameraSettings.m_CameraDistance = newCameraDistance;
        else _cameraSettings.m_CameraDistance = _cameraLimitDistance;
    }
    private void Attack(GameObject enemy)
    {
        Debug.Log("sono dentro attacco");
        if (!alreadyAttacked)
        {

            enemy.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            //animazione.start
            Debug.Log("Attacco");
            alreadyAttacked = true;
            Debug.Log("Chiamo popup");
            //TextPopup.Create(new Vector3(transform.position.x+2, transform.position.y +1, transform.position.z) , damage);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void EnemyAttacked(GameObject enemyHit)
    {
        enemy = enemyHit;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(maxHealth, health);
        if (health <= 0) Destroy(gameObject);
    }
    
    
}
