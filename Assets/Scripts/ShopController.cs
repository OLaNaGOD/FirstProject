using System.Collections.Generic;
using UnityEngine;
using YG;

public class ShopController : Singleton<ShopController>
{
    [SerializeField]
    GameObject _shopWindow;

    [SerializeField]
    GameObject _cellPrefab;

    [SerializeField]
    ShopButtonSA _shop;
    public List<GameObject> _items = new();
    private Dictionary<int, double> _damageDictionary = new();
    public double this[int id]
    {
        get { return _damageDictionary[id]; }
        set
        {
            _damageDictionary[id] = value;
            CalculateAutoDamage();
        }
    }

    private double _clickDamage;
    public double ClickDamage
    {
        get { return _clickDamage; }
        set
        {
            _clickDamage = value;
            BasicManager.Instance.ClickDamage = _clickDamage;
        }
    }

    private void Start() { }

    public void SetDamageDictionary(int id, double dmg)
    {
        this[id] = dmg;
    }

    public void CreateTool()
    {
        var tool = Instantiate(_cellPrefab, _shopWindow.transform);
        var cell = tool.GetComponentInChildren<ShopCell>();
        PushIndexInCell(cell, _items.Count);
        _items.Add(tool);
    }

    public void CreateTool(int index)
    {
        var tool = Instantiate(_cellPrefab, _shopWindow.transform);
        var cell = tool.GetComponentInChildren<ShopCell>();
        PushIndexInCell(cell, index);
        if (index >= 0 && index < YG2.saves.lvlItem.Count)
        {
            cell.Lvl = YG2.saves.lvlItem[index];
            cell.AwakingLoad();
        }
        else
        {
            YG2.saves.lvlItem.Add(cell.Lvl);
            YG2.saves.lvlAwakening.Add(cell.AwakeningLvl);
        }
        _items.Add(tool);
    }

    public void LoadTools()
    {
        for (int i = 0; i < YG2.saves.itemsCount; i++)
        {
            Debug.Log("Создание инструмента");
            CreateTool(i);
        }
    }

    private void PushIndexInCell(ShopCell cell, int index)
    {
        cell.Index = index;
    }

    public int GetItem()
    {
        var i = _items.Count;
        return i;
    }

    private void CalculateAutoDamage()
    {
        double damage = 0;
        foreach (var item in _damageDictionary)
        {
            damage += item.Value;
        }
        BasicManager.Instance.AutoDamage = damage;
    }
}
