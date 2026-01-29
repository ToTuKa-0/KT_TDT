using UnityEngine;
using UnityEngine.SceneManagement;

public class Out : MonoBehaviour
{
    [Header("検知するオブジェクトのタグ")]
    [SerializeField] private string targetTag = "Enemy"; // Inspectorでタグを指定

    [Header("衝突時に移動するシーン名")]
    [SerializeField] private string sceneToLoad;         // Inspectorでシーン名を指定

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 接触オブジェクトのタグが一致したら
        if (collision.CompareTag(targetTag))
        {
            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                // シーンをロード
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("Scene to load is not set!");
            }
        }
    }
}
