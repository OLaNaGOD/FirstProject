using UnityEngine;
using UnityEngine.UI;

public class AchivmetsWindowShower : MonoBehaviour
{
    [SerializeField]
    GameObject _window;
    Button button;
    bool _on = false;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchVisability);
    }

    void SwitchVisability()
    {
        if (!_on)
        {
            _window.gameObject.SetActive(true);
            _on = !_on;
        }
        else
        {
            _window.gameObject.SetActive(false);
            _on = !_on;
        }
    }
}
