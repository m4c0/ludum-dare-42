using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public float spawnTimeout = 10.0f;
    public float alarmTimeout = 10.0f;

    public Renderer platform;

    public Material ok;
    public Material nok;
    public Material warn;

    public Cube cubePrefab;

    private int enterCount = 0;
    private bool readyToSpawn = false;

    private void OnEnable() {
        StartCoroutine(SpawnLoop());
    }

    private void Update() {
        if (enterCount != 0) {
            platform.sharedMaterial = nok;
        } else if (readyToSpawn) {
            platform.sharedMaterial = warn;
        } else {
            platform.sharedMaterial = ok;
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
            readyToSpawn = false;
            yield return new WaitForSeconds(spawnTimeout - alarmTimeout);
            readyToSpawn = true;
            yield return new WaitForSeconds(spawnTimeout);
            Spawn();
        }
    }

    private void Spawn() {
        var cube = Instantiate(cubePrefab);
        cube.transform.position = transform.position;
    }
}
