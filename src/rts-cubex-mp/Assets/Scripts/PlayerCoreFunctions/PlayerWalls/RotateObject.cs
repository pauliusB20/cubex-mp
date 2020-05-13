using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    [SerializeField] GameObject wall;
    public void rotate () {
        wall.transform.Rotate (0f, -45f, 0f);
    }
    public void rotateClockwise () {
        wall.transform.Rotate (0f, 45f, 0f);
    }
}