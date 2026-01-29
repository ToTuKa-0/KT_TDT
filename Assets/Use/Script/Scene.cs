using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [SerializeField] private string sceneName; // Inspectorで指定可能

    // ボタンから呼ぶ関数
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName); // 指定したシーンに移動
        }
    }
}
