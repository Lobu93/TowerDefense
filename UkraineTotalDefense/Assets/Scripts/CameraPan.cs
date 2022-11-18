using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    [SerializeField]
    private SpriteRenderer mapRenderer;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private Vector3 dragOrigin;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2.0f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2.0f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2.0f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        // Save position of mouse in world space when drag starts (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        // Calculate distance between drag origin and new position if it is stil held down
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            print("origin " + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(Input.mousePosition)
                + " = difference " + difference);

            // Move the camera by the distance
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
