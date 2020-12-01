using UnityEngine;

public class Knob : MonoBehaviour
{
    public float value;
    public float incrementSpeed = 0.001f;

    private Vector2 mouseRelativePosition;
    private Camera myCamera;

    void Start()
    {
        myCamera = FindObjectOfType<Camera>();
        enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, -270f * value + 135);
    }

    // Update is called once per frame
    void Update()
    {
        mouseRelativePosition = (Vector2)myCamera.WorldToScreenPoint(transform.position) - (Vector2)Input.mousePosition;
        Debug.Log(mouseRelativePosition.magnitude);
        if (mouseRelativePosition.x > 0)
        {
            value -= incrementSpeed * mouseRelativePosition.magnitude;
        }
        else if (mouseRelativePosition.x < 0)
        {
            value += incrementSpeed * mouseRelativePosition.magnitude;
        }
        value = Mathf.Clamp(value, 0, 1);
        transform.rotation = Quaternion.Euler(0, 0, -270f*value+135);
    }

    private void OnMouseDown()
    {
        enabled = true;
    }

    private void OnMouseUp()
    {
        enabled = false;
    }
}
