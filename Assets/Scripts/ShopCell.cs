using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopCell : MonoBehaviour
{
    [SerializeField]
    Text _nameText;

    [SerializeField]
    Text _priceText;

    [SerializeField]
    Text _statsAllText;

    [SerializeField]
    Text _statsInLvlText;

    [SerializeField]
    Text _lvltext;
    private int _lvl;

    [SerializeField]
    Button _buyButton;

    [SerializeField]
    Button _awakingButton;

    [SerializeField]
    GameObject _awakingWindow;

    [SerializeField]
    Image _toolsIcon;

    [SerializeField]
    GameObject _starPrefab;
    const double baseMod = (5d * 1.1);

    [SerializeField]
    ShopButtonSA _shop;
    int _index;
    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            GetShopItem(_shop.shopItems[Index]);
            SetDefaultValue();
        }
    }
    ShopItem _item;
    double _sumAllDamage;
    public string Name { get; private set; }
    public double SumDmg
    {
        get
        {

            return _sumAllDamage;
        }
        private set
        {
            _sumAllDamage = value;
            CheckTypeDamage();
            _statsAllText.text = ValueConverterStatic.DoubleToString(SumDmg);
        }
    }
    public double DmgPerLvl
    {
        get
        {

            return _baseDamage * Math.Pow(2, _awakingLvl - 1d);
        }
        private set { _baseDamage = value; }
    }
    private double _price;
    public double Price
    {
        get { return _price; }
        private set
        {
            _price = value;
            UpdatePriceText();
        }
    }
    readonly double baseDamage = 5d;
    double _baseDamage;
    readonly double basePrice = 50d;
    const double _damageModif = 0.81d;
    public int Lvl
    {
        get { return _lvl; }
        set
        {
            _lvl = value;
            CalculateDamage();
            _lvltext.text = _lvl.ToString();
            _statsAllText.text = ValueConverterStatic.DoubleToString(SumDmg);
            UpdatePrice();
        }
    }
    private int _awakingLvl = 1;
    public int AwakeningLvl
    {
        get { return _awakingLvl; }
        set
        {
            _awakingLvl = value;

            CalculateDamage();
            _statsInLvlText.text = ValueConverterStatic.DoubleToString(DmgPerLvl);
            AwakingPriceCalc();
        }
    }
    public double AwakingPrice { get; set; }
    public double PriceText { get; private set; }

    private List<GameObject> _starList = new();

    [SerializeField]
    private GameObject _starPrefab2;

    [SerializeField]
    private GameObject _starPrefab3;

    private void Start()
    {
        _buyButton.onClick.AddListener(LvlUp);
        _awakingButton.onClick.AddListener(AwakingCalc);
        UpdatePrice();

        AwakingPriceCalc();
        _toolsIcon.sprite = _item._sprite;
    }

    public void AwakingLoad()
    {
        for (int i = 1; i < YG2.saves.lvlAwakening[Index]; i++)
        {
            if (AwakeningLvl < 11)
            {
                AwakeningLvl++;
                var s = Instantiate(_starPrefab, _awakingWindow.transform);
                _starList.Add(s);
            }
            else if (AwakeningLvl >= 11 && AwakeningLvl < 21)
            {
                AwakeningLvl++;

                if (AwakeningLvl == 12)
                {
                    foreach (var x in _starList)
                    {
                        Destroy(x);
                    }
                    _starList.Clear();
                }
                var s = Instantiate(_starPrefab2, _awakingWindow.transform);
                _starList.Add(s);
            }
            else if (AwakeningLvl >= 21 && AwakeningLvl < 31)
            {
                AwakeningLvl++;
                if (AwakeningLvl == 22)
                {
                    foreach (var x in _starList)
                    {
                        Destroy(x);
                    }
                    _starList.Clear();
                }
                var s = Instantiate(_starPrefab3, _awakingWindow.transform);
                _starList.Add(s);
            }
        }
    }

    private void GetShopItem(ShopItem item)
    {
        _item = item;
    }

    private void SetDefaultValue()
    {
        if (YG2.lang == "ru")
            _nameText.text = _item.name_ru;
        else
            _nameText.text = _item.name_en;

        DmgPerLvl = BaseDamage();
        _statsAllText.text = ValueConverterStatic.DoubleToString(SumDmg);
        _statsInLvlText.text = ValueConverterStatic.DoubleToString(DmgPerLvl);
        _priceText.text = ValueConverterStatic.DoubleToString(BaseValue());
        _lvltext.text = Lvl.ToString();
    }

    private void LvlUp()
    {
        for (int i = 0; i < XPanelScript.Instance.xModficator; i++)
        {
            if (_lvl > 0 && ADSController.Instance.freeModf == true)
            {
                ADSController.Instance.freeModf = false;

                Lvl++;
                YG2.saves.lvlItem[Index] = _lvl;
                if (YG2.saves.bestLvlItem < Lvl)
                {
                    YG2.saves.bestLvlItem = Lvl;
                }
            }
            else if (Price <= BasicManager.Instance.TreeCount)
            {
                BasicManager.Instance.TreeCount -= Price;

                Lvl++;
                YG2.saves.lvlItem[Index] = _lvl;
                if (YG2.saves.bestLvlItem < Lvl)
                {
                    YG2.saves.bestLvlItem = Lvl;
                }

                if (Index == ShopController.Instance.GetItem() - 1)
                {
                    if (_shop.shopItems.Length > Index + 1)
                    {
                        ShopController.Instance.CreateTool(Index + 1);
                        YG2.saves.itemsCount = ShopController.Instance._items.Count;
                    }
                }
            }
            AchivmentController.Instance.InvokeAction();
        }
    }

    private void AwakingPriceCalc()
    {
        if (_item.Type == ClickType.BaseClick)
        {
            var price = BaseValue() * Math.Pow(1.08d, 50);
            AwakingPrice = price * Math.Pow(AwakeningLvl, AwakeningLvl / 2);
        }
        else
        {
            var price = BaseValue() * Math.Pow(1.09d, 50);
            AwakingPrice = price * Math.Pow(AwakeningLvl, AwakeningLvl / 2);
        }
    }

    private void AwakingCalc()
    {
        var price = AwakingPrice;

        if (price <= BasicManager.Instance.TreeCount)
        {
            if (AwakeningLvl < 11)
            {
                AwakeningLvl++;
                var s = Instantiate(_starPrefab, _awakingWindow.transform);
                _starList.Add(s);
                BasicManager.Instance.TreeCount -= price;
            }
            else if (AwakeningLvl >= 11 && AwakeningLvl < 21)
            {
                AwakeningLvl++;
                if (AwakeningLvl == 12)
                {
                    foreach (var x in _starList)
                    {
                        Destroy(x);
                    }
                    _starList.Clear();
                }
                var s = Instantiate(_starPrefab2, _awakingWindow.transform);
                _starList.Add(s);
                BasicManager.Instance.TreeCount -= price;
            }
            else if (AwakeningLvl >= 21 && AwakeningLvl < 31)
            {
                AwakeningLvl++;
                if (AwakeningLvl == 22)
                {
                    foreach (var x in _starList)
                    {
                        Destroy(x);
                    }
                    _starList.Clear();
                }
                var s = Instantiate(_starPrefab3, _awakingWindow.transform);
                _starList.Add(s);
                BasicManager.Instance.TreeCount -= price;
            }
            YG2.saves.lvlAwakening[Index] = AwakeningLvl;
            if (YG2.saves.bestAwakeningLvl < AwakeningLvl)
            {
                YG2.saves.bestAwakeningLvl = AwakeningLvl;
            }
            AchivmentController.Instance.InvokeAction();
        }
    }

    private double BaseValue()
    {
        if (_item.Type == ClickType.BaseClick)
        {
            return baseDamage;
        }
        else
        {
            if (Index > 1)
            {
                var result = basePrice * Math.Pow(baseMod, Index - 1);

                return result;
            }
            else
            {
                return basePrice;
            }
        }
    }

    private double BaseDamage()
    {
        if (_item.Type == ClickType.BaseClick)
        {
            return 1d;
        }
        else
        {
            if (Index > 1)
            {
                var price = BaseValue();
                return price * 0.081d * Math.Pow(_damageModif, Index - 2);
            }
            else
            {
                return 5d;
            }
        }
    }

    private void CalculateDamage()
    {
        var damage = DmgPerLvl * Lvl;
        SumDmg = damage;
    }

    private void UpdatePrice()
    {
        if (_item.Type == ClickType.BaseClick)
        {
            var price = BaseValue() * Math.Pow(1.09d, Lvl);
            Price = Math.Ceiling(price);
        }
        else
        {
            var price = BaseValue() * Math.Pow(1.07d, Lvl);
            Price = Math.Ceiling(price);
        }
    }

    public void UpdatePriceText()
    {
        double price = 0;
        if (_item.Type == ClickType.BaseClick)
        {
            for (int i = 0; i < XPanelScript.Instance.xModficator; i++)
            {
                price += BaseValue() * Math.Pow(1.09d, Lvl + i);
            }
        }
        else
        {
            for (int i = 0; i < XPanelScript.Instance.xModficator; i++)
            {
                price += BaseValue() * Math.Pow(1.07d, Lvl + i);
            }
        }
        PriceText = Math.Ceiling(price);
        _priceText.text = ValueConverterStatic.DoubleToString(PriceText);
    }

    private void CheckTypeDamage()
    {
        if (_item.Type == ClickType.BaseClick)
        {
            ShopController.Instance.ClickDamage = SumDmg + 1d;
        }
        else
        {
            ShopController.Instance.SetDamageDictionary(Index, SumDmg);
        }
    }

    private void OnEnable()
    {
        XPanelScript.Instance.onSwichMod += UpdatePriceText;
    }

    private void OnDisable()
    {
        XPanelScript.Instance.onSwichMod -= UpdatePriceText;
    }
}
