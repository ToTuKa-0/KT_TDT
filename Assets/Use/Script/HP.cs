using UnityEngine;

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

        gameObject.SetActive(false);
    }
}
