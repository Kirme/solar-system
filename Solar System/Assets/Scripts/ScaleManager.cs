using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    private const float G = 6.67384e-11f; //Gravitational constant
    private const float MASS_UNIT = 5.9742e24f; //1 earth mass in kg
    private const float DISTANCE_UNIT = 1.49599e11f; //1 AU in meters
    private const float TIME_UNIT = 8.64e4f; //1 day in seconds 

    public float GetG() {
        return G;
    }
    public float GetMass() {
        return MASS_UNIT;
    }
    public float GetDistance() {
        return DISTANCE_UNIT;
    }
    public float GetTime() {
        return TIME_UNIT;
    }
}
