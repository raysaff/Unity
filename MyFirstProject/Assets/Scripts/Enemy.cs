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
    [SerializeField] private int currentPoint = 0;

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
        // При спавне враг сразу отправляется к нулевой точке. 
        if (_points.Length > 1)
            Patrol();
        else
            navMeshAgent.SetDestination(_points[0].position);
    }

    private void Patrol()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            currentPoint = (currentPoint + 1) % _points.Length;
            navMeshAgent.SetDestination(_points[currentPoint].position);
        }
    }
    private void Chase()
    {
        navMeshAgent.speed = 10f;
        navMeshAgent.SetDestination(_target.position);
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        var direction = _target.position - _eye.position;
        var raycast = Physics.Raycast(_eye.position, direction, out hit, 20);

        if (!raycast || !hit.collider.gameObject.CompareTag("Player"))
        {
            navMeshAgent.speed = 3.5f;
            Invoke("Patrol", 2);
        }
        else
            Chase();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy(gameObject);
    }
 }
