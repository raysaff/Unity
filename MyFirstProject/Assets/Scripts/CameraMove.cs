using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    private Vector3 _offset = Vector3.zero;
    private Vector3 _angle = Vector3.zero;
    private void Awake()
    {
        _offset = transform.position - _player.transform.position;
        _angle = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position - _player.transform.forward * _offset.magnitude;
        transform.position = new Vector3(transform.position.x, _offset.y+1.5f, transform.position.z-1);
        transform.rotation = Quaternion.Euler(_angle.x, _player.transform.rotation.eulerAngles.y, _angle.z);
    }
}
