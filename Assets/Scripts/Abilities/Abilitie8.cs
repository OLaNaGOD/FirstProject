using System;
using System.Collections;
using UnityEngine;
using YG;

public class Abilitie8 : BaseAbilitie
{
    public override IEnumerator AbilitieStart()
    {
        _buttonAbilitie.interactable = false;
        _timer.gameObject.SetActive(true);

        BasicManager.Instance.EnemyHp -= BasicManager.Instance.EnemyHp;

        yield return null;

        _state = StateAbilities.CoolDown;
        YG2.saves.abilities[_id] = _state;

        StartCoroutine(CoolDown());
    }
}
