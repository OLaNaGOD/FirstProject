using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HelpTextAbility : BaseHelpText
{
    [SerializeField]
    Text _textThis;

    private void Start() { }

    public override void SetText()
    {
        _text.text = _textThis.text;
    }
}
