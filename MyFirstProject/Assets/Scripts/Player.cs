using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private GameObject _minePref;
    [SerializeField] private GameObject _grenadePref;
    [SerializeField] private Transform _bulletStartPosition;
                     private Rigidbody _player;
                     private int _health = 100;
                     private int _damage = 25;

                     private float _speed = 5;
                     private float _rotationSpeed = 70;
                     private Vector3 _direction = Vector3.zero;
                     private float _jumpForce = 500;

                     private bool _fire = false;
                     private bool _minePlanting = false;
                     private bool _grenade = false;
                     private bool _reload = true;
                     
                     private float _throwForce = 450;
                     private float _start=0;
                     private float _swing = 0;
                     private Animator _animator = null;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = 100;
        _player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Стрельба.
        if (Input.GetMouseButtonDown(0)) _fire = true;

        // Минирование.
        if (Input.GetKeyDown(KeyCode.M)) _minePlanting = true;

        // Было интересно добавить разную силу броска гранаты. Чем дольше зажимаешь ПКМ, тем сильнее бросаешь.
        if (Input.GetMouseButtonDown(1))
        {
            _start = Time.time;
            _throwForce = 450;
            _swing = 0;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _swing = (Time.time - _start);

            if (_swing >= 5)
                _throwForce += 100;
            else
                _throwForce += _swing * 20;

            _grenade = true;
        }

        // Прыжок.
        if (Input.GetKeyDown(KeyCode.Space) && _player.position.y < 2.2f) Jump();

        _direction.z = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()
    {
        if (_player.position.y <2.2f) _animator.SetBool("Jump", false);
        if (_fire && _reload) Fire();
        if (_minePlanting) MinePlant();
        if (_grenade) FireInTheHole();
        Move();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _animator.SetBool("EndGame", true);
        }
    }

    private void Move()
    {
        if (_direction.z == 0)
            _animator.SetBool("Go", false);
        else
            _animator.SetBool("Go", true);

        transform.Translate(_direction * _speed * Time.fixedDeltaTime * 3);
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime, 0));
    }
    private void Jump()
    {
        _animator.SetBool("Jump", true);
        _player.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void MinePlant()
    {
        var mine = GameObject.Instantiate(_minePref, _bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<Mine>();
        mine.Init(_damage*4);
        _minePlanting = false;
    }

    private void Fire()
    {
        _animator.SetBool("Shoot", true);
        var bullet = GameObject.Instantiate(_bulletPref,_bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<Bullet>();
        bullet.Init(_damage);
        _fire = false;
        _reload = false;
        Invoke("Reload", 1);
    }

    private void Reload()
    {
        _reload = true;
        _animator.SetBool("Shoot", false);
    }

    private void FireInTheHole()
    {
        var grenade = GameObject.Instantiate(_grenadePref, _bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<Grenade>();
        grenade.Init(_damage*4);
        grenade.GetComponent<Rigidbody>().AddForce(_throwForce*transform.forward);
        grenade.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 100);
        _grenade = false;
    }

    public void TakeCare(int HP)
    {
        _health += HP;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Death();
    }
    private void Death()
    {
        //_animator.SetBool("Death", true);
        Destroy(gameObject, 2);
    }
}
