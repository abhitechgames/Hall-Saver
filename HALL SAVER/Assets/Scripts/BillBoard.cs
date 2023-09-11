using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform cam;

    void Start() => cam = Camera.main.transform;

    private void Update()
    {
        transform.LookAt(cam);
    }
}
