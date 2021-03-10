using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPref = null;
    [SerializeField] private GameObject _minePref = null;
    [SerializeField] private GameObject _grenadePref = null;
    [SerializeField] private Transform _bulletStartPosition = null;
    [Space]
    [SerializeField] private GameObject _HUD = null;
    [SerializeField] private Text _UI_health = null;
    [SerializeField] private GameObject _endMsg = null;
    [SerializeField] private GameObject _gameMenu = null;

                     private Rigidbody _player;
                     private int _health = 100;
                     private int _damage = 25;
                     private float _startRapid = 0;
                     private bool _rapid = false;

                     private float _speed = 5;
                     private float _rotationSpeed = 90;
    [SerializeField] private Vector3 _direction = Vector3.zero;
                     private float _jumpForce = 500;

                     private bool _fire = false;
                     private bool _minePlanting = false;
                     private bool _grenade = false;
                     private bool _reload = true;
                     
                     private float _throwForce = 450;
                     private float _startSwing=0;
                     private float _swing = 0;
                     private Animator _animator = null;
                     private AudioSource _audiosource = null;


    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _health = 100;
        _player = GetComponent<Rigidbody>();
        _HUD.SetActive(GameSet.instance.HUD);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameMenu.SetActive(true);
            Time.timeScale = 0;
        }

        if (_health > 20) _UI_health.color = new Color(0, 255, 31);
        else _UI_health.color = new Color(255, 0, 0);

        _UI_health.text = _health.ToString();

        if (_rapid)
        {
            if (Time.time - _startRapid > 10)
            {
                _rapid = false;
                _speed = 5;
            }
        }
        
        // Стрельба.
        if (Input.GetMouseButtonDown(0)) _fire = true;

        // Минирование.
        if (Input.GetKeyDown(KeyCode.M)) _minePlanting = true;

        // Было интересно добавить разную силу броска гранаты. Чем дольше зажимаешь ПКМ, тем сильнее бросаешь.
        if (Input.GetMouseButtonDown(1))
        {
            _startSwing = Time.time;
            _throwForce = 450;
            _swing = 0;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _swing = (Time.time - _startSwing);

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
            _endMsg.SetActive(true);
            _endMsg.GetComponent<Text>().text = "Вы победили!";
            _animator.SetBool("EndGame", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(20);
    }

    private void Move()
    {
        if (_direction.z == 0)
            _animator.SetBool("Go", false);
        else
            _animator.SetBool("Go", true);

        _player.AddForce(0,0,_direction.z, ForceMode.VelocityChange);
        //transform.Translate(_direction * _speed * Time.fixedDeltaTime * 3);
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
        _audiosource.Play();
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

    public void Rapid(float startRapid)
    {
        _rapid = true;
        _speed *= 2;
        _startRapid = startRapid;
    }

    private void Death()
    {
        _animator.SetBool("Death", true);
        _endMsg.SetActive(true);
        _endMsg.GetComponent<Text>().text = "Вы погибли";

        Destroy(gameObject, 3);
    }
}
