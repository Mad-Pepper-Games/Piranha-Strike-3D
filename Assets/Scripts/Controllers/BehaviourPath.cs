using System.Collections.Generic;
using UnityEngine;

public class BehaviourPath : MonoBehaviour
{
    public List<Vector3> Positions = new List<Vector3>();

    public Vector3 FallingPosition;

    public int PositionIndex;

    private void Start()
    {
        for (int i = 0; i < Positions.Count; i++)
        {
            Positions[i] += transform.position;
        }
        FallingPosition += transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < Positions.Count; i++)
        {
            Gizmos.DrawSphere(transform.position+Positions[i], 0.2f);
            if(i+1 < Positions.Count)
            {
                Gizmos.DrawLine(transform.position + Positions[i], transform.position + Positions[i + 1]);
            }
        }
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + FallingPosition, 0.2f);
        Gizmos.DrawLine(transform.position + FallingPosition, transform.position + Positions[Positions.Count-1]);
    }
}
