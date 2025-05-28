using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField,Header("発砲音")] private AudioClip firing;
    [SerializeField,Header("ジャンプ音")] private AudioClip jump;
    [SerializeField,Header("落下音")] private AudioClip drop;
    [SerializeField,Header("死亡")] private AudioClip dead;
    [SerializeField,Header("ダメージ音")] private AudioClip damage;
    [SerializeField,Header("ゲームオーバー音")] private AudioClip gameOver;
    [SerializeField,Header("ボタン音")] private AudioClip select;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FireSound()
    {
    }
    public void JumpSound()
    {
    }
    public void DropSound()
    {
        audioSource.PlayOneShot(drop);
    }
    public void DeadSound()
    {
        audioSource.PlayOneShot(dead);
    }
    public void DamageSound()
    {
    }
    public void GameOverSound()
    {
        audioSource.PlayOneShot(gameOver);
    }
    public void SelectSound()
    {
        audioSource.PlayOneShot(select);
    }
}
