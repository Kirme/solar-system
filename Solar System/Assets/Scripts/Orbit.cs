using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    const float G = 6.67384e-11f; //Gravitational constant
    const float DIST_SCALE = 1e8f; //1 unit of length is 10 000 000 m
    const float VEL_SCALE = 800f;

    private float localVelocity;
    private Vector3 velocity;
    [SerializeField] GameObject orbitAround;

    [Range(0f, 2f)]
    [SerializeField] float velocityMultiplier = 1f;

    private Body myBody;

    // Start is called before the first frame update
    private void Awake() {
        myBody = GetComponent<Body>();
        myBody.SetInOrbit(true);
    }

    private void Start() {
        float axisLength = (orbitAround.transform.position - this.transform.position).magnitude * DIST_SCALE;

        localVelocity = Mathf.Sqrt(G * (orbitAround.GetComponent<Body>().GetMass() + myBody.GetMass()) / axisLength) /  VEL_SCALE * velocityMultiplier;
        velocity = localVelocity * transform.forward;
    }

    public Vector3 GetVelocity() {
        return velocity;
        
    }

    public GameObject GetOrbitingAround() {
        return orbitAround;
    }
}
