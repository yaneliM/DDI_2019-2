using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Inventario/Items/Potion")] // to add menu in unity, copied from item script
public class Potion : Item
{
    public int healthIncrease = 10;
    public override void Use() {
        base.Use();
        Debug.Log("Increasing health by "+healthIncrease);
    }
}
