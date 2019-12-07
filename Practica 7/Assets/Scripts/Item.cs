using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Immidiate,
	Equipment,
	Weapon
}

[CreateAssetMenu(fileName = "Nuevo Item", menuName = "Inventario/Item")]
public class Item : ScriptableObject 
{
	new public string name = "Nuevo Item";
	public Sprite icon = null;

	public ItemType itemType = ItemType.Equipment;
	public string damageValue = "5";
	public string xpValue = "10";

	public virtual void Use()
	{
		Debug.Log("Usando item " + name);
		// Para ser sobre-escrito por otra clase
	}
}
