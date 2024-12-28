using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RayGenerator : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] public int rayCount = 8;
    [SerializeField] private GameObject prefab;
    private GameObject blackHole;
    private IEnumerator refresh;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetBlackHole();
        refresh = RefreshRays();
        StartCoroutine(refresh);
        SpawnRays();
    }

    private void Update()
    {
        
    }

    private void SetBlackHole()
    {
        blackHole = GameObject.Find("BlackHole") ?? GameObject.Find("BlackHole(Clone)");
    }

    private void SpawnRays()
    {
        for (int i = 0; i < rayCount; i++)
        {
            for (int j = 0; j < rayCount; j++)
            {
                float x = Mathf.Sin(i) * Mathf.Cos(j);
                float y = Mathf.Sin(i) * Mathf.Sin(j);
                float z = Mathf.Cos(i);
                Vector3 point = new Vector3(x * 6, y * 6, z * 6);

                GameObject obj = Instantiate(prefab, transform);
                var lineComponent = obj.GetComponent<Line>();
                lineComponent.p1 = transform.position;
                lineComponent.p2 = point;
                lineComponent.p3 = blackHole != null ? blackHole.transform.position : point;
            }
        }
    }

    private IEnumerator RefreshRays()
    {
        while (true)
        {
            SetBlackHole();

            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            SpawnRays();
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}