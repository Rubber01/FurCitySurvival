using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoSingleton<Player>
{   //zona attacco player
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] bool alreadyAttacked = false;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool enemyInAttackRange = false;
    [SerializeField] private bool hasCollideWithEnemy = false;
    private float timeCollidedWithEnemy=0;
    [SerializeField] private float outOfCombat=5;
    [SerializeField] private float healthRestoringOverTime;
    [SerializeField] private float tickRestoringHealth;
    //nemico
    [SerializeField] GameObject enemy;
    private GameObject lastCollisionObject; // Ultimo oggetto che ha colliso
    private bool collisonOccured = false;

    //alleati
    //[SerializeField] public GameObject[] allies;
    [SerializeField] public List<GameObject> allies = new List<GameObject>();

    //UI
    [SerializeField] private HealthBar healthBar;

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
    private bool _sellBlocks;
    private int _coinsToSpawn = 0;
    private AudioSource _audioSource;
    [SerializeField] private FloatingJoystick _joystick;

    [SerializeField] private AnimatorController _animatorController;

    private Rigidbody _rigidbody;

    private Vector3 _moveVector;
    [SerializeField] LoseBuisiness loseBuisiness;



private GameData _gameData;
    public GameData GameData
    {
        set => _gameData = value;
    }

    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _model = _animator.gameObject.transform;
        _stackedBlocks = new Stack<Transform>();
        _sellBlocks = false;
        _cameraSettings = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        allies = new List<GameObject>();
    }

    public void CheckAllies()
    {
        allies=GameObject.FindGameObjectsWithTag("Ally").ToList();
        
    }
    private void Start()
    {

        //healthBar = GetComponentInChildren<HealthBar>();

        healthBar.UpdateHealthBar(maxHealth, health);
        _audioSource = GetComponent<AudioSource>();
        Debug.Log("Audiosource: "+_audioSource.name);
        
    }

    /*public void Move(Vector3 newPosition)
    {
        //lock Y from changes
        _movePosition = new Vector3(newPosition.x, 0, newPosition.y);
        if (!Mathf.Approximately(_movePosition.sqrMagnitude, 0))
        {
            MoveAnim(_movePosition.sqrMagnitude);
            _isMoving = true;
        }
        //Debug.Log("Move pos: " + _movePosition);     
    }*/
    private void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        _moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _turnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);

            _animatorController.PlayRun();
        }

        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
        {
            _animatorController.PlayIdle();
        }

        _rigidbody.MovePosition(_rigidbody.position + _moveVector);
    }

    
   
    void FixedUpdate()
    {
        if (hasCollideWithEnemy && Time.time - timeCollidedWithEnemy >= outOfCombat)
        {
            hasCollideWithEnemy = false;
            StartCoroutine(RestoreHealth());
        }

        /*if (_isMoving)
        {
            _ridigbody.velocity = _movePosition * _moveSpeed;
            _model.rotation = Quaternion.Slerp(_model.rotation, Quaternion.LookRotation(_movePosition), _turnSpeed * Time.fixedDeltaTime);
            _isMoving = false;
        }
        else
        {
            if (_state != PlayerState.Idle) StopAnim();
        }*/
        Move();
        
    }
    IEnumerator RestoreHealth()
    {
        while(health<maxHealth)
        {
            Debug.Log("Restoring: "+healthRestoringOverTime+"hp");
            health += healthRestoringOverTime;
            healthBar.UpdateHealthBar(maxHealth, health);
            yield return new WaitForSeconds(tickRestoringHealth);
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
                    Debug.Log("alleati " + ally.gameObject.name+ " Target "+ collision.gameObject.name);
                    ally.GetComponent<AllyAI>().SetTarget(collision.gameObject);
                }
                //collisonOccured = true;
            }

    }
    private void OnCollisionStay(Collision collision)
    {
        
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                Debug.Log("Oggetto " + gameObject.name);
                Attack(collision.gameObject);
                break;
            case "CreditCoin":
                _audioSource.Play();
                PlayerManager.credits++;
                Destroy(collision.gameObject);
               break;
        /*     case "MetalScrap":
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
            */       
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasCollideWithEnemy = true;
            timeCollidedWithEnemy = Time.time;
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
        if (!alreadyAttacked)
        {
            _animatorController.PlayHit();
            enemy.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            
            Debug.Log("Attacco");
            alreadyAttacked = true;
            Debug.Log("Chiamo popup");
            //TextPopup.Create(new Vector3(transform.position.x+2, transform.position.y +1, transform.position.z) , damage);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        _animatorController.PlayStopHit();
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
        if (health <= 0)
        {
            
            _animatorController.PlayIsDying();
            Debug.Log("Chiamo PlayerDeath");
            loseBuisiness.PlayerDeath();
        }
    }

    //public void AddAlly(GameObject newAlly)
    //{
    //    allies[PlayerManager.currentHench] = newAlly;
    //}
}
