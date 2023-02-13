using RyanHipplesArchitecture.SO_Events;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// All behaviour of a Selection Frame, UI element that tracks it's partner Selectable entity.
/// Handles UI related stuff, toggles it's visibility depending on if object is selected.
/// </summary>
public class SelectionFrame : MonoBehaviour
{

    [SerializeField]
    private float frameSizeModifier,
                 frameCornerSizeModifier;

    [SerializeField] private List<GameObject> frameCorners;
	public List<Image> frameCornerImages;

	private RectTransform rectTransform;
	private GameEventListener onIsSelectedSetEventListener;

	private isSelectable selectedThing;

    private Vector3 _minimumScale, 
					_newScale;

    /// <summary>
    /// Selectable object tracked by the frame. Can only be set once.
    /// </summary>
    public isSelectable SelectedThing
	{
		get => selectedThing;
		set 
		{
			if (selectedThing) return;
			selectedThing = value;
		}
	}

    #region Built-in + Custom Start


    private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		onIsSelectedSetEventListener = GetComponent<GameEventListener>();

    }

	private void OnEnable()
	{

	}

    /// <summary>
    /// This is a custom Start, to avoid race conditions
    /// Whatever instantiates Selection Frames will call this.
    /// </summary>

    public void SelectionFrameInit()
	{
		CacheCornerImageRefs();
		PlaceFrameCorners();

		onIsSelectedSetEventListener.Event = SelectedThing.OnIsSelectedSet;
		// setting event means GameEventListener needs resetting:
		onIsSelectedSetEventListener.enabled = false;
		onIsSelectedSetEventListener.enabled = true;


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
	
	public void DoToggleVisibility()
	{

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
