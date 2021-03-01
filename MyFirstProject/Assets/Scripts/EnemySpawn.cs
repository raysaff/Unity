using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    [SerializeField] private Transform _location;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform[] _points = null;
    void Start()
    {
        var enemy = GameObject.Instantiate(_obj, _location.position, _location.rotation).GetComponent<Enemy>();
        enemy.Init(_points, _target);
    }


}
