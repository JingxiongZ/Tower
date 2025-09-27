using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] wayPoints;
    void Start()
    {

    }

    void Update()
    {

    }

    void Awake()
    {
        wayPoints = new Transform[transform.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }
}
