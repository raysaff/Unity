using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    private float _speed = 3;
    
    private void Awake() => _health = 100;
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy(gameObject);
    }
    private void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            transform.rotation = Quaternion.LookRotation(other.gameObject.transform.position - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, other.gameObject.transform.position, _speed * Time.deltaTime);
        }
    }

}
