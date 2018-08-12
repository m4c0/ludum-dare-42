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

    private void SelectNextValue() {
        if (values.Count > 0) {
            number.value = values[Random.Range(0, values.Count)];
        } else {
            number.value = -1;
        }
    }
	
    public void AddPossibleValue(int n) {
        values.Add(n);
        if (CurrentValue == -1) number.value = n;
    }
    public void RemovePossibleValue(int n) {
        values.Remove(n);
        if (CurrentValue == n) SelectNextValue();
    }

}
