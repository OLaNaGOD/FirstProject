using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopButtonSA", menuName = "Scriptable Objects/ShopButtonSA")]
public class ShopButtonSA : ScriptableObject
{
    public ShopItem[] shopItems;
}

[Serializable]
public class ShopItem
{
    public ClickType Type;
    public string name_ru;
    public string name_en;
    public Sprite _sprite;
}

public enum ClickType
{
    AutoClick,
    BaseClick,
}
