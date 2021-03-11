using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab = null;
    [SerializeField] private AudioClip _plant = null;
                     private int _damage = 150;
                     private List<Collider> ObjectsInTrigger;
                     private AudioSource _audioSource = null;
    
    public void Init(int damage)
    {
        _damage = damage;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_plant);
        _audioSource.volume = GameSet.instance.volume;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);

            // Заполняем список объектами в радиусе взрыва, прямо перед самим взрывом.
            ObjectsInTrigger = new List<Collider>(Physics.OverlapSphere(gameObject.transform.position, 5f));

            // Бежим по списку отталкивая объекты в направлении от мины, а если это ещё и игрок или враг, то нанести им урон. 
            foreach (Collider inZone in ObjectsInTrigger)
            {
                if (inZone.GetComponent<Rigidbody>() != null)
                    inZone.GetComponent<Rigidbody>().AddForce(500 * (inZone.transform.position - gameObject.transform.position));

                if (inZone.CompareTag("Player"))
                    inZone.GetComponent<Player>().TakeDamage(_damage/2);
            }
            _audioSource.Play();
            var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
            Destroy(explosion, 2);
            Destroy(gameObject, 4);
        }   
    }
}
