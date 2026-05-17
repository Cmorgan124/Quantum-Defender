using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public static Menu Instance { get; private set; }

    [Header("Panels")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject turretMenuPanel;

    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI costUI;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ShowShopMenu(); // Make sure the shop is visible by default
    }

    //sets current money and displays the cost of selected tower
    void Update()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        costUI.text = "Cost: " + BuildManager.main.GetSelectedTower().cost.ToString();
    }
    public void ShowTurretMenu(TurretData turret)
    {
        // Swap panels
        shopPanel.SetActive(false);
        turretMenuPanel.SetActive(true);
    }

    public void ShowShopMenu()
    {
        shopPanel.SetActive(true);
        turretMenuPanel.SetActive(false);
    }

}
