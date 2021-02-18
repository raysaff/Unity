using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _health = 100;
    //private bool _isAlive = true;
    private void Awake()
    {
        _health = 100;
    }
    public void TakeDamage(int damage)
    {
            _health -= damage;
            if (_health <= 0) Destroy(gameObject); 
    }
    //public void Death()
    //{
    //    _isAlive = false;
    //}

}
