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

    [SerializeField] private GameEvent OnIsSelectedSet_EventBlueprint;
	[field: SerializeField] public GameEvent OnIsSelectedSet { get; private set; }
	
	[field: SerializeField] public Vector2 FrameDimensions	{ get; private set; }
    public bool IsSelected 
	{ 
		get => isSelected; 
		set 
		{
            isSelected = value;
            OnIsSelectedSet.Raise();
            //Debug.Log("Raising onIsSelectedSet with arg: " + objectID);
        } 
	}

    public bool isSelected; // should be priv

	#region Built-in

	private void Awake()
	{
        // this method clones the template event from the prefab
        // this way each Selectable object has its own unique event
        OnIsSelectedSet = Instantiate(OnIsSelectedSet_EventBlueprint);

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
