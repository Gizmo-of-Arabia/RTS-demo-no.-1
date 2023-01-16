using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldControlInputHandler : MonoBehaviour
{
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.BattlefieldControl.Enable();
    }

    private void Start()
    {
        
    }

    private void OnDisable()
    {
        controls.BattlefieldControl.Disable();
    }
}
