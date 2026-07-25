using UnityEngine;

public class HydroShield : MonoBehaviour
{
    [SerializeField] private GameObject hydrogen;
    void Update()
    {
        if (!hydrogen)
        {
            Destroy(this);
        }
    }
}
