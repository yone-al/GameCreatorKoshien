using UnityEngine;

/// <summary>
/// 障害物オブジェクトの動作制御を管理するコントローラークラス
/// ObstacleGeneratorによって生成された障害物の落下動作、ゲーム状態に応じた停止、画面外での自動削除を処理
/// PlayerControllerとの衝突検知により、プレイヤーのやる気減少のトリガーとなる
/// </summary>
public class ObstacleController : MonoBehaviour
{
    /// <summary>
    /// 障害物が画面外と判定されるY座標の閾値
    /// この値より下に落下した障害物は自動的にDestroy()される
    /// メモリリークを防ぎ、パフォーマンスを維持するための重要な設定
    /// </summary>
    public float threshold = -5.5f;
    
    /// <summary>
    /// GameManagerオブジェクトへの参照
    /// GameManager.isGameActive の状態を監視し、ゲーム停止時に障害物の動きを制御
    /// Start()でFindWithTag("GameManager")により自動取得される
    /// </summary>
    private GameObject gameManager;

    /// <summary>
    /// 障害物の初期化処理
    /// GameManagerの参照を取得し、下向きの落下速度を設定する
    /// </summary>
    void Start()
    {
        // "GameManager"タグを持つオブジェクトを検索して参照を取得
        // このゲームマネージャーからゲーム状態（isGameActive）を監視する
        gameManager = GameObject.FindWithTag("GameManager");
        
        // 障害物に下向きの落下速度を設定（Y軸方向に-5の速度）
        // この速度により、ObstacleGeneratorで生成された障害物が画面上から下へ落下する
        Vector2 velocity = new Vector2(0, -5);
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    /// <summary>
    /// 毎フレーム実行される更新処理
    /// ゲーム状態の監視と障害物の位置チェックを行う
    /// </summary>
    void Update()
    {
        // GameManager.isGameActive が false の場合、障害物の動きを停止
        // ゲームオーバー時やポーズ時に障害物を静止させる
        if (!gameManager.GetComponent<GameManager>().isGameActive)
        {
            // 速度を0にして障害物を停止
            Vector2 velocity = new Vector2(0, 0);
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = velocity;
        }

        // 現在の障害物のY座標を取得
        float posY = this.gameObject.transform.position.y;
        
        // 障害物が画面下の閾値を下回った場合、オブジェクトを削除
        // これにより不要な障害物を削除し、メモリ使用量とパフォーマンスを最適化
        if (posY < threshold)
        {
            Destroy(this.gameObject);
        }
    }
}
