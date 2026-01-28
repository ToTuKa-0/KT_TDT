using UnityEngine;
/*
 *リストの中からランダムに1体選んで出現させる
 *二体目を出す場合は別スクリプトで用意する
*/

public class E_BossSpawn : MonoBehaviour
{
    [SerializeField] GameObject boss; //ボスオブジェクト
    [SerializeField] Transform spawnPoint; //出現地点
    [SerializeField, Tooltip("出現秒数の最小値")] float minTime;
    [SerializeField, Tooltip("出現秒数の最大値")] float maxTime;

    float time = 0f;
    bool rock = false;

    //一度だけ出現させるシステム
    void Update()
    {
        time += Time.deltaTime;

        if (!rock && time >= minTime && time <= maxTime)
        {
            rock = true;
            BossSpawn();
        }
    }

    void BossSpawn()
    {
        Instantiate(boss, spawnPoint.position, Quaternion.identity);
    }
}