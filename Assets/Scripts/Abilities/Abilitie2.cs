using System;
using System.Collections;
using UnityEngine;
using YG;

public class Abilitie2 : BaseAbilitie
{
    [SerializeField]
    float treeModf;

    public override IEnumerator AbilitieStart()
    {
        _buttonAbilitie.interactable = false;
        _timer.gameObject.SetActive(true);
        BasicManager.Instance.treeModif = treeModf;
        float t = _time;
        _state = StateAbilities.CoolDown;
        YG2.saves.abilities[_id] = _state;

        while (t > 0f)
        {
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            _timer.text = $"{minutes:00}:{seconds:00}";

            t -= Time.deltaTime;
            yield return null;
        }
        BasicManager.Instance.treeModif = 1f;

        StartCoroutine(CoolDown());
    }
}
