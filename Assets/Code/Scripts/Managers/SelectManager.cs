using UnityEngine;
using System.Collections;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Menu uiMenu;
    public TurretData SelectedTurret {get; private set; }
    public bool IsMovingTurret {get; private set;}

    private void Awake()
    {
        if (Instance == null) Instance = this;
         else Destroy(gameObject);
    }

    public void SelectTurret(TurretData turret) 
    {
        if(IsMovingTurret) return;

        if(SelectedTurret != null)
        {
           SelectedTurret.rangescript.HideRange(); 
        }

        SelectedTurret = turret;
        uiMenu.ShowTurretMenu(turret);
        SelectedTurret.rangescript.ShowRange(SelectedTurret.turretscript.targetingRange);
    }

    public void Deselect()
    {
        if (IsMovingTurret)
        {
            CancelMove();
            return;
        }
        SelectedTurret.rangescript.HideRange();
        SelectedTurret = null;
        uiMenu.ShowShopMenu();
    }

    public void UpgradeSelectedTurret(int nodeIndex)
    {
        if (SelectedTurret == null) return;

        if (nodeIndex < 0 || nodeIndex >= SelectedTurret.upgradeTree.Length) return;

        TurretData.UpgradeNode targetNode = SelectedTurret.upgradeTree[nodeIndex];

        if (LevelManager.Instance != null && LevelManager.Instance.currency < targetNode.upgradeCost) return;

        LevelManager.Instance.currency -= targetNode.upgradeCost;

        Vector3 currentPos = SelectedTurret.transform.position;
        Quaternion currentRot = SelectedTurret.transform.rotation;
        int currentKills = SelectedTurret.kills;

        GameObject newTowerObject = Instantiate(targetNode.resultPrefab, currentPos, currentRot);
        TurretData newTurretData = newTowerObject.GetComponent<TurretData>();

        Destroy(SelectedTurret.gameObject);

        if (newTurretData != null)
        {
            newTurretData.kills = currentKills;
            SelectTurret(newTurretData);
        }
        else Deselect();

    }

    public void CancelMove()
    {
            IsMovingTurret = false;
            SelectedTurret.rangescript.HideRange();
            SelectedTurret = null;
            uiMenu.ShowShopMenu();
    }

    public void SellSelectedTurret()
    {
        if(SelectedTurret != null && !IsMovingTurret)
        {
            if(LevelManager.Instance != null)
            {
                LevelManager.Instance.currency += SelectedTurret.smValue;
            }
            
            GameObject turretToDestroy = SelectedTurret.gameObject;
            Deselect(); 
            Destroy(turretToDestroy);
        }
    }  

    public void StartMove()
{
        if (SelectedTurret == null) return;

        if (LevelManager.Instance != null && LevelManager.Instance.currency < SelectedTurret.smValue)
        {
            Debug.Log("Not enough money to move!");
            return;
        }

        StartCoroutine(MoveRoutine());
    }

    public void CompleteMove(Vector3 newPosition)
    {
        if (SelectedTurret != null && LevelManager.Instance != null)
        {
            LevelManager.Instance.currency -= SelectedTurret.smValue;
            SelectedTurret.gameObject.transform.position = newPosition;
            IsMovingTurret = false; 
        }
    }

    private IEnumerator MoveRoutine()
    {
        IsMovingTurret = true;
        
        uiMenu.ToggleMoveInstructions(true); 

        while (IsMovingTurret)
        {
            yield return null; 
        }
        uiMenu.ToggleMoveInstructions(false); 
        uiMenu.ShowShopMenu();
        SelectedTurret.rangescript.HideRange(); 
        SelectedTurret = null;  
    }
}
