using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionFrame : MonoBehaviour
{
    [SerializeField]
    private float frameSizeModifier,
                 frameCornerSizeModifier;

    [SerializeField] private List<GameObject> frameCorners;

	private RectTransform rectTransform;

	private isSelectable selectedThing;
	public isSelectable SelectedThing
	{
		get { return selectedThing; }
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

	private void Start()
	{
        PlaceFrameCorners();
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

	// ***
	// * 
	// ***

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
}
