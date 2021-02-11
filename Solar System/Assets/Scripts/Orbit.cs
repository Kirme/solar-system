using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
    private float localVelocity;
    private Vector3 velocity;
    [SerializeField] GameObject orbitAround;

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
        float axisLength = (orbitAround.transform.position - this.transform.position).magnitude * scaleManager.GetDistance(); //Axis length in m

        localVelocity = Mathf.Sqrt(scaleManager.GetG() * (orbitAround.GetComponent<Body>().GetMass() + myBody.GetMass()) / axisLength) * velocityMultiplier; //Velocity in m/s
        localVelocity /= scaleManager.GetDistance(); //Velocity in AU/s
        velocity = localVelocity * transform.forward;
    }

    public Vector3 GetVelocity() {
        return velocity;
        
    }

    public GameObject GetOrbitingAround() {
        return orbitAround;
    }
}
