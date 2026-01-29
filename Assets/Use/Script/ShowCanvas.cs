using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    [SerializeField] private Canvas targetCanvas; // 表示したいCanvasをInspectorで指定

    // ボタンから呼ぶ用の関数
    public void Show()
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true); // Canvasを表示
        }
    }

    // もし非表示にする機能も欲しい場合は追加
    public void Hide()
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false); // Canvasを非表示
        }
    }
}
