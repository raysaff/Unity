using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private AudioClip _explosion = null;
    private int _damage = 150;
    private float _rotSpeed = 40;
    private AudioSource _audioSource = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = GameSet.instance.volume;
    }
    private void Update()
    {
        transform.Rotate(new Vector3(_rotSpeed * Time.deltaTime, _rotSpeed * Time.deltaTime, _rotSpeed * Time.deltaTime));
    }

    private void Explosion()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        Destroy(gameObject, 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        _audioSource.PlayOneShot(_explosion);
        if (other.gameObject.CompareTag("Bullet")) Explosion();

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(_damage);
            Explosion();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            Explosion();
        }
    }
}
