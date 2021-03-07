using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] Door _door=null;

    private void OnTriggerEnter(Collider other)
    {
            _door.isOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _door.isOpen = false;
    }
}
