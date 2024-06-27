using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        instance = this;
    }

    #endregion
    // Code from
    //https://youtu.be/xppompv1DBg?si=wtIg9hWTdpSJaEFm
    public GameObject player;
    //Is needed to set reference to player for spawned enemies
}