using TMPro;
using UnityEngine;

/*
 *空の物にアタッチ
 */

public class Score : MonoBehaviour
{
    public static Score Instance;

    [SerializeField] TextMeshProUGUI scoreText; // TMP用
    int totalScore;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int add)
    {
        totalScore += add;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score：{totalScore}";
        }
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
