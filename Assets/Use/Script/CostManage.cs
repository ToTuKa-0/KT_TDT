using System;
using UnityEngine;

/*
 *空のオブジェクトにアタッチ
 *コスト関連を管理
 *初期コストを決める
 */

public class CostManage : MonoBehaviour
{
    [SerializeField] public static CostManage Instance { get; private set; } //外部からアクセス可能
    [SerializeField] public event Action<int> OnCostChanged; //コスト変更時に通知
    [SerializeField] private int startCost; //初期コスト

    private int currentCost; //現在コスト保持

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this; //シングルトンとして保持
        currentCost = startCost; //初期コスを設定
    }

    public int GetCurrentCost() => currentCost; //現在コストを取得するメソッド

    //エネミー撃破でコスト回復
    public void AddCost(int amount)
    {
        currentCost += amount; //コスト計算
        OnCostChanged?.Invoke(currentCost); //イベント通知(あれば)
    }

    //配置時にコストを消費
    public bool ConsumeCost(int amount)
    {
        //コストがある場合に消費
        if (currentCost >= amount)
        {
            currentCost -= amount; //コストを減らす
            OnCostChanged?.Invoke(currentCost); //イベント通知(あれば)
            return true;
        }
        return false;
    }

    //配置可能かどうかを判定
    public bool CanPlaceUnit(int unitCost)
    {
        return currentCost >= unitCost;
    }
}
