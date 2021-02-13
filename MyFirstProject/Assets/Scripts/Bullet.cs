using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    private int _damage = 0;
    public void Init(int damage)
    {
        _damage = damage;

        Destroy(gameObject, 5);
    }
}
