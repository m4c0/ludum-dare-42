using UnityEngine;

public class Flipper : MonoBehaviour {

    public GameObject on;
    public GameObject off;

    public void Flip() {
        on.SetActive(true);
        off.SetActive(false);
    }
}
