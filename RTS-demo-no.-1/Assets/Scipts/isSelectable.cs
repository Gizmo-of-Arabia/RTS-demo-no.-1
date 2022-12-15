using RyanHipplesArchitecture.SO_Events;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isSelectable : MonoBehaviour
{
    [SerializeField] private RuntimeSet_isSelectable allSelectables;

    [SerializeField] private GameEvent_int onNewSelectableCreated;

    [field: SerializeField] public Vector2 FrameDimensions { get; private set; }

    private void Awake()
	{
		
	}

	private void OnEnable()
	{

	}

	private void Start()
	{

		allSelectables.Add(this);
		onNewSelectableCreated.Raise(allSelectables.Items.Count - 1); // parameter is index of this in Runtime Set

	}

	private void OnDestroy()
	{
		allSelectables.Remove(this);

    }

}
