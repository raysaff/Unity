using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    private bool hunt = false;
                     public int health = 100;
                     private float _speed = 3;
    private void Awake() => health = 100;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
    private void Update()
    {
        //if (!hunt) return;
        //else
        //{
        //    var direction = _target.position - transform.position;
        //    var newDirection = Vector3.RotateTowards(transform.forward, direction, _speed * Time.deltaTime, 0f);
        //    transform.rotation = Quaternion.LookRotation(newDirection);
        //    transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        //}
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
    }

}
