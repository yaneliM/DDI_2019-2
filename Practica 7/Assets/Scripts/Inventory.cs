using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
	public delegate void OnItemChange();
	public OnItemChange onItemChange;
	public int space = 8;
	public List<Item> items = new List<Item>();

	public void Add(Item item)
	{
		if (items.Count < space)
		{
			items.Add(item);
			Debug.Log("add to list");
			if(onItemChange != null)
			{
				onItemChange.Invoke();
				Debug.Log("onItemChange");
			}
		}
		else
		{
			Debug.LogWarning("Spacio insuficiente en inventario");
		}
	}

	public void Remove(Item item)
	{
		if (items.Contains(item))
		{
			items.Remove(item);
			if(onItemChange != null)
			{
				onItemChange.Invoke();
			}
		}
		else
		{
			Debug.LogWarning("Item no esta en inventario");
		}
	}
}
