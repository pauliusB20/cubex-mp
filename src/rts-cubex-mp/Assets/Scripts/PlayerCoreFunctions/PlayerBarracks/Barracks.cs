using UnityEngine;

public class Barracks : MonoBehaviour {
    [Range (1.5f, 30f)]
    [SerializeField] float animationSpeed = 1.5f;
    // Update is called once per frame
    void Update () {
        transform.Rotate (new Vector3 (60f, 60f, 60f) * animationSpeed * Time.deltaTime);
    }
}