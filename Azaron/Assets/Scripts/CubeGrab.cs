using UnityEngine;

public class CubeGrab : MonoBehaviour {

    public LayerMask mask;
    public float maxDistance = 10;
    public float castRadius = 0.4f;

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
        cube.transform.SetParent(this.transform, true);
    }

    private void BreakConnection() {
        if (!cube) return;

        cube.useGravity = true;
        cube.transform.SetParent(null, true);
        cube = null;
    }
}
