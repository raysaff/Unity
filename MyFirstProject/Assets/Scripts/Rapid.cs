using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapid : MonoBehaviour
{
    private float _rotSpeed = 40;
    private float _starttime = 0;
    private void Update()
    {
        transform.Rotate(new Vector3(0, _rotSpeed * Time.deltaTime, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _starttime = Time.time;
            other.GetComponent<Player>().Rapid(_starttime); ;
            Destroy(gameObject);
        }
    }
}
