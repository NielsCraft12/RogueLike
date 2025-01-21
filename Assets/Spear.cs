using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    private GameObject _player;
    private Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, 3);
        _player = GameObject.FindWithTag("Player");
        Vector3 _relativePos = _player.transform.position;
        Quaternion _lookAt = Quaternion.LookRotation(
            Vector3.forward,
            _relativePos - transform.position
        );
        transform.rotation = _lookAt;
        direction = (_player.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            transform.position += direction * moveSpeed * Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
        {
            gameObject.GetComponent<AttackController>().Hit();
            Destroy(gameObject);
        }
    }
}
