using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionFrame : MonoBehaviour
{
	[SerializeField]
	private float frameSizeModifier,
				  frameCornerSizeModifier;

	[SerializeField]
	private List<GameObject> frameCorners;

	// public variables are assigned by frame generator
	public isSelectable selectedThing;

	private RectTransform rectTransform;

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
		// change size based on distance
		rectTransform.localScale = Vector3.one * frameSizeModifier / Vector3.Distance(Camera.main.transform.position, selectedThing.transform.position);

		// move this UI element on top of the selected object, from the POV of the camera
		transform.position = Camera.main.WorldToScreenPoint(selectedThing.transform.position);
	}

	private void OnDestroy()
	{

	}

	// ***
	// * 
	// ***

	private void PlaceFrameCorners()
	{
		//temp code:
		foreach (GameObject item in frameCorners)
		{
			item.transform.localScale = Vector3.one * frameCornerSizeModifier;
		}

		Vector2 fd = selectedThing.frameDimensions;

		frameCorners[0].transform.localPosition = new Vector2(-fd.x, fd.y);
		frameCorners[1].transform.localPosition = new Vector2(fd.x, fd.y);
		frameCorners[2].transform.localPosition = new Vector2(-fd.x, -fd.y);
		frameCorners[3].transform.localPosition = new Vector2(fd.x, -fd.y);



	}
}
