using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Transform _bulletStartPosition;
    [SerializeField] private float _speed = 5;
                     private bool _fire = false;
                     private int _damage = 4;
                     private Vector3 _direction = Vector3.zero;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _fire = true;
        _direction.z = Input.GetAxis("Vertical");
        _direction.x = Input.GetAxis("Horizontal");

        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        //    _direction.z = 1;
        //else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        //    _direction.z = -1;
        //else _direction.z = 0;

        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //    _direction.x = -1;
        //else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //    _direction.x = 1;
        //else _direction.x = 0;
    }
    private void FixedUpdate()
    {
        if (_fire) 
            Fire();
        Move();
    }
    private void Move()
    {
        var speed = _direction * _speed * Time.fixedDeltaTime*3;
        transform.Translate(speed);
    }
    private void Fire()
    {
        var bullet = GameObject.Instantiate(_bulletPref,_bulletStartPosition.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.Init(_damage);
        _fire = false;
    }
  }
