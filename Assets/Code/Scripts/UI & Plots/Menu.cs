using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject turretMenuPanel;

    [Header("TextMeshes")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI costUI;
    [SerializeField] TextMeshProUGUI turretnameUI;
    [SerializeField] TextMeshProUGUI turretkillsUI;

    private void Start()
    {
        ShowShopMenu(); // Make sure the shop is visible by default
    }

    //sets current money and displays the cost of selected tower
    void Update()
    {
        currencyUI.text = "$" + LevelManager.Instance.currency.ToString();
        costUI.text = "Cost: $" + BuildManager.Instance.GetSelectedTower().cost.ToString();
    }
    public void ShowTurretMenu(TurretData turret)
    {
        shopPanel.SetActive(false);
        turretMenuPanel.SetActive(true);
        turretnameUI.text = turret.turretName;
        turretkillsUI.text = "Kills: " + turret.kills.ToString();
    }

    public void ShowShopMenu()
    {
        shopPanel.SetActive(true);
        turretMenuPanel.SetActive(false);
    }

}
