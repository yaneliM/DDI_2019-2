using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour 
{
	public Item item;
	public Image icon;

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

		if(item != null)
		{
			icon.sprite = item.icon;
		}
	}
	
	public void AddItem(Item item)
	{
		this.item = item;
		icon.sprite = item.icon;
		icon.enabled = true;
	}

	public void ClearSlot()
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
	}

	public void RemoveFromInventory()
	{
		if(item != null)
		{
			inventory.Remove(item);
		}
	}

	public void UseItem()
	{
		if(item != null)
		{
			item.Use();
			RemoveFromInventory();
		}
	}
}
