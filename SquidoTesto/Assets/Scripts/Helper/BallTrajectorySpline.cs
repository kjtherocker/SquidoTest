using UnityEngine;


public class BallTrajectorySpline : MonoBehaviour
{
    [Header("Refs")]
    public Transform startPoint;
    [Header("Settings")]
    public int resolution = 40;
    public float simulationTime = 2.5f;
    
    public LineRenderer lineRenderer;
    public LayerMask collisionMask;

    private Vector3 velocity;
    
    public void SetVelocity(Vector3 v)
    {
        velocity = v;
        DrawTrajectory();
    }
    
    public void ClearLineRenderer()
    {
        lineRenderer.positionCount = 0;
    }

    public void DrawTrajectory()
    {
        Vector3 startPos = startPoint.position;

        lineRenderer.positionCount = resolution;

        Vector3 prevPoint = startPos;
        lineRenderer.SetPosition(0, startPos);

        int pointsUsed = 1;

        for (int i = 1; i < resolution; i++)
        {
            float AmountOfSteps = (i / (float)(resolution - 1)) * simulationTime;

            Vector3 currentPoint =
                startPos +
                velocity * AmountOfSteps +
                0.5f * Physics.gravity * (AmountOfSteps * AmountOfSteps);

          
            Vector3 dir = currentPoint - prevPoint;
            float dist = dir.magnitude;

            if (Physics.Raycast(prevPoint, dir.normalized, out RaycastHit hit, dist, collisionMask))
            {
                lineRenderer.SetPosition(pointsUsed, hit.point);
                pointsUsed++;
                break;
            }

            lineRenderer.SetPosition(pointsUsed, currentPoint);
            prevPoint = currentPoint;
            pointsUsed++;
        }

        lineRenderer.positionCount = pointsUsed;
    }
}