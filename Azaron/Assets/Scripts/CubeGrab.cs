using UnityEngine;

public class CubeGrab : MonoBehaviour {

    public LayerMask mask;
    public float maxDistance = 10;
    public float castRadius = 0.4f;
    public float throwIntensity = 3.0f;

    private Rigidbody cube;

	void Update () {
        if (Input.GetMouseButton(0)) {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, castRadius, transform.forward, out hit, maxDistance, mask)) {
                var body = hit.transform.GetComponent<Rigidbody>();
                if (body != cube) {
                    BreakConnection();
                }
                if (body) {
                    cube = body;
                    Attach();
                }
            }
        } else if (Input.GetMouseButtonUp(0)) {
            BreakConnection();
        }
    }

    private void Attach() {
        cube.useGravity = false;
        cube.freezeRotation = true;
        cube.transform.SetParent(this.transform, true);
    }

    private void BreakConnection() {
        if (!cube) return;

        var x = Input.GetAxis("Mouse X") * transform.right;
        var y = Input.GetAxis("Mouse Y") * transform.up;

        cube.freezeRotation = false;
        cube.useGravity = true;
        cube.transform.SetParent(null, true);
        cube.AddForce((x + y) * throwIntensity, ForceMode.Impulse);
        cube = null;
    }
}
