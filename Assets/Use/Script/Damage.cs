using UnityEngine;
using System.Collections;

/*
 *ダメージを与える
 *接触時に攻撃を行う
 *一定間隔で攻撃をする
 *特定のオブジェクトに衝突時止まって攻撃(エネミーのみ)
 */

public class Damage : MonoBehaviour
{
    [SerializeField] int damage; //ダメージ量
    [SerializeField] float damageInterval; //攻撃タイミング
    [SerializeField] string targetTag; //攻撃対象のタグを設定

    private Coroutine damageCoroutine;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //指定したタグの時に実行
        if (collision.gameObject.CompareTag(targetTag))
        {
            HP hp = collision.gameObject.GetComponent<HP>(); //衝突対象のコンポーネント取得
            E_Move mover = collision.gameObject.GetComponent<E_Move>(); //移動スクリプト取得

            if (hp != null && damageCoroutine == null)
            {
                if (mover != null) mover.isPaused = true; //衝突時に移動停止

                damageCoroutine = StartCoroutine(DamageOverTime(hp, mover));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        //指定したタグの時に実行
        if (collision.gameObject.CompareTag(targetTag))
        {
            //ダメージ処理が行われてるなら止める
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;

                E_Move mover = collision.gameObject.GetComponent<E_Move>();

                if (mover != null) mover.isPaused = false; //離れたら移動再開
            }
        }
    }

    //一定間隔でダメージを与える
    IEnumerator DamageOverTime(HP hp, E_Move mover)
    {
        while (true)
        {
            hp.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
