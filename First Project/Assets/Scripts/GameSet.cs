using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSet : MonoBehaviour
{
    public static GameSet instance;

    public bool HUD = true;
    public float enemySpeed = 6;
    public float volume = 0.7f;

    private void Awake()
    {
        instance = this;
    }
}
