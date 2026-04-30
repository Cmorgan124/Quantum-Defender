using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    private void Awake()
    {
        main = this;
    }

    //sets starting currency
    private void Start()
    {
        currency = 100;
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
