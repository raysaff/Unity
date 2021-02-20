using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private Transform[] _points = null;
                     private NavMeshAgent navMeshAgent = null;
                     private int currentPoint = 1;
    public void Init(Transform[] points)
    {
        _points = points;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _health = 100;
    }
    private void Start()
    {
        if (_points[0] != null)
        {
            navMeshAgent.SetDestination(_points[0].position);
        }
    }
    private void Update()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && _points[0] != null)
        {
            currentPoint = (currentPoint + 1) % _points.Length;
            navMeshAgent.SetDestination(_points[currentPoint].position);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy(gameObject);
    }
 }
