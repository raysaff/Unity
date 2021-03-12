using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPref; //здесь определяем какой объект появится
    [SerializeField] private Transform _bulletStartPosition; //здесь определяем, где ему появиться
    [SerializeField] private Transform _target = null; //цель для вращения туррели
                     private float _speed = 3;
                     private bool _fire = false;
                     private int _damage = 4;
                     private float _timer = 1f; // таймер, чтобы туррель не стреляла слишком часто
                     private AudioSource _audiosource = null;
    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //от таймера отнимаем дельтатайм
        _timer -= Time.deltaTime;

        //стреляем если игрок в коллайдере и таймер истёк
        if (_fire && _timer < 0)
        {
            Fire();
            _timer = 1f;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
            _fire = true;
        
    }
    private void OnTriggerStay(Collider other)
    {
        //блок кода для вращения туррели
        var direction = _target.position - transform.position;
        var newDirection = Vector3.RotateTowards(transform.forward, direction, _speed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
            _fire = false;
    }
    private void Fire()
    {
        var bullet = GameObject.Instantiate(_bulletPref, _bulletStartPosition.position, _bulletStartPosition.rotation).GetComponent<Bullet>();
        bullet.Init(_damage);
        _audiosource.Play();
    }
}
