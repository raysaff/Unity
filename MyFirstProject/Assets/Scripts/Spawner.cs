using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    [SerializeField] private Transform _location;
    void Start()
    {
        Instantiate(_obj, _location.position, _location.rotation);
    }
}
