using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode KeyOne;
    [SerializeField] private KeyCode KeyTwo;
    [SerializeField] private Vector3 _moveDirection;

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyOne)) GetComponent<Rigidbody>().velocity += _moveDirection;
        if (Input.GetKey(KeyTwo)) GetComponent<Rigidbody>().velocity -= _moveDirection;
        if (Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKey(KeyCode.Q)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
