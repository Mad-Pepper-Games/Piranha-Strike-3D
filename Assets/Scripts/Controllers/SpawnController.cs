using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public float SpawnArea;
    public GameObject FishPrefab;
    private int localFishValue;

    private void OnEnable()
    {
        localFishValue = 1;
        Spawn();
    }

    public void Spawn()
    {
        for (int i = 0; i < localFishValue; i++)
        {
            Vector3 RandomPos = Random.insideUnitSphere * SpawnArea;
            RandomPos = new Vector3(RandomPos.x, 0, RandomPos.z);
            GameObject localFish = Instantiate(FishPrefab, transform.position + RandomPos, Quaternion.identity);
            localFish.transform.parent = transform;
            localFish.transform.parent = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SpawnArea);
    }
}
