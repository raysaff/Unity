using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private int _damage = 150;
    private List<Collider> ObjectsInTrigger;
    private AudioSource _audioSource = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Init(int damage)
    {
        _damage = damage;
        Invoke("Explosion", 5);
        // Граната "взрывается" через 5 секунд.        
    }

    private void Explosion()
    {
        _audioSource.Play();
        // Заполняем список объектами в радиусе взрыва, прямо перед самим взрывом.
        ObjectsInTrigger = new List<Collider>(Physics.OverlapSphere(gameObject.transform.position, 5f));

        // Бежим по списку отталкивая объекты в направлении "от гранаты", а если это ещё и игрок или враг, то нанести им урон. 
        foreach (Collider inZone in ObjectsInTrigger)
        {
            if (inZone.GetComponent<Rigidbody>() != null)
                inZone.GetComponent<Rigidbody>().AddForce(500 * (inZone.transform.position - gameObject.transform.position));

            if (inZone.CompareTag("Player"))
                inZone.GetComponent<Player>().TakeDamage(_damage);

            if (inZone.CompareTag("Enemy"))
                inZone.GetComponent<Enemy>().TakeDamage(_damage);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        Destroy(gameObject, 2);
    }
}

