using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float horizontalSpeed = 1;
    public float verticalSpeed = 1;

    public float horizontalRotationSpeed = 1;
    public float verticalRotationSpeed = 1;

    public float maxVerticalAngle = 45;

    public Transform head;

    private CharacterController cc;
    private float headAngle = 0;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

    private void OnDisable() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update () {
        headAngle += verticalRotationSpeed * -Input.GetAxis("Mouse Y");
        headAngle = Mathf.Clamp(headAngle, -maxVerticalAngle, maxVerticalAngle);
        head.localRotation = Quaternion.Euler(headAngle, 0, 0);

        var x = horizontalRotationSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, x, 0);

        var h = Input.GetAxis("Horizontal") * horizontalSpeed * transform.right;
        var v = Input.GetAxis("Vertical") * verticalSpeed * transform.forward;
        cc.SimpleMove(h + v);
    }
}
