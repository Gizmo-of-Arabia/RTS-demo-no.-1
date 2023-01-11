using RyanHipplesArchitecture.SO_Events;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class isSelectable : MonoBehaviour
{
    #region Built-in

    [SerializeField] private RuntimeSet_isSelectable allSelectables;
	[SerializeField] private GameEvent_int onNewSelectableCreated;
	[SerializeField] private GameEvent_int onIsSelectedSet;
	
	[field: SerializeField] public Vector2 FrameDimensions { get; private set; }
    public int ObjectID 
	{ 
		get => objectID;
		private set => objectID = value;
	}
	public bool IsSelected 
	{ 
		get => isSelected; 
		set 
		{
            isSelected = value;
            onIsSelectedSet.Raise(objectID);
			//Debug.Log("Raising onIsSelectedSet with arg: " + objectID);
		} 
	}

    public bool isSelected; // should be priv
    private int objectID;

	private void Awake()
	{
		objectID = GetInstanceID();
		//Debug.Log(objectID);

		IsSelected = false;
	}

	private void OnEnable()
	{

	}

	private void Start()
	{

		allSelectables.Add(this);
		onNewSelectableCreated.Raise(allSelectables.Items.Count - 1); // argument is index of this in Runtime Set

	}

	private void OnDestroy()
	{
		allSelectables.Remove(this);

    }

    #endregion

    #region Custom

	public void TEST_isSelected()
	{

		IsSelected = IsSelected ? false : true;
	}







    #endregion



}
