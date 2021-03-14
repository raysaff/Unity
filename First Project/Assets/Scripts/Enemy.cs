using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 25;
    [SerializeField] private Transform[] _points = null;
    [SerializeField] private Transform _target = null;
    [SerializeField] private Transform _eye = null;
    [SerializeField] private AudioClip[] _noises = null;
                     private NavMeshAgent navMeshAgent = null;
                     private int currentPoint = 0;
                     private float _patrolSpeed = 3.5f;
                     private float _chaseSpeed;
                     private RaycastHit hit;
                     private bool raycast = false;
                     private Animator _animator = null;
                     private AudioSource _audioSource = null;
                     private Vector3 _offsetOne = Vector3.zero;
                     private Vector3 _offsetTwo = Vector3.zero;
                     private float _stepTimer = 0.5f;
                     private float _stepTimerDown = 0;

    public void Init(Transform[] points, Transform target)
    {
        _points = points;
        _target = target;
    }

    private void Awake()
    {
        _offsetOne = transform.position;
        _offsetTwo = transform.position;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = GameSet.instance.volume;
        _chaseSpeed = GameSet.instance.enemySpeed;
        _animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        _health = 25;
        
    }
    private void Start()
    {  
        // При спавне враг сразу отправляется к нулевой точке или идёт на патруль.
        Patrol();
        _audioSource.Play();
    }

    private void Update()
    {
        _offsetTwo = transform.position;
        if (_offsetTwo != _offsetOne)
        {
            if (_stepTimerDown < 0)
            {
                _audioSource.PlayOneShot(_noises[1]);
                _stepTimerDown = _stepTimer;
            }
            else
            {
                _stepTimerDown -= Time.deltaTime;
            }
            _offsetOne = _offsetTwo;
        }
        

        var direction = _target.position - _eye.position;
        raycast = Physics.Raycast(_eye.position, direction, out hit, 20);

        // Если в зоне видимости нет игрока.
        if (!raycast || !hit.collider.gameObject.CompareTag("Player"))
            Patrol();
        else 
            Chase();
    }

    private void Patrol()
    {

        if (_points.Length == 1)
        {
            _animator.SetBool("Go", false);
        }
        else
        {
            _animator.SetBool("Go", true);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                navMeshAgent.speed = _patrolSpeed;
                _stepTimer = 0.5f;
                currentPoint = (currentPoint + 1) % _points.Length;
            }
            navMeshAgent.SetDestination(_points[currentPoint].position);
        }
 
    }

    private void Chase()
    {
        _animator.SetBool("Go", true);
        _stepTimer = 0.2f;
        navMeshAgent.speed = _chaseSpeed;
        navMeshAgent.SetDestination(_target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_noises[3]);
            collision.gameObject.GetComponent<Player>().TakeDamage(5);
        }
            
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Death();
    }
    public void Death()
    {
        _audioSource.PlayOneShot(_noises[2]);
        _animator.SetBool("Death", true);
        navMeshAgent.isStopped = true;
        Destroy(gameObject, 1);
    }
 }
