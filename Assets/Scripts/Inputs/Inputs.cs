using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    public static Inputs instance;
    public Controls controls;


    public void Awake()
    {
        if (instance == null)
        {
        instance = this;
       

        }
        
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
controls.Disable();
    }

}
