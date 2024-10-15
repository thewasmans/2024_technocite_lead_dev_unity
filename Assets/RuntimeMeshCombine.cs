using System.Collections;
using UnityEngine;

public class RuntimeMeshCombine : MonoBehaviour
{
    public GameObject Prefab;
    WaitForSeconds waitForSeconds = new WaitForSeconds(.5f);

    IEnumerator Start()
    {
        for (int i = 0; i < Random.Range(4, 10); i++)
        {
            yield return waitForSeconds;

            GameObject instance = Instantiate(Prefab);
            instance.transform.SetParent(transform);
        }
        
        yield return waitForSeconds;

        StaticBatchingUtility.Combine(gameObject);
    }
}
