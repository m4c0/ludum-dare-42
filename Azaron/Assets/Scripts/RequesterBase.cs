using System.Collections;
using UnityEngine;

public class RequesterBase : MonoBehaviour {

    public Material ok;
    public Material nok;
    public Material wait;
    public Renderer baseRenderer;

    public AudioSource woosh;

    private new Collider collider;

    private Rigidbody validCube;
    private bool wentInvalid = false;
    private int touchCount = 0;
    private bool locked = false;

    void Awake() {
        collider = GetComponent<Collider>();
    }

    void Update() {
        if (locked) {
            if (validCube) {
                validCube.transform.Rotate(0, 100 * Time.deltaTime, 0);
                validCube.transform.Translate(0, 10 * Time.deltaTime, 0);
                validCube.transform.localScale *= 0.95f;
            }
            baseRenderer.sharedMaterial = ok;
            return;
        }
        if (wentInvalid) {
            baseRenderer.sharedMaterial = nok;
            return;
        }
        if (!validCube) {
            baseRenderer.sharedMaterial = wait;
            return;
        }
        if (!validCube.useGravity) { // Player is holding
            baseRenderer.sharedMaterial = wait;
            return;
        }
        if (validCube.velocity.sqrMagnitude > 0.001) {
            baseRenderer.sharedMaterial = wait;
            return;
        }
        var cupos = validCube.transform.position;
        var mepos = transform.position;

        var cupos2 = new Vector2(cupos.x, cupos.z);
        var mepos2 = new Vector2(mepos.x, mepos.z);

        if ((cupos2 - mepos2).sqrMagnitude > 4) {
            baseRenderer.sharedMaterial = wait;
            return;
        }

        locked = true;
        validCube.useGravity = false;
        validCube.isKinematic = true;
        collider.isTrigger = false;

        StartCoroutine(RaiseTheBar());
    }

    void OnTriggerEnter(Collider other) {
        touchCount++;

        if (wentInvalid) return;

        var cube = other.GetComponent<Cube>();
        if (!cube) {
            wentInvalid = true;
            return;
        }
        if (cube.Id != Requester.Instance.CurrentValue) {
            wentInvalid = true;
            return;
        }

        var body = cube.GetComponent<Rigidbody>();
        if (validCube && (validCube != body)) {
            wentInvalid = true;
            return;
        }

        validCube = body;
    }
    void OnTriggerExit(Collider other) {
        touchCount--;
        if (touchCount == 0) wentInvalid = false;
    }

    private IEnumerator RaiseTheBar() {
        woosh.Play();

        yield return new WaitForSeconds(3);

        Requester.Instance.RemovePossibleValue(validCube.GetComponent<Cube>().Id);
        Destroy(validCube.gameObject);

        locked = false;
        collider.isTrigger = true;
    }
}
