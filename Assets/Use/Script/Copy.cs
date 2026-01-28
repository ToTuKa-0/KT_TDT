using UnityEngine;
using UnityEngine.InputSystem;

/*
 *オブジェクトを複製するもの
 *複製するものをプレハブ化させてこいつを付ける
 *コストが足りない場合非表示になるためわからない
 *配置できない場合消える
 */

public class Copy : MonoBehaviour
{
    [SerializeField] GameObject copy; // 複製するPrefab（配置用ユニット）
    [SerializeField] Transform[] placementPoints; // ユニットを配置できる地点
    [SerializeField] int unitCost = 10; // ユニットを配置するために必要なコスト
    [SerializeField] bool isUI = true; // UI用か、実際に配置されるコピー用かのフラグ

    Camera mainCamera; // メインカメラ参照
    bool drag = false; // ドラッグ中かどうか
    bool isOriginal = true; // このオブジェクトが元のUIボタンかコピーか
    Vector3 offset; // ドラッグ時のマウスとオブジェクトのずれ
    GameObject dragObject; // 現在ドラッグ中のオブジェクト
    Vector3 initialDragPosition; // ドラッグ開始時の位置（必要に応じて復帰用）

    void Start()
    {
        mainCamera = Camera.main; // メインカメラを取得
    }

    private void OnEnable()
    {
        // UI用オブジェクトの場合、コスト変化に応じてボタンの表示/非表示を更新
        if (isUI && CostManage.Instance != null)
        {
            CostManage.Instance.OnCostChanged += UpdateDisplay; // イベント登録
            UpdateDisplay(CostManage.Instance.GetCurrentCost()); // 初期表示更新
        }
    }

    private void OnDisable()
    {
        // UI用オブジェクトの場合、イベント登録を解除
        if (isUI && CostManage.Instance != null)
        {
            CostManage.Instance.OnCostChanged -= UpdateDisplay;
        }
    }

    void Update()
    {
        Vector3 mousePos = GetMouseWorldPos(); // 現在のマウス位置をワールド座標に変換

        // --- マウス押下でコピー生成 ---
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!isOriginal) return; // コピーはドラッグの反応をしない

            // マウス位置にレイを飛ばしてクリック判定
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // クリックしたらPrefabを複製
                dragObject = Instantiate(copy, transform.position, Quaternion.identity);
                offset = dragObject.transform.position - mousePos; // ドラッグオフセット
                drag = true; // ドラッグ開始
                initialDragPosition = dragObject.transform.position;

                // 複製したオブジェクトにコピー用設定を渡す
                Copy cloneScript = dragObject.GetComponent<Copy>();
                if (cloneScript != null)
                {
                    cloneScript.isOriginal = false; // コピーとして扱う
                    cloneScript.isUI = false;       // UIではない
                }
            }
        }

        // --- ドラッグ中の処理 ---
        if (drag && dragObject != null && Mouse.current.leftButton.isPressed)
        {
            // ドラッグ中はマウスに追従
            dragObject.transform.position = mousePos + offset;
        }

        // --- マウスを離した瞬間 ---
        if (Mouse.current.leftButton.wasReleasedThisFrame && drag)
        {
            drag = false; // ドラッグ終了

            if (dragObject != null)
            {
                bool placed = false; // 配置できたかの判定

                Transform closestPoint = null;
                float minDistance = float.MaxValue;
                float allowedDistance = 0.5f; // 許容距離

                if (placementPoints != null && placementPoints.Length > 0)
                {
                    // 最も近い配置可能地点を探す
                    foreach (Transform point in placementPoints)
                    {
                        float distance = Vector3.Distance(dragObject.transform.position, point.position);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestPoint = point;
                        }
                    }

                    // 許容距離内に配置可能地点がある場合
                    if (closestPoint != null && minDistance <= allowedDistance)
                    {
                        // コストが足りれば配置
                        if (CostManage.Instance != null && CostManage.Instance.ConsumeCost(unitCost))
                        {
                            dragObject.transform.position = closestPoint.position; // 位置を確定
                            dragObject.tag = "Player"; // タグを変更（ゲームロジック用）
                            placed = true;
                        }
                        else
                        {
                            Debug.Log("コピー削除: コスト不足");
                        }
                    }
                }

                // 配置できなかった場合は削除
                if (!placed)
                {
                    Destroy(dragObject);
                    Debug.Log("コピー削除: 配置不可またはコスト不足");
                }
            }

            dragObject = null; // 参照をクリア
        }
    }

    // コストに応じてUIの表示/非表示を更新（UI専用）
    void UpdateDisplay(int currentCost)
    {
        if (isUI)
            gameObject.SetActive(currentCost >= unitCost); // コストが足りなければ非表示
    }

    // マウス座標をワールド座標に変換
    Vector3 GetMouseWorldPos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue(); // スクリーン座標取得
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f));
        mouseWorldPos.z = 0; // Z座標は2Dゲーム用に0固定
        return mouseWorldPos;
    }

}
