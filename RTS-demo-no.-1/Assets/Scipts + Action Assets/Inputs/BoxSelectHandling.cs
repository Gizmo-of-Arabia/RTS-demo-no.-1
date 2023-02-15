using RyanHipplesArchitecture.SO_Events;
using RyanHipplesArchitecture.SO_Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;


/// <summary>
/// Handles the "Hold to Box" Selecting feature. 
/// It's for selecting any number of selectable entities by drawing a box.
/// Uses the "slow tap" action, which is performed on release.
/// Sets SelectBoxCorner, SelectBoxCorner_Opposite while the button is held down.
/// Those vars help the UI Painter make an appropriate box.
/// TODO: Send start, stop event to UI Painter
/// TODO: Make a big shape on release that actually selects.
/// </summary>
public class BoxSelectHandling : MonoBehaviour
{
    private Controls controls;

    // in RTS games, TapSelecting nothing deselects stuff.
    [SerializeField] private GameEvent onDeselectEverything;

    [SerializeField]
    private Vector3Reference 
        SelectBoxCorner, SelectBoxCorner_Opposite;

    private float _minMouseTravelPixelsForBoxSelect;

    private Coroutine _boxSelectCoroutine;
    private Coroutine _hasCursorGoneFarEnoughChecker;



    private Vector3 _startingMousePosition;
    private float _distanceTraveledByMouse;
    private bool _hasCursorGoneFarEnough;
    private bool _isBoxSelectOngoing;

    #region Default Methods

    private void Awake()
    {
        controls = new Controls();
        _minMouseTravelPixelsForBoxSelect = GetComponent<TapSelectHandling>().MaxMouseTravelPixelsForTapSelect;
        _hasCursorGoneFarEnough = false;
    }

    private void OnEnable()
    {
        controls.BattlefieldControl.Enable();

        controls.BattlefieldControl.BoxSelect.started += OnBoxSelectStarted;
        controls.BattlefieldControl.TapSelect.performed += OnBoxSelect;




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

        controls.BattlefieldControl.BoxSelect.started -= OnBoxSelectStarted;
        controls.BattlefieldControl.TapSelect.performed -= OnBoxSelect;





    }

    #endregion
    #region Custom Methods


    private bool IsCursorFarEnough()
    {
        _distanceTraveledByMouse = Vector3.Distance(_startingMousePosition, Input.mousePosition);
        return _distanceTraveledByMouse > _minMouseTravelPixelsForBoxSelect;
    }

    private void OnBoxSelectStarted(InputAction.CallbackContext context)
    {
        _isBoxSelectOngoing = true;
        _hasCursorGoneFarEnough = false;

        _startingMousePosition = Input.mousePosition;

        _boxSelectCoroutine = StartCoroutine(BoxSelectCoroutine());
        _hasCursorGoneFarEnoughChecker = StartCoroutine(HasCursorGoneFarEnoughChecker());

        //Debug.Log("Box Select Started");
        

    }

    private void OnBoxSelect(InputAction.CallbackContext context)
    {
        _isBoxSelectOngoing = false;

        if (!_hasCursorGoneFarEnough)
        {
            return;
        }


        //TODO: HERE SPAWN THE BIG SHAPE AND SELECT STUFF
    }

    /// <summary>
    /// Sets Selection Box boundaries. Starting point is set once, 
    /// opposite corner can change every frame.
    /// </summary>
    public IEnumerator BoxSelectCoroutine()
    {
        SelectBoxCorner.Variable.SetValue(_startingMousePosition);
        while (_isBoxSelectOngoing)
        {
            SelectBoxCorner_Opposite.Variable.SetValue(Input.mousePosition);
            //Debug.Log($"Corner1: {SelectBoxCorner.Value}\n Corner2: {SelectBoxCorner_Opposite.Value}");
            yield return null;
        }
    }


    /// <summary>
    /// This one starts up whenever BoxSelect starts.
    /// Its only purpose is to check whether the cursor has moved far enough.
    /// It closes the door after that point, so that the input can't go back
    /// to using TapSelect (e.g. by dragging the cursor back to the starting position).
    /// </summary>
    public IEnumerator HasCursorGoneFarEnoughChecker()
    {
        while (_isBoxSelectOngoing)
        {
            if (IsCursorFarEnough())
            {
                _hasCursorGoneFarEnough = true;
                yield break;
            }
            yield return null;
        }
    }




    #endregion
}
