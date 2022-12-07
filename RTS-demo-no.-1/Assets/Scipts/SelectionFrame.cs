using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionFrame : MonoBehaviour
{
	[SerializeField]
	private Sprite FrameCorner;

	// this has to track + follow selected thing
	public isSelectable selectedThing;

	private void Awake()
	{

	}

	private void OnEnable()
	{

	}

	private void Start()
	{

	}

	private void Update()
	{
		transform.position = Camera.main.WorldToScreenPoint(selectedThing.transform.position);
	}

	private void OnDestroy()
	{

	}
}
