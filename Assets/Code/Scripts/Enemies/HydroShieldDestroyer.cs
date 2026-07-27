using UnityEngine;

public class HydroShield : MonoBehaviour
{
    [SerializeField] private GameObject hydrogenShieldleft;
    [SerializeField] private GameObject hydrogenShieldright;
    void OnDestroy()
    {
        Destroy(hydrogenShieldleft);
        Destroy(hydrogenShieldright);
    }
}
