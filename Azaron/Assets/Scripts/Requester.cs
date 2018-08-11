using System.Collections.Generic;
using UnityEngine;

public class Requester : MonoBehaviour {

    public static Requester Instance { get; private set; }

    public Number number;

    public int CurrentValue {
        get { return number.value; }
    }

    private List<int> values = new List<int>();

    void Awake() {
        Instance = this;
    }
    void Start() {
        SelectNextValue();
    }

    private void SelectNextValue() {
        number.value = values[Random.Range(0, values.Count)];
    }
	
    public void AddPossibleValue(int n) {
        values.Add(n);
    }
    public void RemovePossibleValue(int n) {
        values.Remove(n);
        if (CurrentValue == n) SelectNextValue();
    }

}
