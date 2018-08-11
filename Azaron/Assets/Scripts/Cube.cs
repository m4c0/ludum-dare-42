using UnityEngine;

public class Cube : MonoBehaviour {

    public int Id { get; private set; }

	void Start () {
        Id = Random.Range(0, 1000);

        foreach (var number in GetComponentsInChildren<Number>()) {
            number.value = Id;
        }

        transform.localScale = Vector3.one * Random.Range(0.5f, 3.0f);

        Requester.Instance.AddPossibleValue(Id);
	}
}
