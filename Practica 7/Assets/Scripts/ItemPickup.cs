using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable 
{
	public Item item;
	private Inventory inventory;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		inventory = FindObjectOfType<Inventory>();
		if (inventory == null)
		{
			Debug.LogWarning("No se encontró el Inventario");
		}
	}

	public override void Interact()
	{
		//Pick up object, add to inventory
		Debug.Log("Levantando Item " + item.name);
		if(item.itemType == ItemType.Immidiate)
		{
			item.Use();
			Debug.Log("item.Use");
		}
		else
		{
			inventory.Add(item);
			Debug.Log("add.item");
		}
		Destroy(this.gameObject);
	}
}
