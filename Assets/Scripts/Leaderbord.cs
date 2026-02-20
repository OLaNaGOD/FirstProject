using UnityEngine;
using YG;
using YG.Utils.LB;

public class Leaderbord : MonoBehaviour
{
    private void OnEnable()
    {
        YG2.onGetLeaderboard += CheckLederBoard;
    }

    private void OnDisable()
    {
        YG2.onGetLeaderboard -= CheckLederBoard;
    }

    void CheckLederBoard(LBData table)
    {
        if (table.currentPlayer.score < YG2.saves.maxLvl)
        {
            YG2.SetLeaderboard("Level", YG2.saves.maxLvl);
        }
    }
}
