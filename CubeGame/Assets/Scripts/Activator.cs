using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private GameObject[] firstGroupe = null;
    [SerializeField] private GameObject[] secondGroupe = null;
    [SerializeField] private Activator button = null;
    [SerializeField] private Material normal = null;
    [SerializeField] private Material transparent = null;
    public bool canPush = true;


    private void OnTriggerEnter(Collider other)
    {
        if (canPush)
        {
            if (other.CompareTag("Cube") || other.CompareTag("Player"))
            {
                foreach (GameObject first in firstGroupe)
                {
                    first.GetComponent<Renderer>().material = normal;
                    first.GetComponent<Collider>().isTrigger = false;
                }
                foreach (GameObject second in secondGroupe)
                {
                    second.GetComponent<Renderer>().material = transparent;
                    second.GetComponent<Collider>().isTrigger = true;
                }
                GetComponent<Renderer>().material = transparent;
                button.GetComponent<Renderer>().material = normal;
                button.canPush = true;
            }
        }

    }
}
