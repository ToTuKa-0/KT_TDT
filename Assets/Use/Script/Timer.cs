using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime = 300f; // 秒単位で設定（例: 5分=300秒）
    [SerializeField] private TextMeshProUGUI timerText; // 表示用TMP
    [SerializeField] private string sceneToLoad; // タイマー終了後に移動するシーン名

    private float currentTime;
    private bool isRunning = false;

    void Start()
    {
        currentTime = startTime;
        isRunning = true;
        UpdateTimerText();
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isRunning = false;
            OnTimerEnd();
        }

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(currentTime).ToString(); // 秒単位で表示
        }
    }

    void OnTimerEnd()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); // 指定したシーンに移動
        }
    }

    // 外部から一時停止・再開可能
    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;
}
