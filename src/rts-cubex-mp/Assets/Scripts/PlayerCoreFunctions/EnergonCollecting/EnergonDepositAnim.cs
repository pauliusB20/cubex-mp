using UnityEngine;

public class EnergonDepositAnim : MonoBehaviour
{
    [Range(1.2f, 30f)]
    [SerializeField] float animationSpeed = 1.5f;
    // Update is called once per frame
    void Update()
    {
         transform.Rotate(new Vector3(45f, 0f, 45f) * animationSpeed * Time.deltaTime);
    }
}
