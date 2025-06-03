using UnityEngine;

public class HPManager : MonoBehaviour
{
    public static HPManager Instance;

    public int Hp = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �Q�[���N���������ƕێ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
