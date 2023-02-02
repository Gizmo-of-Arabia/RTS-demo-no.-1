using RyanHipplesArchitecture.SO_Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class BattlefieldControlInputHandler : MonoBehaviour
{
    private Controls controls;

    // in RTS games, TapSelecting nothing deselects stuff.
    [SerializeField] private GameEvent onDeselectEverything;

    [SerializeField] private float maxMouseTravelPixelsForTapSelect;

    #region Default Methods

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.BattlefieldControl.Enable();
        controls.BattlefieldControl.TapSelect.started += OnTapSelectStarted;
        controls.BattlefieldControl.TapSelect.performed += OnTapSelect;


        controls.BattlefieldControl.BoxSelect.performed += OnBoxSelect;
        controls.BattlefieldControl.BoxSelect.canceled  += OnBoxSelectCanceled;




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
        controls.BattlefieldControl.TapSelect.started -= OnTapSelectStarted;
        controls.BattlefieldControl.TapSelect.performed -= OnTapSelect;


        controls.BattlefieldControl.BoxSelect.performed -= OnBoxSelect;
        controls.BattlefieldControl.BoxSelect.canceled  -= OnBoxSelectCanceled;





    }

    #endregion
    #region Custom Methods

    Vector3 _startingMousePosition; 
    float _distanceTraveledByMouse;
    private void OnTapSelectStarted(InputAction.CallbackContext context)
    {
        _startingMousePosition = Input.mousePosition;
        //Debug.Log("TS Started");
    }

    private void OnTapSelect(InputAction.CallbackContext context)
    {
        onDeselectEverything.Raise();
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) return;

        _distanceTraveledByMouse = Vector3.Distance(_startingMousePosition, Input.mousePosition);
        Debug.Log(_distanceTraveledByMouse);
        if (_distanceTraveledByMouse > maxMouseTravelPixelsForTapSelect) return;
    
        //Debug.Log($"Hit a thing: {hit.collider.gameObject.name}");
        hit.collider.gameObject.GetComponent<isSelectable>().IsSelected = true;
    }

    private void OnBoxSelect(InputAction.CallbackContext context)
    {
       // Debug.Log($"ONBOXSELECT");

    }

    private void OnBoxSelectCanceled(InputAction.CallbackContext context)
    {

       // Debug.Log($"ONBOXSELECT_cancel");

    }


    #endregion
}
