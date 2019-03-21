using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void BombExploded(int id);
    public static event BombExploded OnBombExploded;

    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDeath;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void BombBlast(int id)
    {
        if (OnBombExploded != null)
        {
            OnBombExploded(id);
        }
    }

    public static void CheckGameOver()
    {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
    }
}