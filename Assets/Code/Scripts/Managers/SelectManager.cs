using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Menu uiMenu;
    public TurretData SelectedTurret {get; private set; }

private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectTurret(TurretData turret) 
    {
        SelectedTurret = turret;
        uiMenu.ShowTurretMenu(turret);
    }

    public void Deselect()
    {
        SelectedTurret = null;
        uiMenu.ShowShopMenu();
    }

    public void SellSelectedTurret()
    {
        LevelManager.Instance.currency += SelectedTurret.sellValue;
        GameObject turretToDestroy = SelectedTurret.gameObject;
        Deselect(); 
        Destroy(turretToDestroy);
    }   
}
