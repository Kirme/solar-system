using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    private float localVelocity;
    private Vector3 velocity;
    [SerializeField] GameObject orbitAround;
    private bool lastChangePositive = false;
    private float lastX;

    [Range(0f, 2f)]
    [SerializeField] float velocityMultiplier = 1f;

    private Body myBody;
    private ScaleManager scaleManager;

    // Start is called before the first frame update
    private void Awake() {
        myBody = GetComponent<Body>();
        myBody.SetInOrbit(true);
        scaleManager = FindObjectOfType<ScaleManager>();
    }

    private void Start() {
        lastX = this.transform.position.x;
        float axisLength = (orbitAround.transform.position - this.transform.position).magnitude * scaleManager.GetDistance(); //Axis length in m

        //Equation: sqrt(G*M/r)
        localVelocity = Mathf.Sqrt(scaleManager.GetG() * orbitAround.GetComponent<Body>().GetMass() / axisLength) * velocityMultiplier; //Velocity in m/s
        //Debug.Log(localVelocity);
        localVelocity /= scaleManager.GetDistance(); //Velocity in AU/s
        velocity = localVelocity * transform.forward;
    }

    //Find the aphelion and perihelion
    private void FixedUpdate() {
        float thisX = this.transform.position.x;
        float change = thisX - lastX;
        if (change > 0 && !lastChangePositive) {
            Debug.Log("Aphelion: " + thisX);
        } else if (change <= 0 && lastChangePositive) {
            Debug.Log("Perihelion: " + thisX);
        }

        lastChangePositive = change > 0;
        lastX = thisX;
    }

    public Vector3 GetVelocity() {
        return velocity;
        
    }

    public GameObject GetOrbitingAround() {
        return orbitAround;
    }
}
