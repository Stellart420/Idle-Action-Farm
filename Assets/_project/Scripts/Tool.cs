using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [HideInInspector] public bool IsWorking;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsWorking) return;

        if (other.transform.parent != null)
        {
            if (other.transform.parent.TryGetComponent(out Crop crop))
            {
                Collider otherCollider = other.GetComponent<Collider>();
                Vector3 closestPoint = otherCollider.ClosestPointOnBounds(transform.position);
                crop.OnHarvest(closestPoint);
            }
        }
    }
}
