using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private GameObject _minePref;
    [SerializeField] private Transform _bulletStartPosition;
    [SerializeField] private int _health = 100;
                     private float _speed = 5;
                     private float _rotationSpeed = 45;
                     private bool _fire = false;
                     private bool _minePlanting = false;
                     private int _damage = 25;
                     private Vector3 _direction = Vector3.zero;
                     public int _ammo = 21;
    private void Awake() => _health = 100;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) _fire = true;
        if (Input.GetMouseButtonDown(1)) _minePlanting = true;
        _direction.z = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        if (_fire) Fire();
        if (_minePlanting) MinePlant();
        Move();
    }
    private void Move()
    {
        transform.Translate(_direction * _speed * Time.fixedDeltaTime * 3);
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime, 0));
    }
    private void MinePlant()
    {
        var mine = GameObject.Instantiate(_minePref, _bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<Mine>();
        mine.Init(_damage);
        _minePlanting = false;
    }
    private void Fire()
    {
        var bullet = GameObject.Instantiate(_bulletPref,_bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<Bullet>();
        bullet.Init(_damage);
        _fire = false;
    }
    public void TakeCare(int HP)
    {
        _health += HP;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        //if (_health <= 0) Destroy(gameObject);
    }
}
