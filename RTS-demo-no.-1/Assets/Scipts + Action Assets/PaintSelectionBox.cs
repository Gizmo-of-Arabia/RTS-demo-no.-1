using RyanHipplesArchitecture.SO_Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSelectionBox : MonoBehaviour
{

    [SerializeField]
    private Vector3Reference
        SelectBoxCorner, SelectBoxCorner_Opposite;

    private RectTransform _rectTransform;

    // additional corners of the selection box
    private Vector3 _BoxCorner;
    private Vector3 _BoxCorner2;

    private float _boxWidth;
    private float _boxHeight;
    private Vector2 _newSizeDelta;

    private Vector3 _globalBoxPosition;


    #region Default Methods

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        _rectTransform = GetComponentInChildren<RectTransform>();
    }

    private void Update()
    {
        SetSelectionBoxDimensions();
        
    }

    #endregion

    #region Custom Methods

    private void SetSelectionBoxDimensions ()
    {
        //Center of the box:
        _globalBoxPosition = GetMidPoint(SelectBoxCorner, SelectBoxCorner_Opposite);


        //Corners:
        _BoxCorner.x = SelectBoxCorner_Opposite.Value.x;
        _BoxCorner.y = SelectBoxCorner.Value.y;

        _BoxCorner2.x = SelectBoxCorner.Value.x;
        _BoxCorner2.y = SelectBoxCorner_Opposite.Value.y;

        //Dimensions based on corners:
        _boxWidth = Mathf.Abs(SelectBoxCorner.Value.x - SelectBoxCorner_Opposite.Value.x);
        _boxHeight = Mathf.Abs(SelectBoxCorner.Value.y - SelectBoxCorner_Opposite.Value.y);

        _newSizeDelta.x = _boxWidth;
        _newSizeDelta.y = _boxHeight;

        //Finally setting the actual properties:
        _rectTransform.transform.position = _globalBoxPosition;
        _rectTransform.sizeDelta = _newSizeDelta;
    }

    private Vector3 GetMidPoint(Vector3 vec1, Vector3 vec2)
    {
        return (vec1 + vec2) / 2;
    }



    #endregion

}
