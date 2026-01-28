using UnityEngine;
using System.Collections;

/*
 *エネミーの動きについて
 *指定した座標を一度だけ順に通っていく
 *それと速度も
 *特定のタグと接触してると移動を停止
 */

public class E_Move : MonoBehaviour
{
    [SerializeField, Tooltip("通過地点を設定")] Vector2[] points; //通過地点の配列
    [SerializeField, Tooltip("数値が小さいほど早くなる")] float speed;
    [SerializeField, Tooltip("停止させる対象のタグ")] string stopTag;

    [Tooltip("触んなくていい")] public bool isPaused = false; //trueなら移動停止
    bool loop = false;

    void Start()
    {
        StartCoroutine(MoveSetUP());
    }

    //移動条件
    IEnumerator MoveSetUP()
    {
        //最低1回は実行
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

    //移動処理
    IEnumerator Move(Vector2 target)
    {
        Vector2 start = transform.position; //移動開始地点を記録
        float elapsed = 0f; //経過時間を測る

        //speedの時間かけて目的地に移動
        while (elapsed < speed)
        {
            //停止フラグで一時的に止める
            if (!isPaused)
            {
                elapsed += Time.deltaTime;
                transform.position = Vector2.Lerp(start, target, elapsed / speed);
            }
            yield return null;
        }

        transform.position = target;
    }

    //衝突時開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(stopTag))
        {
            isPaused = true; //指定タグに当たったら停止
        }
    }

    //衝突終了時
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(stopTag))
        {
            isPaused = false; //タグから離れたら再開
        }
    }
}
