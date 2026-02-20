using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseHelpText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Text _text;

    [SerializeField]
    protected GameObject _obj;

    private void Awake()
    {
        if (_obj != null)
        {
            _text = _obj.GetComponent<Text>();

            _obj.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _obj.transform.position = this.transform.position;
        _obj.SetActive(true);
        SetText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _obj.SetActive(false);
    } 

    public abstract void SetText();
}