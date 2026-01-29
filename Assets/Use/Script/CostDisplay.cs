using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/*
 *空のオブジェクトに着ける
 *Textで表示されるように
 */

public class CostDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text; //コストを表示するテキストを配置

    private void OnEnable()
    {
        if (CostManage.Instance != null)
        {
            CostManage.Instance.OnCostChanged += UpdateCostText; //コストが変わるたびに更新
            UpdateCostText(CostManage.Instance.GetCurrentCost()); //現在コストをUIUに
        }
    }

    private void OnDisable()
    {
        if (CostManage.Instance != null)
        {
            CostManage.Instance.OnCostChanged -= UpdateCostText;
        }
    }

    private void UpdateCostText(int currentCost)
    {
        if (text != null)
        {
            text.text = "Cost:" + currentCost;
        }
    }
}
