using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private Transform _eggGun = null;
    [SerializeField] private GameObject _eggPrefab = null;
    [SerializeField] private AudioClip _final = null;
    [SerializeField] private GameObject _endGame = null;
                     private AudioSource _audioSource = null;
                     private Animator _animator = null;
                     public bool anger = false;
                     private int _health = 100;
                     private float _timer = 1f;
                     private float _speed = 3;
                     private int _damage = 5;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = GameSet.instance.volume;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!anger) return;

        var direction = _player.transform.position - transform.position;
        var newDirection = Vector3.RotateTowards(transform.forward, direction, _speed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            Fire();
            _timer = 2f;
        }
    }
    private void Fire()
    {
        var egg = GameObject.Instantiate(_eggPrefab, _eggGun.position, _eggGun.rotation).GetComponent<BossEggs>();
        egg.Init(_damage);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Death();
    }

    private void Death()
    {
        _animator.SetBool("Death", true);
        _audioSource.PlayOneShot(_final);
        _endGame.SetActive(true);
        Destroy(gameObject, 3);
    }
}
