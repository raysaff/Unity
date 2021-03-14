using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEggs : MonoBehaviour
{
    private float _speed = 5;
    private int _damage = 0;
    public void Init(int damage)
    {
        _damage = damage;
        Destroy(gameObject, 4);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
        Destroy(gameObject);
    }

}
