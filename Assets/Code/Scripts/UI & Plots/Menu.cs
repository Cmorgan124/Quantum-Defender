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
    [SerializeField] TextMeshProUGUI sellUI;

    private void Start()
    {
        ShowShopMenu();
    }

    void Update()
    {
        currencyUI.text = "$" + LevelManager.Instance.currency.ToString();
        costUI.text = "Cost: $" + BuildManager.Instance.GetSelectedTower().cost.ToString();
        if(SelectManager.Instance.SelectedTurret != null)
        {
            turretkillsUI.text = "Kills: " + SelectManager.Instance.SelectedTurret.kills;
        }

    }
    public void ShowTurretMenu(TurretData turret)
    {
        shopPanel.SetActive(false);
        turretMenuPanel.SetActive(true);
        turretnameUI.text = turret.turretName;
        sellUI.text = "Sell: +$" + turret.sellValue;
    }

    public void ShowShopMenu()
    {
        shopPanel.SetActive(true);
        turretMenuPanel.SetActive(false);
    }

}
