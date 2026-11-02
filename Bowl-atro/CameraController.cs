using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Player Object
    public GameObject player;

    // Camera Offset from player
    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // calculate initial offset of camer from player
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // update camera position to maintain offset
        transform.position = player.transform.position + offset;
    }
}
