using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _location;
    void Start()
    {
        Instantiate(_enemy, _location.position, _location.rotation);
    }
}
