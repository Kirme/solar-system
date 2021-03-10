/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitV1 : MonoBehaviour {
    [SerializeField] GameObject orbitAround;
    [SerializeField] private int speedUp = 10;

    private Body body;
    const float G = 6.67384e-11f;
    const float earthMass = 5.972e24f;
    private bool fast = false;
    private Vector3 previousPos;
    private Vector3 dir;
    private float modifier = 1e8f; //Compensation to make distance from sun correct (1 unit = 1e8 m)

    float velocity = 0f; 

    private void Start() {
        float axisLength = (orbitAround.transform.position - this.transform.position).magnitude * modifier;
        previousPos = this.transform.position;
        body = this.GetComponent<Body>();

        velocity = Mathf.Sqrt(G * (orbitAround.GetComponent<Body>().GetMass() + body.GetMass()) * earthMass / axisLength) / modifier;
        
        body.SetInOrbit(true);
        
        CalcStartDir();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (fast) {
                Time.timeScale = 1f;
            } else {
                Time.timeScale = 100f;
            }
            fast = !fast;
        }
    }

    private void FixedUpdate() {
        Move();
        //CalcDir();
    }

    private void CalcStartDir() {
        Vector3 normal = orbitAround.transform.position - this.transform.position;
        dir = Vector3.Cross(Vector3.up, normal);
    }

    private void CalcDir() {
        dir = this.transform.position - previousPos;
        previousPos = this.transform.position;
        
        //float axisLength = (orbitAround.transform.position - this.transform.position).magnitude * modifier;

        //velocity = Mathf.Sqrt(G * orbitAround.GetComponent<Body>().GetMass() / axisLength);
    }

    private void Move() {
        float dt = Time.fixedDeltaTime;
        Vector3 gravityAcc = body.GetAcceleration();
        dir += gravityAcc;
        Vector3 moveTo = dir.normalized * velocity;
        //Debug.Log("Sideways: " + velocity);
        //Debug.Log("Gravity: " + gravityAcc);
        
        this.transform.position += moveTo * dt;
        Debug.Log(velocity);
    }
}*/
