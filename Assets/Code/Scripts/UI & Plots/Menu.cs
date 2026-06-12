using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.FullSerializer;

//The big boy. Holds all the ui logic. From main shop, to all the selected tower logic. 

public class Menu : MonoBehaviour
{
    [System.Serializable]
    //Upgrade button struct. Holds the main button, info button, and the two sides of the upgrade card (front image and back description).
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
    [SerializeField] public GameObject towerMenuPanel;
    [SerializeField] private GameObject moveinstructionsPanel;
    [SerializeField] private GameObject smGroup;

    [SerializeField] public UpgradeButtonUI[] uiNodes = new UpgradeButtonUI[4];

    [Header("TextMeshes")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI costUI;
    [SerializeField] TextMeshProUGUI towernameUI;
    [SerializeField] TextMeshProUGUI towerkillsUI;
    [SerializeField] TextMeshProUGUI sellUI;
    [SerializeField] TextMeshProUGUI moveUI;

    //Ensures that the shop menu is displayed at the start
    private void Start()
    {
        ShowShopMenu();
        moveinstructionsPanel.SetActive(false);
    }

    //Displays the correct amount of current money and tower kills, and the cost of a selected tower in the shop.
    void Update()
    {
        currencyUI.text = "$" + LevelManager.Instance.currency.ToString();
        costUI.text = "Cost: $" + BuildManager.Instance.GetSelectedTower().cost.ToString();
        if(SelectManager.Instance.SelectedTower != null)
        {
            towerkillsUI.text = "Kills: " + SelectManager.Instance.SelectedTower.kills;
        }
    }

    //Swaps from the shop menu to the selected tower menu
    public void ShowTowerMenu(TowerData tower)
    {
        shopPanel.SetActive(false);
        towerMenuPanel.SetActive(true);
        
        if (smGroup != null) smGroup.SetActive(true);
        if (moveinstructionsPanel != null) moveinstructionsPanel.SetActive(false);

        towernameUI.text = tower.towerName;
        towerkillsUI.text = "Kills: " + tower.kills;
        sellUI.text = "Sell: +$" + tower.smValue;
        if (moveUI != null) moveUI.text = "Move: $" + tower.smValue;

        int currentLevel = tower.currentUpgradeLevel;

        for(int i = 0; i < uiNodes.Length; i++)
        {
            uiNodes[i].showingDescription = false;

            uiNodes[i].titleText.text = tower.upgradeTree[i].nodeName;
            uiNodes[i].costText.text = "$" + tower.upgradeTree[i].upgradeCost;
            uiNodes[i].descriptionText.text = tower.upgradeTree[i].nodeDescription;
            if(uiNodes[i].iconDisplay != null) uiNodes[i].iconDisplay.sprite = tower.upgradeTree[i].nodeIcon;

            if(i == currentLevel)
            {
                uiNodes[i].upgradeButton.interactable = true;
            }
            else
            {
                uiNodes[i].upgradeButton.interactable = false;
            }
            RefreshCard(i);
        }
    }

    //Swaps to the description of the upgrade
    public void ToggleInfo(int nodeIndex)
    {
        if(nodeIndex < 0 || nodeIndex >= uiNodes.Length) return;
        uiNodes[nodeIndex].showingDescription = !uiNodes[nodeIndex].showingDescription;
        RefreshCard(nodeIndex);
    }

    //Shows the correct side of the upgrade button
    private void RefreshCard(int index)
    {
            if (uiNodes[index].showingDescription)
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

    //Swaps from tower menu to shop menu
    public void ShowShopMenu()
    {
        shopPanel.SetActive(true);
        towerMenuPanel.SetActive(false);
    }

    //Swaps between move instructions and normal tower menu
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
