using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Menu uiMenu;
    private TurretData selectedTurret;

private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectTurret(TurretData turret) 
    {
        selectedTurret = turret;
        uiMenu.ShowTurretMenu(turret);
    }

    public void Deselect()
    {
        selectedTurret = null;
        uiMenu.ShowShopMenu();
    }
}
