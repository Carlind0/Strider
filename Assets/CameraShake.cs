using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
   public static CameraShake instance;
    public float globalShakeForce = 0.2f;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
    }
    public void CameraShakeImpulse(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulse(globalShakeForce);
    }
}
