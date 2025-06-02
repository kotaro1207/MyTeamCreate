using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearScene : MonoBehaviour
{
    [SerializeField, Header("シーン名を入力")]
    private string sceneName;

    public string PlayerTag = "Player"; // プレイヤーのタグ

    public bool isClear = false;

    private bool isPlayerTouched = false; // プレイヤーに触れたかどうか

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Playerに当たりました!!");
            isPlayerTouched = true;   // タッチ開始
            isClear = true;
        }
    }

    void Update()
    {
        if (isPlayerTouched)
        {
            Debug.Log("クリアシーンに以降します");
            SceneManager.LoadScene(sceneName);
            isPlayerTouched = false; // 二重呼び出し防止（シーン遷移後は不要）
        }
    }
}