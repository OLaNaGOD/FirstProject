using System;
using UnityEngine;
using UnityEngine.UI;

public class XPanelScript : Singleton<XPanelScript>
{
    [SerializeField]
    Button _buttonX1;

    [SerializeField]
    Button _buttonX25;

    [SerializeField]
    Button _buttonX50;

    [SerializeField]
    Button _buttonX100;

    public int xModficator = 1;

    public event Action onSwichMod;

    private void Start()
    {
        setXModfX1();

        _buttonX1.onClick.AddListener(setXModfX1);
        _buttonX25.onClick.AddListener(setXModfX2);
        _buttonX50.onClick.AddListener(setXModfX3);
        _buttonX100.onClick.AddListener(setXModfX4);
    }

    void setXModfX1()
    {
        xModficator = 1;
        onSwichMod?.Invoke();
        _buttonX1.image.color = Color.green;
        _buttonX25.image.color = Color.white;
        _buttonX50.image.color = Color.white;
        _buttonX100.image.color = Color.white;
    }

    void setXModfX2()
    {
        xModficator = 25;
        onSwichMod?.Invoke();
        _buttonX1.image.color = Color.white;
        _buttonX25.image.color = Color.green;
        _buttonX50.image.color = Color.white;
        _buttonX100.image.color = Color.white;
    }

    void setXModfX3()
    {
        xModficator = 50;
        onSwichMod?.Invoke();
        _buttonX1.image.color = Color.white;
        _buttonX25.image.color = Color.white;
        _buttonX50.image.color = Color.green;
        _buttonX100.image.color = Color.white;
    }

    void setXModfX4()
    {
        xModficator = 100;
        onSwichMod?.Invoke();
        _buttonX1.image.color = Color.white;
        _buttonX25.image.color = Color.white;
        _buttonX50.image.color = Color.white;
        _buttonX100.image.color = Color.green;
    }
}
