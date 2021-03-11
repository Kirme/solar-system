using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    [Tooltip("Mass in terms of earth's mass")]
    [SerializeField] private float earths = 1f;

    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private bool inOrbit = false;
    private float mass;
    private ScaleManager scaleManager;

    private void Awake() {
        scaleManager = FindObjectOfType<ScaleManager>();
        mass = earths * scaleManager.GetMass(); //Set mass in kg
    }

    private void Start() {
        if (inOrbit) {
            Orbit orbit = GetComponent<Orbit>();
            velocity += orbit.GetVelocity();
        }
    }

    private void FixedUpdate() {
        if (inOrbit) {
            GameObject orbitAround = GetComponent<Orbit>().GetOrbitingAround();
            Attract(orbitAround.GetComponent<Body>());
        } else {
            Body[] bodies = FindObjectsOfType<Body>();

            foreach (Body b in bodies) {
                if (b != this)
                    Attract(b);
            }
        }
    }

    public float GetMass() {
        return mass;
    }

    public Vector3 GetVelocity() {
        return velocity;
    }

    public void SetInOrbit(bool to) {
        inOrbit = to;
    }

    public void Move() {
        Vector3 totalVelocity = velocity;
        if (inOrbit) {
            Vector3 orbitalSpeed = GetComponent<Orbit>().GetOrbitingAround().GetComponent<Body>().GetVelocity();
            totalVelocity += orbitalSpeed;
        }

        this.transform.position += totalVelocity * scaleManager.GetTime() * Time.fixedDeltaTime;
    }

    private void CalcVelocity(Vector3 force) {
        acceleration = force / mass; //Acceleration in m/s^2
        acceleration /= scaleManager.GetDistance(); //Acceleration in AU*s^-2
        
        velocity += acceleration * Time.fixedDeltaTime * scaleManager.GetTime();
    }

    private void Attract(Body otherBody) {
        Vector3 dir = (otherBody.transform.position - this.transform.position) * scaleManager.GetDistance();
        float distSqrd = dir.sqrMagnitude; //Distance in meters

        float forceMagnitude = scaleManager.GetG() * mass * otherBody.GetMass() / distSqrd; //G * m1 * m2 / d^2
        Vector3 gravForce = dir.normalized * forceMagnitude; //Force in Newtons
        
        CalcVelocity(gravForce);
        Move();
    }
}
