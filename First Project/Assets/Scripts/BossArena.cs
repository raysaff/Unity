using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArena : MonoBehaviour
{
    [SerializeField] private GameObject _wall = null;
    [SerializeField] private GameObject _elevetor = null;
    [SerializeField] private Boss _boss = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _wall.SetActive(true);
            StartCoroutine(ElevatorUp());
            _boss.anger = true;
        }
            
    }

    IEnumerator ElevatorUp()
    {
        while (_elevetor.transform.position.y < 0)
        {
            _elevetor.transform.position = new Vector3(_elevetor.transform.position.x, 
                                                       _elevetor.transform.position.y + 0.1f,
                                                       _elevetor.transform.position.z);
            yield return null;
        }
            
    }
}
