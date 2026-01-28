using UnityEngine;
using System.Collections;

/*
 *エネミーの生成について
 *特定の秒数の間でのランダム出現
 */

public class E_Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] E_obj; //出現させるエネミー
    [SerializeField] Transform spawnPoint; //出現地点
    [SerializeField, Tooltip("出現秒数の最小値")] float minTime;
    [SerializeField, Tooltip("出現秒数の最大値")] float maxTime;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    //出現システム
    IEnumerator Spawn()
    {
        while (true)
        {
            //配列からランダムに選択
            int random = Random.Range(0, E_obj.Length);
            GameObject serect = E_obj[random];

            Instantiate(serect, spawnPoint.position, Quaternion.identity); //オブジェクト配置

            //繰り返し処理
            float Loop = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(Loop);
        }
    }
}
