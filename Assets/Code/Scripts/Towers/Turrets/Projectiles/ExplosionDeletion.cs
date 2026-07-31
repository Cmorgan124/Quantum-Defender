using UnityEngine;

public class ExplosionDeletion : MonoBehaviour
{
    [SerializeField] float lifetime;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
