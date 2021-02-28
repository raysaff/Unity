using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private Transform[] _points = null;
                     private NavMeshAgent navMeshAgent = null;
                     private int currentPoint = 0;
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
        // При спавне враг сразу отправляется к нулевой точке. 
        navMeshAgent.SetDestination(_points[0].position);
    }
    private void Update()
    {
        // Ходить дальше враг ничинает, только если есть ещё точки патрулирования.
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && _points.Length > 1)
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
