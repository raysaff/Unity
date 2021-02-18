using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Transform _bulletStartPosition;
    [SerializeField] private float _speed = 3;
    [SerializeField] private Transform _target = null;
    private bool _fire = false;
    private int _damage = 4;
    private float _timer = 2f;
    private void Update()
    {
        var direction = _target.position - transform.position;
        var newDirection = Vector3.RotateTowards(transform.forward, direction, _speed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        _timer -= Time.deltaTime;
        if (_fire && _timer < 0)
        {
            Fire();
            _timer = 2f;
        }
            
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
            _fire = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        _fire = false;
    }
    private void Fire()
    {
        var bullet = GameObject.Instantiate(_bulletPref, _bulletStartPosition.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Init(_damage, _target);
    }
}
