using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Singleton<EnemyController>
{
    [SerializeField]
    EnemyList _enemyList;

    [SerializeField]
    GameObject _prefab;

    [SerializeField]
    GameObject _clickZone;

    [SerializeField]
    GameObject _timerPrefab;

    [SerializeField]
    GameObject _timerSpawnPoint;

    Enemy _enemy;
    public GameObject timer;

    public GameObject SpawnEnemy(int indexLvl, bool isBoss)
    {
        var enemy = Instantiate(_prefab, _clickZone.transform);
        var image = enemy.GetComponent<Image>();
        if (indexLvl <= 200)
        {
            if (!isBoss)
            {
                _enemy = _enemyList._lvlArray[Mathf.CeilToInt(indexLvl / 10f) - 1]._enemyArray[
                    Random.Range(0, 4)
                ];
                image.sprite = _enemy.Icon;
                image.SetNativeSize();
                Destroy(timer);
            }
            else if (isBoss)
            {
                _enemy = _enemyList._lvlArray[Mathf.CeilToInt(indexLvl / 10f) - 1]._enemyArray[
                    Random.Range(4, 6)
                ];
                image.sprite = _enemy.Icon;
                image.SetNativeSize();
                SpawnTimer();
            }
        }
        else
        {
            if (!isBoss)
            {
                _enemy = _enemyList._lvlArray[_enemyList._lvlArray.Length - 1]._enemyArray[
                    Random.Range(0, 4)
                ];
                image.sprite = _enemy.Icon;
                image.SetNativeSize();
                Destroy(timer);
            }
            else if (isBoss)
            {
                _enemy = _enemyList._lvlArray[_enemyList._lvlArray.Length - 1]._enemyArray[
                    Random.Range(4, 6)
                ];
                image.sprite = _enemy.Icon;
                image.SetNativeSize();
                SpawnTimer();
            }
        }

        return enemy;
    }

    public string GetName()
    {
        return _enemy.NameRu;
    }

    private void SpawnTimer()
    {
        if (timer != null)
        {
            Destroy(timer);
        }

        timer = Instantiate(_timerPrefab, _timerSpawnPoint.transform);
    }
}
