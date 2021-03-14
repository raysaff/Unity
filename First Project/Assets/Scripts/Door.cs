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
        if (!isOpen) StartCoroutine(CloseDoor()); //transform.position = _closed;
        else StartCoroutine(OpenDoor()); //transform.position = new Vector3(transform.position.x, -2, transform.position.z); 
    }
    IEnumerator OpenDoor()
    {
        while (transform.position.y > -4)
        {
            transform.position = new Vector3(transform.position.x,
                                              transform.position.y - 0.01f,
                                              transform.position.z);
            yield return null;
        }
    }
    IEnumerator CloseDoor()
    {
        while (transform.position.y < 3)
        {
            transform.position = new Vector3(transform.position.x,
                                              transform.position.y + 0.01f,
                                              transform.position.z);
            yield return null;
        }
    }
}
