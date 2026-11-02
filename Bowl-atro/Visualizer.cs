using UnityEngine;

public class Visualizer : MonoBehaviour
{
    public float radius = 0.05f;
    public Color color = Color.red;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnDrawGizmos()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb) return;

        Gizmos.color = color;
        Vector3 worldCOM = transform.TransformPoint(rb.centerOfMass);
        Gizmos.DrawSphere(worldCOM, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
