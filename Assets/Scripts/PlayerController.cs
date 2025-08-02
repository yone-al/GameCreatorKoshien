using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// プレイヤーキャラクターの動作、やる気（HP）システム、障害物との衝突処理を管理するコントローラークラス
/// GameManagerクラスと連携してゲーム状態を制御し、UIのやる気ゲージを更新する
/// </summary>
public class PlayerController : MonoBehaviour
{
    // === やる気（HP）システム関連 ===
    /// <summary>
    /// プレイヤーの最大やる気値（HP）。デフォルトは5で、ゲーム開始時に yaruki に設定される
    /// UIのSliderコンポーネントの maxValue としても使用される
    /// </summary>
    public int maxYaruki = 5;
    
    /// <summary>
    /// プレイヤーの現在のやる気値（HP）
    /// ObstacleControllerが持つ"Obstacle"タグのオブジェクトと衝突するたびに1減少
    /// 0以下になると GameManager.GameOver() が呼び出される
    /// </summary>
    public int yaruki;

    // === プレイヤー移動制御関連 ===
    /// <summary>
    /// プレイヤーの移動速度。Unity Input Systemからの入力値と Time.deltaTime と掛け合わせて移動量を計算
    /// </summary>
    public float speed = 3.0f;
    
    /// <summary>
    /// プレイヤーのX軸移動制限。画面外への移動を防ぐため -limitX ～ +limitX の範囲に制限
    /// </summary>
    public float limitX = 1.5f;
    
    /// <summary>
    /// プレイヤーのY軸移動制限。画面外への移動を防ぐため -limitY ～ +limitY の範囲に制限
    /// </summary>
    public float limitY = 4.5f;

    // === 外部オブジェクト参照 ===
    /// <summary>
    /// GameManagerオブジェクトへの参照。isGameActive状態の確認とGameOver()メソッドの呼び出しに使用
    /// GameManager.isGameActive が false の場合、プレイヤーの移動を停止する
    /// </summary>
    public GameObject gameManager;
    
    /// <summary>
    /// やる気ゲージUI（Sliderコンポーネント）への参照
    /// maxValue と value プロパティを更新して現在のやる気を視覚的に表示
    /// </summary>
    public GameObject yarukiGauge;

    /// <summary>
    /// ゲーム開始時の初期化処理
    /// やる気を最大値に設定し、UIのやる気ゲージを初期化する
    /// </summary>
    void Start()
    {
        // やる気を最大値で初期化
        yaruki = maxYaruki;
        
        // やる気ゲージUIの最大値と現在値を設定
        // この設定により、Sliderが正しいやる気の割合を表示できる
        yarukiGauge.GetComponent<Slider>().maxValue = maxYaruki;
        yarukiGauge.GetComponent<Slider>().value = yaruki;
    }

    /// <summary>
    /// 毎フレーム実行される更新処理
    /// ゲームが有効な場合のみプレイヤーの入力処理と移動制限を行う
    /// </summary>
    void Update()
    {
        // GameManager.isGameActive が false の場合、プレイヤーの操作を無効化
        // ゲームオーバー時やポーズ時に移動を停止する
        if (!gameManager.GetComponent<GameManager>().isGameActive)
        {
            return;
        }

        // Unity Input Systemから水平・垂直方向の入力値を取得
        // キーボード（WASD, 矢印キー）やゲームパッドの入力を -1.0 ～ 1.0 の範囲で取得
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 入力値、移動速度、フレーム時間を考慮した移動ベクトルを計算
        // Time.deltaTime により、フレームレートに依存しない一定速度での移動を実現
        Vector3 movement = new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
        this.gameObject.transform.Translate(movement);

        // X軸方向の移動制限を適用
        // Mathf.Clamp により -limitX ～ +limitX の範囲内に位置を制限
        float clampedX = Mathf.Clamp(this.gameObject.transform.position.x, -limitX, limitX);
        this.gameObject.transform.position = new Vector3(clampedX, this.gameObject.transform.position.y, 0);
        
        // Y軸方向の移動制限を適用
        // Mathf.Clamp により -limitY ～ +limitY の範囲内に位置を制限
        float clampedY = Mathf.Clamp(this.gameObject.transform.position.y, -limitY, limitY);
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, clampedY, 0);
    }

    /// <summary>
    /// 2Dトリガー衝突時の処理
    /// ObstacleControllerを持つ"Obstacle"タグのオブジェクトとの衝突を検知し、やる気を減少させる
    /// やる気が0以下になった場合、GameManagerのGameOver()メソッドを呼び出す
    /// </summary>
    /// <param name="other">衝突した相手のCollider2D。ObstacleGeneratorで生成された障害物オブジェクト</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // 衝突した相手が"Obstacle"タグを持つかチェック
        // ObstacleGeneratorで生成され、ObstacleControllerがアタッチされたオブジェクトのみ処理
        if (other.gameObject.CompareTag("Obstacle"))
        {
            // やる気を1減少させる
            yaruki--;
            
            // やる気ゲージUIを現在のやる気値で更新
            // プレイヤーに現在の体力状況を視覚的にフィードバック
            yarukiGauge.GetComponent<Slider>().value = yaruki;
            
            // デバッグ用ログ出力（開発時の状態確認用）
            Debug.Log("やる気: " + yaruki);

            // やる気が0以下になった場合はゲームオーバー
            // GameManager.GameOver()を呼び出してゲーム終了処理を実行
            if (yaruki <= 0)
            {
                gameManager.GetComponent<GameManager>().GameOver();
            }
        }
    }
}
