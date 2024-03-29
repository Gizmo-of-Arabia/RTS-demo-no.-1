using RyanHipplesArchitecture.SO_Events;
using RyanHipplesArchitecture.SO_RuntimeSet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Set of methods for an entity that is selectable by a player.
/// Could be a unit, a building, etc.
/// </summary>
public class isSelectable : MonoBehaviour
{
	// Runtime Sets:
    [SerializeField] private RuntimeSet_isSelectable allSelectables;
    [SerializeField] private RuntimeSet_isSelectable SelectedSelectables;
	//

    [SerializeField] private GameEvent_int onNewSelectableCreated;

    [SerializeField] private GameEvent onIsSelectedSet_EventBlueprint;

    [field: SerializeField, Tooltip("Event raised when this is selected or deselected, unique -- instantiated from blueprint")]
    public GameEvent OnIsSelectedSet { get; private set; }
	
	[field: SerializeField] public Vector2 FrameDimensions	{ get; private set; }

	/// <summary>
	/// Holds info on whether the object is currently selected by the player.
	/// Raises an event upon being set, so that UI knows to update.
	/// </summary>
    public bool IsSelected 
	{ 
		get => isSelected; 
		set 
		{
            isSelected = value;
            OnIsSelectedSet.Raise();
            //Debug.Log("Raising onIsSelectedSet with arg: " + objectID);
			if (value)
			{
				SelectedSelectables.Add(this);
			}
        } 
	}

    private bool isSelected;

	#region Built-in

	private void Awake()
	{
        // this method clones the template event from the prefab
        // this way each Selectable object has its own unique event
        OnIsSelectedSet = Instantiate(onIsSelectedSet_EventBlueprint);

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

    public void DoDeselect()
    {
        IsSelected = false;
		SelectedSelectables.Remove(this);
    }








    #endregion

    #region Test

    public void TEST_isSelected()
	{

		IsSelected = IsSelected ? false : true;
	}







    #endregion



}
