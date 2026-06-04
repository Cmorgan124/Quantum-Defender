using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.FullSerializer;

public class Menu : MonoBehaviour
{
    [System.Serializable]
    public struct UpgradeButtonUI
    {
        public Button upgradeButton;
        public Button infoButton;
        
        [Header("Front Card")]
        public GameObject frontviewContainer;
        public Image iconDisplay;
        public TextMeshProUGUI titleText;
        public TextMeshProUGUI costText;

        [Header("Back Card")]
        public GameObject backviewContainer;
        public TextMeshProUGUI descriptionText;

        [HideInInspector] public bool showingDescription;
    }

    [Header("Panels/Groups")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject turretMenuPanel;
    [SerializeField] private GameObject moveinstructionsPanel;
    [SerializeField] private GameObject smGroup;

    [SerializeField] private UpgradeButtonUI[] uiNodes = new UpgradeButtonUI[4];

    [Header("TextMeshes")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI costUI;
    [SerializeField] TextMeshProUGUI turretnameUI;
    [SerializeField] TextMeshProUGUI turretkillsUI;
    [SerializeField] TextMeshProUGUI sellUI;
    [SerializeField] TextMeshProUGUI moveUI;

    private bool showingDescriptions = false;

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

        int currentLevel = turret.currentUpgradeLevel;

        for(int i = 0; i < uiNodes.Length; i++)
        {
            uiNodes[i].showingDescription = false;

            uiNodes[i].titleText.text = turret.upgradeTree[i].nodeName;
            uiNodes[i].costText.text = "$" + turret.upgradeTree[i].upgradeCost;
            uiNodes[i].descriptionText.text = turret.upgradeTree[i].nodeDescription;
            if(uiNodes[i].iconDisplay != null) uiNodes[i].iconDisplay.sprite = turret.upgradeTree[i].nodeIcon;

            if(i == currentLevel)
            {
                uiNodes[i].infoButton.interactable = true;
            }
            else
            {
                uiNodes[i].infoButton.interactable = false;
            }

            RefreshCard(i);
        }
    }

    public void ToggleInfoFlipState(int nodeIndex)
    {
        if(nodeIndex < 0 || nodeIndex >= uiNodes.Length) return;
        uiNodes[nodeIndex].showingDescription = !uiNodes[nodeIndex].showingDescription;
        RefreshCard(nodeIndex);
    }

    private void RefreshCard(int index)
    {
            if (showingDescriptions)
            {
                uiNodes[index].frontviewContainer.SetActive(false);
                uiNodes[index].backviewContainer.SetActive(true);
            }
            else
            {
                uiNodes[index].frontviewContainer.SetActive(true);
                uiNodes[index].backviewContainer.SetActive(false); 
            }
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
