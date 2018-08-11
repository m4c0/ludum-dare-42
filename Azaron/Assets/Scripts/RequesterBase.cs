using UnityEngine;

public class RequesterBase : MonoBehaviour {

    public Material ok;
    public Material nok;
    public Material wait;
    public Renderer baseRenderer;

    private int touchCount = 0;
    private bool invalid = false;

    private Cube validCube;

    void Update() {
        baseRenderer.sharedMaterial = invalid ? nok : validCube ? ok : wait;
    }

    void OnTriggerEnter(Collider other) {
        touchCount++;

        var cube = other.GetComponent<Cube>();
        if (!cube) {
            invalid = true;
            validCube = null;
            return;
        }
        if (cube.Id != Requester.Instance.CurrentValue) {
            invalid = true;
            validCube = null;
            return;
        }
        if (touchCount == 1) {
            Requester.Instance.RemovePossibleValue(cube.Id);
            validCube = cube;
            //Destroy(other.gameObject);
            //validCube = null;
        }
    }
    void OnTriggerExit(Collider other) {
        touchCount--;
        if (touchCount == 0) invalid = false;
        validCube = null;
    }

}
