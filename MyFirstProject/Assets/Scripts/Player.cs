using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Transform _bulletStartPosition;
                     private float _speed = 5;
                     private float _rotationSpeed = 45;
                     private bool _fire = false;
                     private int _damage = 25;
                     private Vector3 _direction = Vector3.zero;
                     public int health = 100;
                     public int _ammo = 21;
    private void Awake()
    {
        health = 100;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) _fire = true;
        _direction.z = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        if (_fire) Fire();
        Move();
    }
    private void Move()
    {
        transform.Translate(_direction * _speed * Time.fixedDeltaTime * 3);
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime, 0));
    }
    private void Fire()
    {
        var bullet = GameObject.Instantiate(_bulletPref,_bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<Bullet>();
        bullet.Init(_damage);
        _fire = false;
    }
    public void TakeCare(int HP)
    {
        health += HP;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        //if (_health <= 0) Destroy(gameObject);
    }
}
