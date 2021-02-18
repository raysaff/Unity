using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    [SerializeField] private float _speed = 9;
    private Transform _target = null;
    private int _damage = 0;
    public void Init(int damage, Transform target)
    {
        _target = target;
        _damage = damage;
        Destroy(gameObject, 20);
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed*Time.deltaTime);
    }
}
