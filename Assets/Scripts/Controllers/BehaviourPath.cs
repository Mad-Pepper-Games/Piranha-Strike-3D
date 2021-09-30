using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourPath : MonoBehaviour
{
    public List<Vector3> Positions = new List<Vector3>();

    public Vector3 FallingPosition;

    public int PositionIndex;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < Positions.Count; i++)
        {
            Gizmos.DrawSphere(Positions[i], 0.2f);
            if(i+1 < Positions.Count)
            {
                Gizmos.DrawLine(Positions[i], Positions[i + 1]);
            }
        }
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(FallingPosition, 0.2f);
        Gizmos.DrawLine(FallingPosition, Positions[Positions.Count-1]);
    }
}
