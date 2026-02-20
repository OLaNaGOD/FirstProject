using UnityEngine;
using UnityEngine.EventSystems;

public class HelpTextCell : BaseHelpText
{
    ShopCell _cell;

    private void Start()
    {
        _cell = GetComponentInParent<ShopCell>();
    }

    public override void SetText()
    {
        _text.text = ValueConverterStatic.DoubleToString(_cell.AwakingPrice);
    }
}
