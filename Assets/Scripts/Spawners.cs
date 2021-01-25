using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField] CharMovement[] SpawnObjects;

    [SerializeField] Transform spawnArea, moveArea;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        GameObject.Instantiate(
            SpawnObjects.Random1(),
            spawnArea.position + new Vector3(
                (Random.value - 0.5f) * spawnArea.localScale.x,
                0,
                (Random.value - 0.5f) * spawnArea.localScale.z),
            Quaternion.identity).target = moveArea.position + new Vector3(
            (Random.value - 0.5f) * moveArea.localScale.x,
            0,
            (Random.value - 0.5f) * moveArea.localScale.z);
        yield return new WaitForSeconds(100 * Random.value);
        StartCoroutine(Spawn());
    }
}