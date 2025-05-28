using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField,Header("���C��")] private AudioClip firing;
    [SerializeField,Header("�W�����v��")] private AudioClip jump;
    [SerializeField,Header("������")] private AudioClip drop;
    [SerializeField,Header("���S")] private AudioClip dead;
    [SerializeField,Header("�_���[�W��")] private AudioClip damage;
    [SerializeField,Header("�Q�[���I�[�o�[��")] private AudioClip gameOver;
    [SerializeField,Header("�{�^����")] private AudioClip select;

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
