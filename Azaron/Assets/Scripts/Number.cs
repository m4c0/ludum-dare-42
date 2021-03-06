﻿using UnityEngine;

[ExecuteInEditMode]
public class Number : MonoBehaviour {

    private int oldValue = -2;
    public int value = -1;

    [SerializeField]
    private Sprite[] allDigits;
    [SerializeField]
    private SpriteRenderer[] panelDigits;

    void Update() {
        if (oldValue == value) return;

        var v = value;
        for (int i = panelDigits.Length - 1; i >= 0; i--) {
            if (value == -1) {
                panelDigits[i].sprite = null;
                continue;
            }

            var digit = v % 10;
            panelDigits[i].sprite = allDigits[digit];
            v /= 10;
        }

        oldValue = value;
    }
}
