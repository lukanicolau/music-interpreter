using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    private float originalZoom;
    private Vector3 originalPosition;

    private Vector3 targetPosition;
    private float targetZoom;
    private float transitionSpeed;

    private void Start()
    {
        originalPosition = transform.position;
        camera = GetComponent<Camera>();
        originalZoom = camera.orthographicSize;
        enabled = false;
    }

    private void Update()
    {
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, transitionSpeed);
        transform.position = Vector3.Lerp(transform.position, targetPosition, transitionSpeed);
        if (transform.position == targetPosition && camera.orthographicSize == targetZoom)
        {
            enabled = false;
        }
    }
    public void ZoomIn(Vector3 targetPosition, float zoomAmount, float transitionSpeed)
    {
        this.targetPosition = new Vector3(targetPosition.x,targetPosition.y,transform.position.z);
        targetZoom = zoomAmount;
        this.transitionSpeed = transitionSpeed;
        enabled = true;
    }

    public void ZoomOut()
    {
        targetPosition = originalPosition;
        targetZoom = originalZoom;
        enabled = true;
    }
}
