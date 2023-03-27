using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10;
    public int damage;
    public GameObject target;
    public bool isGuided;
    public bool isCurvedTrajectory;
    public Vector3 curvedMidPointElevation;
    public LineRenderer lineRenderer;
    
    public Vector3 startPosition;
    public Vector3 targetPosition;    

    private float distance;
    private float startTime;

    private GameManagerBehavior gameManager;

    private int numPoints = 50;
    private Vector3[] positions = new Vector3[50];

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehavior>();

        if (!isGuided)
        {
            CalculateRotation();
        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
        curvedMidPointElevation = Vector3.Lerp(startPosition, targetPosition, 0.5f);
        curvedMidPointElevation += new Vector3(0.0f, 0.0f, distance);

        DrawQuadraticCurve();
        
        Debug.Log("curvedMidPointElevation: " + curvedMidPointElevation);
    }

    // Update is called once per frame
    void Update()
    {
        float timeInterval = Time.time - startTime;

        if (isCurvedTrajectory)
        {
            // gameObject.transform.position = 
            // CalculateQuadraticBezierPoint(timeInterval, startPosition, curvedMidPointElevation, targetPosition);
            Vector3 curvedValueLocal = CalculateQuadraticBezierPoint(
                timeInterval * speed / distance, startPosition, curvedMidPointElevation, targetPosition);

            gameObject.transform.localScale = new Vector3(curvedValueLocal.z + 1.0f, curvedValueLocal.z + 1.0f, 1.0f);
        }

        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if (isGuided)
        {
            CalculateRotation();
        }

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth -= Mathf.Max(damage, 0);

                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);
                    AudioSource audioSource = target.GetComponent<AudioSource>();
                    AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);

                    gameManager.Gold += 50;
                }
            }
            Destroy(gameObject);
        }
    }

    private void CalculateRotation()
    {
        Vector3 direction = startPosition - target.transform.position;
        gameObject.transform.rotation = Quaternion.AngleAxis(
            Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
            new Vector3(0, 0, 1));
    }

    private void DrawQuadraticCurve()
    {
        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            positions[i - 1] = CalculateQuadraticBezierPoint(t, startPosition, curvedMidPointElevation, targetPosition);
        }

        lineRenderer.SetPositions(positions);
    }

    private Vector3 CalculateQuadraticBezierPoint(float timeParam, Vector3 point_0, Vector3 point_1, Vector3 point_2)
    {
        // B(t) = (1-t)2 P0 + 2(1-t)tP1 + t2P2
        //          u            u        tt
        //         uu * P0 + 2 * u * t * P1 + tt * P2

        float u = 1 - timeParam;
        float tt = timeParam * timeParam;
        float uu = u * u;

        Vector3 pointLocal = uu * point_0;
        pointLocal += 2 * u * timeParam * point_1;
        pointLocal += tt * point_2;

        return pointLocal;
    }
}
