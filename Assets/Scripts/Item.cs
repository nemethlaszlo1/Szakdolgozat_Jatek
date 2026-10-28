using UnityEngine;

[CreateAssetMenu(fileName = "UjTargy", menuName = "Inventory/Targy")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public string description;

    public bool isWeapon;
    public bool isConsumable;
    public bool isKey;

    public int healAmount;

    public int attackDamage;
    public float attackRange;
    public float attackSpeed;

    public int price;


}
