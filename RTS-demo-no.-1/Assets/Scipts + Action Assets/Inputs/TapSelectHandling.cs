using RyanHipplesArchitecture.SO_Events;
using RyanHipplesArchitecture.SO_Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;


/// <summary>
/// Handles the "Tap" or "Click" Selecting feature. 
/// It's for selecting a single selectable entity with a click.
/// Uses the "slow tap" action, which is performed on release.
/// </summary>
public class TapSelectHandling : MonoBehaviour
{
    private Controls controls;

    // in RTS games, TapSelecting nothing deselects stuff.
    [SerializeField] private GameEvent onDeselectEverything;

    [field: SerializeField] public float MaxMouseTravelPixelsForTapSelect { get; private set; }

    [SerializeField] private FloatReference raycastRange;
    [SerializeField] private IntReference selectables_LayerMask;

    private Vector3 _startingMousePosition;
    private float _distanceTraveledByMouse;

    // this flag ensures you can't "go back" from BoxSelect
    private bool _mouseHasTraveledTooFar; 


    #region Default Methods

    private void Awake()
    {
        controls = new Controls();
        _mouseHasTraveledTooFar = false;
    }

    private void OnEnable()
    {
        controls.BattlefieldControl.Enable();
        controls.BattlefieldControl.TapSelect.started += OnTapSelectStarted;
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
        controls.BattlefieldControl.TapSelect.started -= OnTapSelectStarted;
        controls.BattlefieldControl.TapSelect.performed -= OnTapSelect;
        controls.BattlefieldControl.TapSelect.canceled -= OnTapSelectCanceled;


    }

    #endregion
    #region Custom Methods


    /// <summary>
    /// Saves starting mouse position for later detection of whether the mouse moved a lot.
    /// </summary>
    /// <param name="context">Action context, mandatory.</param>
    private void OnTapSelectStarted(InputAction.CallbackContext context)
    {
        _startingMousePosition = Input.mousePosition;
        //Debug.Log("TS Started");
    }


    /// <summary>
    /// Happens ON RELEASE OF THE BUTTON.
    /// Delesects everything. Standard procedure for RTS games.
    /// If the mouse moves past the maximum distance, "Tap Select" interpretation will no longer apply.
    /// Sends a raycast, if the raycast hits nothing, there's nothing to select.
    /// If all requirements were met, targeted selectable entity's IsSelected is set to true.
    /// </summary>
    /// <param name="context">Action context, mandatory.</param>
    private void OnTapSelect(InputAction.CallbackContext context)
    {
        onDeselectEverything.Raise();

        if (MouseTraveledTooFar())
        {
            return;
        }

        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, raycastRange, selectables_LayerMask))
        {
            return;
        }

        hit.collider.gameObject.GetComponent<isSelectable>().IsSelected = true;
    }

    private void OnTapSelectCanceled(InputAction.CallbackContext context)
    {
        _mouseHasTraveledTooFar = false;
    }

    private bool MouseTraveledTooFar()
    {
        _distanceTraveledByMouse = Vector3.Distance(_startingMousePosition, Input.mousePosition);
        return _distanceTraveledByMouse > MaxMouseTravelPixelsForTapSelect;
    }

    #endregion
}
