using UnityEngine;
using System.Collections;

//A singleton that helps with indiviual tower selection

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Menu uiMenu;
    public TowerData SelectedTower {get; private set; }
    public bool IsMovingTower {get; private set;}

    private void Awake()
    {
        if (Instance == null) Instance = this;
         else Destroy(gameObject);
    }

    public void SelectTower(TowerData tower) 
    {
        if(IsMovingTower) return;

        if(SelectedTower != null)
        {
           SelectedTower.rangescript.HideRange(); 
        }

        SelectedTower = tower;
        uiMenu.ShowTowerMenu(tower);
        SelectedTower.rangescript.ShowRange(SelectedTower.range);
    }

    public void Deselect()
    {
        if (IsMovingTower)
        {
            CancelMove();
            return;
        }
        SelectedTower.rangescript.HideRange();
        SelectedTower = null;
        uiMenu.ShowShopMenu();
    }

    public void UpgradeSelectedTower(int nodeIndex)
    {
        if (SelectedTower == null) return;
        if (nodeIndex < 0 || nodeIndex >= SelectedTower.upgradeTree.Length) return;

        TowerData.UpgradeNode targetNode = SelectedTower.upgradeTree[nodeIndex];

        if (LevelManager.Instance != null && LevelManager.Instance.currency < targetNode.upgradeCost) return;

        LevelManager.Instance.currency -= targetNode.upgradeCost;

        Vector3 currentPos = SelectedTower.transform.position;
        Quaternion currentRot = SelectedTower.transform.rotation;
        int currentKills = SelectedTower.kills;
        string currentName = SelectedTower.towerName;

        GameObject newTowerObject = Instantiate(targetNode.resultPrefab, currentPos, currentRot);
        TowerData newTowerData = newTowerObject.GetComponent<TowerData>();

        Destroy(SelectedTower.gameObject);

        if (newTowerData != null)
        {
            newTowerData.towerName = currentName;
            newTowerData.kills = currentKills;
            SelectTower(newTowerData);
        }
        else Deselect();

    }

    public void CancelMove()
    {
            IsMovingTower = false;
            SelectedTower.rangescript.HideRange();
            SelectedTower = null;
            uiMenu.ShowShopMenu();
    }

    public void SellSelectedTower()
    {
        if(SelectedTower != null && !IsMovingTower)
        {
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.currency += SelectedTower.smValue;
            }
            
            GameObject towerToDestroy = SelectedTower.gameObject;
            Deselect(); 
            Destroy(towerToDestroy);
        }
    }  

    public void StartMove()
{
        if (SelectedTower == null) return;

        if (LevelManager.Instance != null && LevelManager.Instance.currency < SelectedTower.smValue)
        {
            Debug.Log("Not enough money to move!");
            return;
        }

        StartCoroutine(MoveRoutine());
    }

    public void CompleteMove(Vector3 newPosition, Plot newPlot)
    {
        if (SelectedTower != null && LevelManager.Instance != null)
        {
            Plot oldPlot = SelectedTower.currentPlot;
            if(oldPlot) oldPlot.placedTower = null;
            LevelManager.Instance.currency -= SelectedTower.smValue;
            SelectedTower.gameObject.transform.position = newPosition;
            newPlot.placedTower = SelectedTower.gameObject;
            SelectedTower.currentPlot = newPlot;
            IsMovingTower = false;
        }
    }

    private IEnumerator MoveRoutine()
    {
        IsMovingTower = true;
        
        uiMenu.ToggleMoveInstructions(true); 

        while (IsMovingTower)
        {
            yield return null; 
        }
        uiMenu.ToggleMoveInstructions(false); 
        uiMenu.ShowShopMenu();
        SelectedTower.rangescript.HideRange(); 
        SelectedTower = null;  
    }
}
