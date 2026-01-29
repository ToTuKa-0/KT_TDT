using UnityEngine;

public class Quit : MonoBehaviour
{
    public void quit()
    {
#if UNITY_EDITOR
        // エディター上では停止
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルドしたゲームでは終了
        Application.Quit();
#endif
    }
}
