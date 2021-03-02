using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private Transform[] _points = null;
    [SerializeField] private Transform _target = null;
    [SerializeField] private Transform _eye = null;
                     private NavMeshAgent navMeshAgent = null;
                     private int currentPoint = 0;
                     private float _patrolSpeed = 3.5f;
                     private float _chaseSpeed = 6f;
                     private RaycastHit hit;
                     private bool raycast = false;

    public void Init(Transform[] points, Transform target)
    {
        _points = points;
        _target = target;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _health = 100;
    }
    private void Start()
    {  
        // При спавне враг сразу отправляется к нулевой точке или идёт на патруль.
        Patrol();
    }

    private void Update()
    {
        var direction = _target.position - _eye.position;
        raycast = Physics.Raycast(_eye.position, direction, out hit, 20);

        // Если в зоне видимости нет игрока.
        if (!raycast || !hit.collider.gameObject.CompareTag("Player"))
        {
            Invoke("Patrol", 2);
        } 
        else 
        {
            CancelInvoke("Patrol");
            Chase();
        }
    }

    private void Patrol()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.speed = _patrolSpeed;
            currentPoint = (currentPoint + 1) % _points.Length;
        }
        navMeshAgent.SetDestination(_points[currentPoint].position);
    }

    private void Chase()
    {
        navMeshAgent.speed = _chaseSpeed;
        navMeshAgent.SetDestination(_target.position);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy(gameObject);
    }
 }
