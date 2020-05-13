using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningStationAnim: MonoBehaviour
{
    [SerializeField] float speed = 2f;

    [Range(-1f, 1f)]
    [SerializeField] float direction = 1f;
    void Update()
    {
        transform.Rotate(new Vector3(0f, 45f, 0f) * speed * Time.deltaTime * Mathf.Sign(direction));
    }
}
