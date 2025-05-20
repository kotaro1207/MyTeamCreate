using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DangerUISpawner : MonoBehaviour
{
    [SerializeField] private Image DangerUI;
    private GameObject _canvas;
    private Camera _camera;

    void Start()
    {
        _canvas = GameObject.Find("DangerCanvas");
        _camera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            // 弾のワールド座標を取得
            Vector3 worldPos = collision.transform.position;

            // ワールド→スクリーン座標に変換
            Vector3 screenPos = _camera.WorldToScreenPoint(worldPos);

            // スクリーン→UIローカル座標に変換
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.GetComponent<RectTransform>(),
                screenPos,
                _camera,
                out Vector2 localPos
            );

            // UIを生成して位置をセット
            Image image = Instantiate(DangerUI, _canvas.transform);
            image.rectTransform.anchoredPosition = new Vector2(845, localPos.y); // x=固定, y=弾に合わせる
        }
    }
}
