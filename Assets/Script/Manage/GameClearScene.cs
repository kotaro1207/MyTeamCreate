using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearScene : MonoBehaviour
{
    [SerializeField, Header("�V�[���������")]
    private string sceneName;

    public string PlayerTag = "Player"; // �v���C���[�̃^�O

    public bool isClear = false;

    private bool isPlayerTouched = false; // �v���C���[�ɐG�ꂽ���ǂ���

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player�ɓ�����܂���!!");
            isPlayerTouched = true;   // �^�b�`�J�n
            isClear = true;
        }
    }

    void Update()
    {
        if (isPlayerTouched)
        {
            Debug.Log("�N���A�V�[���Ɉȍ~���܂�");
            SceneManager.LoadScene(sceneName);
            isPlayerTouched = false; // ��d�Ăяo���h�~�i�V�[���J�ڌ�͕s�v�j
        }
    }
}