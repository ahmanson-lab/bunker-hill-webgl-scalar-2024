using UnityEngine;

public class Controls2 : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 100.0f;

    public Vector3 minPosition;
    public Vector3 maxPosition;

    void Update()
    {
        // Movement forward and backward
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        // Rotation left and right
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }

        // Movement up and down
        if (Input.GetKey(KeyCode.O))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        // Clamp position
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x),
            Mathf.Clamp(transform.position.y, minPosition.y, maxPosition.y),
            Mathf.Clamp(transform.position.z, minPosition.z, maxPosition.z)
        );
    }
}
