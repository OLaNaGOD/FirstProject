using System.Collections;
using UnityEngine;

public class AutoHitCorutine : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(AutoDamageCorutine());
    }

    private IEnumerator AutoDamageCorutine()
    {
        WaitForSeconds _1sec = new WaitForSeconds(0.7f);
        WaitForSeconds _2sec = new WaitForSeconds(0.3f);
        while (true)
        {
            yield return _2sec;
            AutoHit();
            yield return _1sec;
        }
    }

    private void AutoHit()
    {
        if (BasicManager.Instance.EnemyObject)
        {
            BasicManager.Instance.EnemyHp -=
                BasicManager.Instance.AutoDamage * BasicManager.Instance.dpsModif;
        }
    }
}
