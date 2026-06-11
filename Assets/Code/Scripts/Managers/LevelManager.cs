using UnityEngine;

//The main man. A singleton that tracks all the global stuff like currency and enemy pathing. 

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //sets starting currency
    private void Start()
    {
        currency = 1000000;
    }

    //increases currency based on enemy type
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    //spends currency based on tower type or upgrade
    public bool SpendCurrency(int amount) 
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        } else
        {
            Debug.Log("Insuffiecent funds");
            return false;
        }
    }
}
