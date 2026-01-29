using UnityEngine;

public class ScoreSaveManager : MonoBehaviour
{
    public static ScoreSaveManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン跨ぎOK
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ゲーム終了時にスコアを保存してランキング更新
    public void SaveScore(int score)
    {
        // 1位〜3位を取得
        int[] scores = new int[3];
        scores[0] = PlayerPrefs.GetInt("Score1", 0);
        scores[1] = PlayerPrefs.GetInt("Score2", 0);
        scores[2] = PlayerPrefs.GetInt("Score3", 0);

        // 今回のスコアを追加してソート
        int[] allScores = new int[4] { score, scores[0], scores[1], scores[2] };
        System.Array.Sort(allScores);
        System.Array.Reverse(allScores); // 大きい順

        // 上位3件だけ保存
        PlayerPrefs.SetInt("Score1", allScores[0]);
        PlayerPrefs.SetInt("Score2", allScores[1]);
        PlayerPrefs.SetInt("Score3", allScores[2]);
        PlayerPrefs.Save();
    }

    public int[] GetRanking()
    {
        return new int[3] {
            PlayerPrefs.GetInt("Score1", 0),
            PlayerPrefs.GetInt("Score2", 0),
            PlayerPrefs.GetInt("Score3", 0)
        };
    }
}
