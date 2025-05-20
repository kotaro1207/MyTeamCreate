using System.Collections;
using UnityEngine;

public class DangerUI : MonoBehaviour
{
    [SerializeField] private float second = 1f;

    private void Start()
    {
        StartCoroutine(WaitDestroy(second));
    }

    private IEnumerator WaitDestroy(float num)
    {
        yield return new WaitForSeconds(num);

        Destroy(gameObject);
    }
}
