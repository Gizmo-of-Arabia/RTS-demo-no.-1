using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectionFrame : MonoBehaviour
{
    #region Built-in + Custom Start

    [SerializeField]
    private float frameSizeModifier,
                 frameCornerSizeModifier;

    [SerializeField] private List<GameObject> frameCorners;
	public List<Image> frameCornerImages;

	private RectTransform rectTransform;

	private isSelectable selectedThing;
	private int selectedThingID;
	public isSelectable SelectedThing
	{
		get => selectedThing;
		set 
		{
			if (selectedThing) return;
			selectedThing = value;
		}
	}

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();

    }

	private void OnEnable()
	{

	}

	// this is a custom Start, to avoid race conditions
	// Whatever instantiates Selection Frames should call this.
	public void SelectionFrameInit()
	{
		CacheCornerImageRefs();
		PlaceFrameCorners();
		selectedThingID = SelectedThing.ObjectID;

		DisableAllCornerImages();
		//Debug.Log("Frame Init complete, my subject's ID: " + selectedThingID);
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
		ScaleBasedOnDistanceFromCamera();

		// move this UI element on top of the selected object, from the POV of the camera
		transform.position = Camera.main.WorldToScreenPoint(SelectedThing.transform.position);
	}

	private void OnDestroy()
	{

	}

    #endregion

    #region Custom



    // local vars:
    Vector3 _minimumScale, _newScale;
	
	private void ScaleBasedOnDistanceFromCamera()
    {
        _minimumScale = new Vector3(0.25f, 0.25f, 0.25f); // MAGIC NUMBER!
        _newScale = Vector3.one * frameSizeModifier / Vector3.Distance(Camera.main.transform.position, SelectedThing.transform.position);

		if (_newScale.x < _minimumScale.x)
		{
            rectTransform.localScale = _minimumScale;
            return;
		}
        rectTransform.localScale = _newScale;
    }

    private void PlaceFrameCorners()
	{
		//temp code:
		foreach (GameObject item in frameCorners)
		{
			item.transform.localScale = Vector3.one * frameCornerSizeModifier;
		}

		Vector2 _fd = SelectedThing.FrameDimensions;

		frameCorners[0].transform.localPosition = new Vector2(-_fd.x, _fd.y);
		frameCorners[1].transform.localPosition = new Vector2(_fd.x, _fd.y);
		frameCorners[2].transform.localPosition = new Vector2(-_fd.x, -_fd.y);
		frameCorners[3].transform.localPosition = new Vector2(_fd.x, -_fd.y);
	}
	
	public void DoToggleVisibility(int objectID)
	{
		if (objectID != selectedThingID) 
			return;

		if (selectedThing.IsSelected)
			EnableAllCornerImages();
		else
			DisableAllCornerImages();

    }

	private void CacheCornerImageRefs()
	{
		foreach (var _corner in frameCorners)
		{
			frameCornerImages.Add(_corner.GetComponent<Image>());
		}
	}

    private void EnableAllCornerImages()
    {
        foreach (var _image in frameCornerImages)
            _image.enabled = true;

    }

    private void DisableAllCornerImages()
    {
        foreach (var _image in frameCornerImages)
            _image.enabled = false;

    }


    #endregion
}
