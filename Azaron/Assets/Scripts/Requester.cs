using UnityEngine;

public class Requester : MonoBehaviour {

    private Number number;

	void Start () {
        number.value = Random.Range(0, 1000);
	}
	
}
