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

    private Rigidbody toSpawn;

    private void OnEnable() {
        StartCoroutine(SpawnLoop());
    }

    private void Update() {
        if (enterCount != 0) {
            platform.sharedMaterial = nok;
        } else if (toSpawn) {
            platform.sharedMaterial = warn;
        } else {
            platform.sharedMaterial = ok;
        }

        if (toSpawn) {
            toSpawn.transform.Rotate(0, 100 * Time.deltaTime, 0);
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
            yield return new WaitForSeconds(spawnTimeout - alarmTimeout);
            Spawn();
            yield return new WaitForSeconds(spawnTimeout);

            toSpawn.GetComponent<Collider>().enabled = true;
            toSpawn.useGravity = true;
            toSpawn.isKinematic = false;
            toSpawn = null;
        }
    }

    private void Spawn() {
        var cube = Instantiate(cubePrefab);
        cube.transform.position = transform.position;

        cube.GetComponent<Collider>().enabled = false;

        toSpawn = cube.GetComponent<Rigidbody>();
        toSpawn.useGravity = false;
        toSpawn.isKinematic = true;
    }
}
