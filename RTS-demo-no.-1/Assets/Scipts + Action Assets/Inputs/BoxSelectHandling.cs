using RyanHipplesArchitecture.SO_Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class BoxSelectHandling : MonoBehaviour
{
    private Controls controls;

    // in RTS games, TapSelecting nothing deselects stuff.
    [SerializeField] private GameEvent onDeselectEverything;

    private float minMouseTravelPixelsForBoxSelect;

    private Coroutine boxSelectCoroutine;

    private Vector3 _startingMousePosition;
    private float _distanceTraveledByMouse;

    #region Default Methods

    private void Awake()
    {
        controls = new Controls();
        minMouseTravelPixelsForBoxSelect = GetComponent<TapSelectHandling>().MaxMouseTravelPixelsForTapSelect;
    }

    private void OnEnable()
    {
        controls.BattlefieldControl.Enable();

        controls.BattlefieldControl.BoxSelect.started += OnBoxSelectStarted;
        controls.BattlefieldControl.TapSelect.performed += OnBoxSelect;
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

        controls.BattlefieldControl.BoxSelect.started -= OnBoxSelectStarted;
        controls.BattlefieldControl.TapSelect.performed -= OnBoxSelect;
        controls.BattlefieldControl.BoxSelect.canceled  -= OnBoxSelectCanceled;





    }

    #endregion
    #region Custom Methods


        private bool MouseTraveledFarEnough()
    {
        _distanceTraveledByMouse = Vector3.Distance(_startingMousePosition, Input.mousePosition);
        return _distanceTraveledByMouse > minMouseTravelPixelsForBoxSelect;
    }

    private void OnBoxSelectStarted(InputAction.CallbackContext context)
    {
        
        _startingMousePosition = Input.mousePosition;
        boxSelectCoroutine = StartCoroutine(BoxSelectCoroutine());
        

    }

    private void OnBoxSelect(InputAction.CallbackContext context)
    {

    }


    private void OnBoxSelectCanceled(InputAction.CallbackContext context)
    {

       // Debug.Log($"ONBOXSELECT_cancel");

    }

    public IEnumerator BoxSelectCoroutine()
    {
        while (false /* BOXSELECT RUNNING */)
        {
            Debug.Log($"ONBOXSELECT_Coroutine");
            yield return null;
        }
        yield break;
    }




    #endregion
}
