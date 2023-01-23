using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class BattlefieldControlInputHandler : MonoBehaviour
{
    private Controls controls;

    #region Default Methods

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.BattlefieldControl.Enable();
        controls.BattlefieldControl.TapSelect.performed += OnTapSelect;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        //Debug.Log(Input.mousePosition); // TEMP
    }

    private void OnDisable()
    {
        controls.BattlefieldControl.Disable();
        controls.BattlefieldControl.TapSelect.performed -= OnTapSelect;
    }

    #endregion
    #region Custom Methods

    private void OnTapSelect(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition))) {
            Debug.Log("Hit a thing");
        }
    }



    #endregion
}
