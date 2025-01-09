using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 10f;

    private Vector3 cameraPosition;


    private void FixedUpdate()
    {

        cameraPosition = new Vector3(
            Mathf.SmoothStep(transform.position.x, target.transform.position.x, speed * Time.fixedDeltaTime),
            Mathf.SmoothStep(transform.position.y, target.transform.position.y, speed * Time.fixedDeltaTime));
    }

    private void LateUpdate()
    {

        transform.position = cameraPosition + Vector3.forward * -10;
    }
}