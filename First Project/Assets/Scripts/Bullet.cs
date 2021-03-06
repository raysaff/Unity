﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    private float _speed = 30;
    private int _damage = 0;
    public void Init(int damage)
    {
        _damage = damage;
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<Boss>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

}
