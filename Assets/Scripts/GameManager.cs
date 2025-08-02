using UnityEngine;
using TMPro;

/// <summary>
/// ゲーム全体の状態管理とタイマー機能を制御するメインマネージャークラス
/// PlayerController、ObstacleController、ObstacleGeneratorと連携してゲームの進行を管理
/// ゲーム開始から終了まで、時間制限とゲームオーバー処理を担当する
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ゲームが現在進行中かどうかを示すフラグ
    /// true: ゲーム進行中（プレイヤー操作可能、障害物生成・移動継続）
    /// false: ゲーム停止中（プレイヤー操作無効、障害物生成・移動停止）
    /// PlayerController.Update()、ObstacleController.Update()、ObstacleGenerator.Update()で参照される
    /// </summary>
    public bool isGameActive = true;
    
    /// <summary>
    /// ゲームオーバー時に表示するUIパネルオブジェクト
    /// GameOver()メソッドで SetActive(true) により表示される
    /// </summary>
    public GameObject gameOverPanel;
    
    /// <summary>
    /// ゲームの制限時間（秒）
    /// この時間に達すると自動的にGameOver()が呼び出される
    /// </summary>
    public float timeLimit = 60.0f;
    
    /// <summary>
    /// ゲーム開始からの経過時間（秒）
    /// Update()でTime.deltaTimeずつ増加し、timeLimitと比較される
    /// timerTextの表示更新にも使用される
    /// </summary>
    public float timeCount = 0.0f;
    
    /// <summary>
    /// タイマー表示用のTextMeshProUGUIコンポーネントを持つUIオブジェクト
    /// timeCountの値をもとに「8:XX」形式で残り時間を表示
    /// ゲーム終了時は「9:00」に変更される
    /// </summary>
    public GameObject timerText;

    /// <summary>
    /// ゲーム開始時の初期化処理
    /// 現在は特別な初期化処理は行わない
    /// </summary>
    void Start()
    {
        // 将来的にゲーム開始時の初期化処理を追加する場合はここに記述
    }

    /// <summary>
    /// 毎フレーム実行される更新処理
    /// ゲームが有効な場合のみタイマーの更新と制限時間チェックを行う
    /// </summary>
    void Update()
    {
        // ゲームが非アクティブの場合、タイマー更新を停止
        // ゲームオーバー後はこの条件によりタイマーが停止する
        if (!isGameActive)
        {
            return;
        }

        // フレーム時間を加算して経過時間を更新
        // Time.deltaTimeにより、フレームレートに依存しない正確な時間計測を実現
        timeCount += Time.deltaTime;

        // タイマーUIの表示を更新（「8:XX」形式で表示）
        // PadLeft(2, '0')により、1桁の秒数を「01」「02」のように0埋めで表示
        timerText.GetComponent<TextMeshProUGUI>().text = "8:" + ((int)timeCount).ToString().PadLeft(2, '0');
        
        // デバッグ用ログ出力（開発時の時間確認用）
        Debug.Log(timeCount);

        // 制限時間に達した場合の処理
        if (timeCount >= timeLimit)
        {
            // timeCountを制限時間でキャップして正確な値に調整
            timeCount = timeLimit;
            
            // ゲームオーバー処理を実行
            // この呼び出しにより isGameActive が false になり、他のコンポーネントの動作が停止
            GameOver();
            
            // タイマー表示を最終時刻「9:00」に設定
            timerText.GetComponent<TextMeshProUGUI>().text = "9:00";
        }
    }

    /// <summary>
    /// ゲーム終了処理
    /// PlayerController.OnTriggerEnter2D()からやる気が0以下になった場合、
    /// またはこのクラスのUpdate()で制限時間に達した場合に呼び出される
    /// </summary>
    public void GameOver()
    {
        // ゲーム状態を非アクティブに変更
        // この変更により、PlayerController、ObstacleController、ObstacleGeneratorの動作が停止
        isGameActive = false;
        
        // ゲームオーバーUIパネルを表示
        gameOverPanel.SetActive(true);
        
        // デバッグ用ログ出力（開発時のゲーム終了確認用）
        Debug.Log("Game Over");
    }
}
