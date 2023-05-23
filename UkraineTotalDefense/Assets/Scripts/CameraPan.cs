using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    [SerializeField] GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;

    private GameManagerBehavior gameManager;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2.0f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2.0f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2.0f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsClickingOverUI())
        //{
        //    return;
        //}

        if (!gameManager.isGamePaused)
        {
            PanCamera();
        }
    }

    private void PanCamera()
    {
        // Save position of mouse in world space when drag starts (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        RaycastHit2D hit = Physics2D.Raycast(dragOrigin, Vector2.zero);

        //if (IsClickingOverUI())
        //{
        //    return;
        //}

        if (hit || IsClickingOverUI())
        {
            if (hit)
            {
                // Debug.LogWarning("Click in UI: " + hit.collider.gameObject);

                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
            }
        }
        //if (hit)
        //{
        //    // Debug.LogWarning("Click in UI: " + hit.collider.gameObject);

        //    IClickable clickable = hit.collider.GetComponent<IClickable>();
        //    clickable?.Click();
        //}
        else
        {
            // Calculate distance between drag origin and new position if it is stil held down
            if (Input.GetMouseButton(0))
            {
                Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

                if (!IsClickingOverUI())
                {
                    gameManager.upgradePanel.SetActive(false);

                    GameObject selectedOpenspot = gameManager.currentOpenspot;
                    if (selectedOpenspot)
                    {
                        selectedOpenspot.GetComponent<PlaceUnit>().selectedDecal.SetActive(false);
                    }
                }

                // difference = new Vector3(difference.x, difference.y, -10.0f);

                //print("origin " + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(Input.mousePosition)
                //    + " = difference " + difference);

                // Debug.Log("Click out of UI");

                // Move the camera by the distance
                cam.transform.position = ClampCamera(cam.transform.position + difference);
            }
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

    private bool IsClickingOverUI()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);

        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            if (results[0].gameObject.tag == "UI_HUD")
            {
                // Debug.Log("Hit " + results[0].gameObject.name);

                return true;
            }
        }

        return false;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    //Set up the new Pointer Event
        //    m_PointerEventData = new PointerEventData(m_EventSystem);

        //    //Set the Pointer Event Position to that of the mouse position
        //    m_PointerEventData.position = Input.mousePosition;

        //    //Create a list of Raycast Results
        //    List<RaycastResult> results = new List<RaycastResult>();

        //    //Raycast using the Graphics Raycaster and mouse click position
        //    m_Raycaster.Raycast(m_PointerEventData, results);

        //    if (results.Count > 0)
        //    {
        //        if (results[0].gameObject.tag == "UI_HUD")
        //        {
        //            Debug.Log("Hit " + results[0].gameObject.name);

        //            return true;
        //        }
        //    }
        //}

        //return false;
    }
}
