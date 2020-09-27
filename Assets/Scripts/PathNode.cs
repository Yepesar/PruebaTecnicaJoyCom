using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    [SerializeField] public Color linerColor;

    private List<Transform> nodes = new List<Transform>();

    private void OnDrawGizmos()
    {
        Gizmos.color = linerColor;
        Transform[] pathTrasforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTrasforms.Length; i++)
        {
            if (pathTrasforms[i] != transform)
            {
                nodes.Add(pathTrasforms[i]);
            }          
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 previusNode = Vector3.zero;

            if (i > 0)
            {
                 previusNode = nodes[i - 1].position;
            }
            else if (i == 0 && nodes.Count > 1)
            {
                previusNode = nodes[nodes.Count - 1].position;
            }

            Gizmos.DrawLine(previusNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, 0.3f);
        }
    }
}
