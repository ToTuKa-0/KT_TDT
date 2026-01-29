using TMPro;
using UnityEngine;

public class ScoreRankingDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rankingText;

    void Start()
    {
        ShowRanking();
    }

    void ShowRanking()
    {
        if (ScoreSaveManager.Instance == null) return;

        int[] scores = ScoreSaveManager.Instance.GetRanking();

        if (rankingText != null)
        {
            rankingText.text =
                $"1à ÅF{scores[0]}\n" +
                $"2à ÅF{scores[1]}\n" +
                $"3à ÅF{scores[2]}";
        }
    }
}
