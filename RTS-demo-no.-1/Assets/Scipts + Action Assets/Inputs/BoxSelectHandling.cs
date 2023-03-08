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
/// Informs Select Box painter on whether the Box Select procedure is ongoing.
/// Creates a Collider Mesh that selects selectables within the 
/// box drawn by the player.
/// </summary>
public class BoxSelectHandling : MonoBehaviour
{
    private Controls controls;

    // in RTS games, TapSelecting nothing deselects stuff.
    [SerializeField] private GameEvent onDeselectEverything;

    /// <summary>
    /// Raise this with a "true" arg to indicate that the Box Select procedure is starting
    /// (for example to UI classes). Raise with "false" for its end.
    /// </summary>
    [SerializeField] private GameEvent_bool onBoxSelectOngoing;

    [SerializeField] private MeshCollider selectionMeshCollider;
    private Mesh _selectionMesh;


    [SerializeField]
    private Vector3Reference 
        SelectBoxCorner, SelectBoxCorner_Opposite;

    [SerializeField] private FloatVariable HugeNumber;

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
        controls.BattlefieldControl.TapSelect.performed += OnBoxSelectPerformed;




    }

    private void Start()
    {
        _selectionMesh = new Mesh();
        //selectionMeshCollider.sharedMesh = _selectionMesh;


    }

    private void Update()
    {

    }

    private void OnDisable()
    {
        controls.BattlefieldControl.Disable();

        controls.BattlefieldControl.BoxSelect.started -= OnBoxSelectStarted;
        controls.BattlefieldControl.TapSelect.performed -= OnBoxSelectPerformed;





    }

    #endregion
    #region Custom Methods


    private bool IsCursorFarEnough()
    {
        _distanceTraveledByMouse = Vector3.Distance(_startingMousePosition, Input.mousePosition);
        return _distanceTraveledByMouse > _minMouseTravelPixelsForBoxSelect;
    }

    /// <summary>
    /// Is called when OnBoxSelect starts.
    /// Sets flags and raises events signifying that the whole "Box Select" procedure is on.
    /// Sets starting mouse position, starts all the necessary coroutines.
    /// </summary>
    private void OnBoxSelectStarted(InputAction.CallbackContext context)
    {
        _isBoxSelectOngoing = true;
        _hasCursorGoneFarEnough = false;
        onBoxSelectOngoing.Raise(true);

        _startingMousePosition = Input.mousePosition;

        _boxSelectCoroutine = StartCoroutine(BoxSelectCoroutine());
        _hasCursorGoneFarEnoughChecker = StartCoroutine(HasCursorGoneFarEnoughChecker());

        //Debug.Log("Box Select Started");


    }


    /// <summary>
    /// Is called when OnBoxSelect has been "performed" (so when it's all done).
    /// Raises onBoxSelectOngoing with the "false" arg (meaning it's over).
    /// Sets the flag for coroutines to false.
    /// If the cursor hasn't made it far enough, the actual SELECTION part is aborted.
    /// Finally, spawns a big box that selects things on collision.
    /// </summary>
    private void OnBoxSelectPerformed(InputAction.CallbackContext context)
    {
        onDeselectEverything.Raise();

        onBoxSelectOngoing.Raise(false);
        _isBoxSelectOngoing = false;

        if (!_hasCursorGoneFarEnough || !IsCursorFarEnough())
        {
            return;
        }

        //actual selecting:

        _selectionMesh.vertices = SelectionMeshVertices();
        _selectionMesh.triangles = SelectionMeshTriangles();
        selectionMeshCollider.sharedMesh = _selectionMesh;



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

    /// <summary>
    /// Sets the 5 Selection Mesh Vertices. First one is on the camera.
    /// Other four are projected in the direction of ScreenPointToRay at a large distance.
    /// The PointProjectedFromTheScreen's arg is a corner of the selection box for each of the 4 bottom vertices.
    /// </summary>
    /// <returns>Vector3 array of vertices for the Selection Mesh.</returns>
    private Vector3[] SelectionMeshVertices ()
    {
        Vector3[] _selectionMeshVertices = new Vector3[5];

        //top vertice of the _selectionMesh

        _selectionMeshVertices[0] = Camera.main.transform.position;

        //bottom vertices:

        _selectionMeshVertices[1] = PointProjectedFromTheScreen(SelectBoxCorner);
        _selectionMeshVertices[2] = PointProjectedFromTheScreen(new Vector3(SelectBoxCorner_Opposite.Value.x, SelectBoxCorner.Value.y, 0));
        _selectionMeshVertices[3] = PointProjectedFromTheScreen(SelectBoxCorner_Opposite);
        _selectionMeshVertices[4] = PointProjectedFromTheScreen(new Vector3(SelectBoxCorner.Value.x, SelectBoxCorner_Opposite.Value.y, 0));

        // DEBUG/TEST CODE, spawns spheres at vertice points:
        /*
        foreach (var item in _selectionMeshVertices)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = item;
        }
        */

        return _selectionMeshVertices;
    }

    /// <summary>
    /// Projects a point from the camera, in the direction of the given screen point. Uses HugeNumber to ensure a large distance.
    /// </summary>
    /// <param name="ScreenPoint"> Screen point from which the returned Vec3 is projected.</param>
    /// <returns> Returns a point at the end of a long straight line that starts in the camera, goes 
    /// in the direction of the given screen point, ends very far away.</returns>
    private Vector3 PointProjectedFromTheScreen(Vector3 ScreenPoint)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(ScreenPoint.x, ScreenPoint.y, HugeNumber.Value));
    }

    /// <summary>
    /// Calculates triangles for the selection mesh using vertex indexes.
    /// </summary>
    /// <returns>Int Array containing triangles for the Selection Mesh.</returns>
    private int[] SelectionMeshTriangles()
    {
        // 6 triangles
        int[] _selectionMeshTriangles = new int[]
        {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 1, // sides

            3, 2, 1,
            4, 3, 1  // bottom 
        };

        return _selectionMeshTriangles;
        
    }

    #endregion
}
