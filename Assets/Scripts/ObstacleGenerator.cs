using UnityEngine;

/// <summary>
/// 障害物の自動生成を管理するジェネレータークラス
/// 設定された間隔で画面上部からランダムな障害物を生成し、GameManagerと連携してゲーム状態を監視
/// 生成された障害物にはObstacleControllerがアタッチされ、自動的に落下・衝突処理を行う
/// </summary>
public class ObstacleGenerator : MonoBehaviour
{
    /// <summary>
    /// 生成可能な障害物プレハブの配列
    /// Unityエディタで設定され、この配列からランダムに選択された障害物が生成される
    /// 各プレハブにはObstacleControllerコンポーネントがアタッチされている必要がある
    /// </summary>
    public GameObject[] obstacles;
    
    /// <summary>
    /// 障害物生成の間隔（秒）
    /// この時間ごとに新しい障害物が画面上部に生成される
    /// </summary>
    public float interval = 1.0f;
    
    /// <summary>
    /// 障害物生成のX軸範囲
    /// -rangeX ～ +rangeX の範囲内でランダムなX座標に障害物を配置
    /// プレイヤーの移動範囲（PlayerController.limitX）と対応している
    /// </summary>
    public float rangeX = 1.5f;
    
    /// <summary>
    /// 障害物生成のY座標（画面上部の位置）
    /// 全ての障害物はこのY座標から生成され、ObstacleControllerにより下向きに落下する
    /// </summary>
    public float posY = 5.5f;

    /// <summary>
    /// 前回の障害物生成からの経過時間を追跡するプライベート変数
    /// Update()でTime.deltaTimeずつ増加し、intervalと比較される
    /// </summary>
    private float time = 0.0f;
    
    /// <summary>
    /// GameManagerオブジェクトへの参照
    /// GameManager.isGameActive の状態を監視し、ゲーム停止時に障害物生成を停止
    /// </summary>
    public GameObject gameManager;

    /// <summary>
    /// ゲーム開始時の初期化処理
    /// 現在は特別な初期化処理は行わない
    /// </summary>
    void Start()
    {
        // 将来的に障害物生成の初期化処理を追加する場合はここに記述
    }

    /// <summary>
    /// 毎フレーム実行される更新処理
    /// ゲームが有効な場合のみ時間を計測し、指定間隔で障害物を生成する
    /// </summary>
    void Update()
    {
        // GameManager.isGameActive が false の場合、障害物生成を停止
        // ゲームオーバー時やポーズ時に新しい障害物の生成を防ぐ
        if (!gameManager.GetComponent<GameManager>().isGameActive)
        {
            return;
        }

        // フレーム時間を加算して経過時間を更新
        // Time.deltaTimeにより、フレームレートに依存しない正確な時間計測を実現
        time += Time.deltaTime;
        
        // 設定された間隔に達した場合、新しい障害物を生成
        if (time >= interval)
        {
            // obstacles配列からランダムに障害物プレハブを選択
            // 複数の障害物バリエーションがある場合、ゲームプレイに多様性を提供
            int randomIndex = Random.Range(0, obstacles.Length);
            GameObject obstacle = obstacles[randomIndex];
            
            // X軸方向にランダムな位置を決定
            // rangeXの範囲内でランダムに配置することで、プレイヤーが回避行動を取る必要性を生み出す
            float randomX = Random.Range(-rangeX, rangeX);
            
            // 選択された障害物プレハブを指定位置に生成
            // 生成された障害物には自動的にObstacleControllerがアタッチされ、落下処理が開始される
            // PlayerControllerとの衝突検知により、プレイヤーのやる気減少のトリガーとなる
            Instantiate(obstacle, new Vector3(randomX, posY, 0), Quaternion.identity);
            
            // タイマーをリセットして次の生成間隔の計測を開始
            time = 0.0f;
        }
    }
}
