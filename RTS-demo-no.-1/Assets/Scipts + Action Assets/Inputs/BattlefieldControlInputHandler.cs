using RyanHipplesArchitecture.SO_Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class BattlefieldControlInputHandler : MonoBehaviour
{
    private Controls controls;

    [SerializeField] private GameEvent onDeselectEverything;

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
        onDeselectEverything.Raise(); // in RTS games, TapSelecting nothing deselects stuff.
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) return;

        Debug.Log($"Hit a thing: {hit.collider.gameObject.name}");
        hit.collider.gameObject.GetComponent<isSelectable>().IsSelected = true;


        //TODO: here select the thing.



    }



    #endregion
}
