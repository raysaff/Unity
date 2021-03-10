using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private int _damage = 150;
    private List<Collider> ObjectsInTrigger;
    public void Init(int damage) => _damage = damage;
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
            Destroy(gameObject);
        }

        
}
}
