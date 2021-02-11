using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {
    private bool fast = false;

    private void Start() {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    private void Update() {

        if (Input.GetButtonDown("Fire1"))
            ChangeTimeScale();
    }

    private void ChangeTimeScale() {
        if (fast) {
            Time.timeScale = 1f;
        } else {
            Time.timeScale = 10f;
        }
        fast = !fast;
    }
}
