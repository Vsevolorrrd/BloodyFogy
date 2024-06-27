using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    #region Singleton

    public static CameraShake CameraShakeInstance;

    private void Awake()
    {
        if (CameraShakeInstance != null)
        {
            Debug.LogWarning("More than one instance");
            return;
        }
        CameraShakeInstance = this;
    }
    #endregion
    public Animator anim;
    public void CamShake()
    {
        anim.SetTrigger("Shake");
    }
}// Animation that shakes the camera
