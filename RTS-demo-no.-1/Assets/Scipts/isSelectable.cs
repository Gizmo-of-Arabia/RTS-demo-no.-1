using RyanHipplesArchitecture.SO_Events;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isSelectable : MonoBehaviour
{
    [SerializeField] private RuntimeSet_isSelectable AllSelectables;

    [SerializeField] private GameEvent OnNewSelectableCreated;

    [field: SerializeField] public Vector2 frameDimensions { get; private set; }

    private void Awake()
	{
		
	}

	private void OnEnable()
	{

	}

	private void Start()
	{

		AllSelectables.Add(this);

		// pass index of newly added object in the event
		OnNewSelectableCreated.Raise();

	}

	private void OnDestroy()
	{
		AllSelectables.Remove(this);

    }

}
