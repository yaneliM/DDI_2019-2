using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour 
{
	public Item item;
	public Image icon;
	public Text name, xp, damage,tag1,tag2;

	private Inventory inventory;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		tag1.enabled = false;
		tag2.enabled = false;
		inventory = FindObjectOfType<Inventory>();
		if (inventory == null)
		{
			Debug.LogWarning("No se encontró el Inventario");
		}

		if(item != null)
		{
			icon.sprite = item.icon;
			name.text = item.name;
			xp.text = item.xpValue;
			damage.text = item.damageValue;
			tag1.enabled = true;
			tag2.enabled = true;
		}
	}
	
	public void AddItem(Item item)
	{
		this.item = item;
		icon.sprite = item.icon;
		icon.enabled = true;
		name.text = item.name;
		name.enabled = true;
		xp.text = item.xpValue;
		xp.enabled = true;
		damage.text = item.damageValue;
		damage.enabled = true;

		tag1.enabled = true;
		tag2.enabled = true;
	}

	public void ClearSlot()
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		name.text = null;
		name.enabled = false;
		xp.text = null;
		xp.enabled = false;
		damage.text = null;
		damage.enabled = false;

		tag1.enabled = false;
		tag2.enabled = false;
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
