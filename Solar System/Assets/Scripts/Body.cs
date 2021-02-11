using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    const float G = 6.67384e-11f; //Gravitational constant
    const float MASS_SCALE = 5.9722e24f; //1 unit of mass is one earth mass in kg
    const float DIST_SCALE = 1e8f; //1 unit of length is 10 000 000 m

    [Tooltip("Mass in terms of earth's mass")]
    [SerializeField] private float earths = 1f;

    private Vector3 acceleration = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private bool inOrbit = false;
    private float mass;

    private void Awake() {
        mass = earths * MASS_SCALE;
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
            //Debug.Log("GRAV VEL: " + velocity);
            //Debug.Log("ORBIT VEL: " + orbitalSpeed);
        }

        this.transform.position += totalVelocity * Time.fixedDeltaTime;
    }

    private void CalcVelocity(Vector3 force) {
        acceleration = force / mass;
        velocity += acceleration * Time.fixedDeltaTime;
    }

    private void Attract(Body otherBody) {
        Vector3 dir = (otherBody.transform.position - this.transform.position) * DIST_SCALE;
        float distSqrd = dir.sqrMagnitude;

        float forceMagnitude = mass * otherBody.GetMass() / distSqrd; //G * m1 * m2 / d^2
        Vector3 gravForce = dir.normalized * forceMagnitude / DIST_SCALE;
        CalcVelocity(gravForce);

        Move();
    }
}
