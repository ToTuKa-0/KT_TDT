using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *HPについてのあれこれ
 *死亡処理も入っている
 *撃破時のコストも
 */

public class HP : MonoBehaviour
{
    [SerializeField] int maxHP;　//最大HP
    [SerializeField] int currentHP; //現在HP(最大と同じでいい)
    [SerializeField] int cost; //撃破時獲得コスト
    [SerializeField] int score; //撃破スコア

    void Start()
    {
        currentHP = maxHP;
    }

    //ダメージを受けた時の処理
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (CostManage.Instance != null)
        {
            CostManage.Instance.AddCost(cost);
        }

        if (Score.Instance != null)
        {
            Score.Instance.AddScore(score);
        }

        gameObject.SetActive(false);
    }
}
