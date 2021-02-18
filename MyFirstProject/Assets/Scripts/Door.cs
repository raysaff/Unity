using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    private void Update()
    {
        if (!isOpen) return;

        transform.Rotate(new Vector3(0, 90, 0));
        isOpen = false;

    }
}
