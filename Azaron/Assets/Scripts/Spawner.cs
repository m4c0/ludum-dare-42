using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float spawnTimeout = 10.0f;

    public Renderer platform;

    public Material ok;
    public Material nok;
    public Material warn;

    public Cube cubePrefab;

    private int enterCount = 0;

    private void OnEnable() {
        StartCoroutine(SpawnLoop());
    }

    private void Update() {
        if (enterCount == 0) {
            platform.sharedMaterial = ok;
        } else {
            platform.sharedMaterial = warn;
        }
    }

    private void OnTriggerEnter(Collider other) {
        enterCount++;
    }
    private void OnTriggerExit(Collider other) {
        enterCount--;
    }

    private IEnumerator SpawnLoop() {
        while (isActiveAndEnabled) {
            Spawn();
            yield return new WaitForSeconds(spawnTimeout);
        }
    }

    private void Spawn() {
        var cube = Instantiate(cubePrefab);
        cube.transform.position = transform.position;
    }
}
