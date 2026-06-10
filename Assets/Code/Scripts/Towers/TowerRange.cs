using UnityEngine;
using UnityEngine.AI;

//Creates and enables/disables a towers range indicator upon selection

public class TowerRange : MonoBehaviour
{
    [SerializeField] private int segements = 50;
    [SerializeField] private LineRenderer lr;

    //Sets up variables and hides circle
    void Awake()
    {
        lr.positionCount = segements;
        lr.useWorldSpace = false;
        lr.loop = true;

        HideRange();
    }

    //Creates the range circle
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

    //Hides component
    public void HideRange() => lr.enabled = false;
}
