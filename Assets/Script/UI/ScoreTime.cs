using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreTime : MonoBehaviour
{
    public float score = 9000;
    public float timer = 0f;
    public float time = 0f;
    private int max = 9000, min = 0;
    private GameClearScene Clear;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // �V�[�����܂����ł��I�u�W�F�N�g��ێ�
    }
    private void Start()
    {
        GameObject clearObj = GameObject.Find("SceneManager(GameClear)");
        if (clearObj != null)
        {
            Clear = clearObj.GetComponent<GameClearScene>();
        }
    }

    void Update()
    {
        // ���̃V�[�������擾
        string sceneName = SceneManager.GetActiveScene().name;

        // "GameScene" �̂Ƃ������^�C�}�[�𓮂����i�����������ۂ̃Q�[���V�[�����ɕς��āj
        if (sceneName == "GameScene")
        {
            timer += Time.deltaTime;
            time += Time.deltaTime;

            if (timer >= 1.0f && (Clear == null || !Clear.isClear))
            {
                timer = 0;
                score -= 100;
            }

            score = Mathf.Clamp(score, min, max);
            Debug.Log(time);
            Debug.Log(score);
        }
    }



}