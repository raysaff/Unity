using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    private Vector3 _closed = Vector3.zero;
    private Vector3 _open = Vector3.zero;

    private void Awake()
    {
        _closed = transform.position;
    }

    private void Update()
    {
        if (!isOpen) transform.position = _closed;
        else transform.position = new Vector3(transform.position.x, transform.position.y-2, transform.position.z); 
    }
}
