using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Panels/Groups")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject turretMenuPanel;
    [SerializeField] private GameObject moveinstructionsPanel;
    [SerializeField] private GameObject smGroup;

    [Header("TextMeshes")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI costUI;
    [SerializeField] TextMeshProUGUI turretnameUI;
    [SerializeField] TextMeshProUGUI turretkillsUI;
    [SerializeField] TextMeshProUGUI sellUI;
    [SerializeField] TextMeshProUGUI moveUI;

    private void Start()
    {
        ShowShopMenu();
        moveinstructionsPanel.SetActive(false);
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
        
        if (smGroup != null) smGroup.SetActive(true);
        if (moveinstructionsPanel != null) moveinstructionsPanel.SetActive(false);

        turretnameUI.text = turret.turretName;
        turretkillsUI.text = "Kills: " + turret.kills;
        
        sellUI.text = "Sell: +$" + turret.smValue;
        if (moveUI != null) moveUI.text = "Move: $" + turret.smValue;
    }

    public void ShowShopMenu()
    {
        shopPanel.SetActive(true);
        turretMenuPanel.SetActive(false);
    }

    public void ToggleMoveInstructions(bool show)
    {
        if (show)
        {
            if (smGroup != null) smGroup.SetActive(false);
            if (moveinstructionsPanel != null) moveinstructionsPanel.SetActive(true);
        }
        else
        {
            if (smGroup != null) smGroup.SetActive(true);
            if (moveinstructionsPanel != null) moveinstructionsPanel.SetActive(false);
        }
    }
}
