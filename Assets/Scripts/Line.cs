using UnityEngine;

public class Line : MonoBehaviour
{
    private const int VERTEX_COUNT = 256;
    private const float LINE_WIDTH = 0.02f;

    [Header("Bezier Curve Control Points")]
    public Vector3 p1; 
    public Vector3 p2;  
    public Vector3 p3;  
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InitializeLineRenderer();
    }

    private void Start()
    {
        DrawBezierCurve();
    }

    private void InitializeLineRenderer()
    {
        if (lineRenderer == null) return;
        
        lineRenderer.startWidth = LINE_WIDTH;
        lineRenderer.endWidth = LINE_WIDTH;
        lineRenderer.useWorldSpace = true;
    }

    private void DrawBezierCurve()
    {
        Vector3[] curvePoints = CalculateBezierPoints();
        lineRenderer.positionCount = curvePoints.Length;
        lineRenderer.SetPositions(curvePoints);
    }

    private Vector3[] CalculateBezierPoints()
    {
        Vector3[] points = new Vector3[VERTEX_COUNT + 1];
        
        for (int i = 0; i <= VERTEX_COUNT; i++)
        {
            float t = i / (float)VERTEX_COUNT;
            points[i] = CalculateQuadraticBezierPoint(t);
        }

        return points;
    }

    private Vector3 CalculateQuadraticBezierPoint(float t)
    {
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * p1 + 
               2f * oneMinusT * t * p2 + 
               t * t * p3;
    }
}