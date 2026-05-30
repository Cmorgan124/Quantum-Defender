using UnityEngine;
using UnityEngine.AI;

public class TurretRange : MonoBehaviour
{
    [SerializeField] private int segements = 50;
    [SerializeField] private LineRenderer lr;

    void Awake()
    {
        lr.positionCount = segements;
        lr.useWorldSpace = false;
        lr.loop = true;

        HideRange();
    }

    public void ShowRange(float radius)
    {
        float angleStep = 360f/segements;

        for(int i = 0; i < segements; i++)
        {
            float currentAngle = i * angleStep;

            float x = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * radius; 
            float y = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * radius; 

            lr.SetPosition(i, new Vector3(x, y, 0f));      
        }

        lr.enabled = true;
    }

    public void HideRange() => lr.enabled = false;
}
