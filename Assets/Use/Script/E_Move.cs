using UnityEngine;
using System.Collections;

public class E_Move : MonoBehaviour
{
    [SerializeField] Vector2[] points; //配列
    [SerializeField, Tooltip("数値が小さいほど早くなる")] float speed;
    bool loop = false; //繰り返さない

    void Start()
    {
        StartCoroutine(MoveSetUP());
    }

    IEnumerator MoveSetUP()
    {
        //1回は実行
        do
        {
            //pointsを順に動かす
            for (int i = 0; i < points.Length; i++)
            {
                yield return StartCoroutine(Move(points[i])); //yield returnで別フレームで行う処理をリセット
            }
        }
        while (loop);
    }

    IEnumerator Move(Vector2 target)
    {
        Vector2 start = transform.position; //移動開始地点を記録
        float elapsed = 0f; //経過時間を測る

        //speedの時間かけて目的地に移動
        while (elapsed < speed)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector2.Lerp(start, target, elapsed / speed);
            yield return null;
        }

        transform.position = target;
    }
}
