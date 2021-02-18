using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _rotSpeed = 40;
    private int _HP = 10;
    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotSpeed*Time.deltaTime,0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeCare(_HP);
            Destroy(gameObject);
        } 
    }
}
