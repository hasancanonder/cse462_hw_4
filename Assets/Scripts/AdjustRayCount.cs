using System.Collections.Generic;
using UnityEngine;

public class AdjustRayCount : MonoBehaviour
{
    [SerializeField] private List<GameObject> rays;

    public void IncreaseRayCount()
    {
        ChangeRayCount(1);
    }

    public void DecreaseRayCount()
    {
        ChangeRayCount(-1);
    }

    private void ChangeRayCount(int amount)
    {
        foreach (var ray in rays)
        {
            if (ray.TryGetComponent<RayGenerator>(out var rayGenerator))
            {
                int newCount = Mathf.Max(4, rayGenerator.rayCount + amount);
                rayGenerator.rayCount = newCount;
            }
        }
    }
}