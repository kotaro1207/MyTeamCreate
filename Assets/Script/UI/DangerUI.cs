using System.Collections;
using UnityEngine;

public class DangerUI : MonoBehaviour
{
    [SerializeField] private float second = 1f;
    [SerializeField] private AudioClip sound;

    private new AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(WaitDestroy(second));
        audio.PlayOneShot(sound); 
    }

    private IEnumerator WaitDestroy(float num)
    {
        yield return new WaitForSeconds(num);

        Destroy(gameObject);
    }
}
