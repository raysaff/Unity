using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] Door _door=null;
                     private bool isOn = false;

    private void Update()
    {
        _door.isOpen = isOn;
    }

    private void OnTriggerStay(Collider other)
    {
        isOn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOn = false;
    }
}
