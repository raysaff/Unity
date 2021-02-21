using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private int _damage = 150;
    private float _rotSpeed = 40;
    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotSpeed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")) Destroy(gameObject);

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
    }
}
