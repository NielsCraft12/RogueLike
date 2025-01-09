using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float speed;

    private MeshRenderer mesh;


    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // We move the texture constantly.
        mesh.material.mainTextureOffset = new Vector2(Time.time * speed, 0);
    }
}
