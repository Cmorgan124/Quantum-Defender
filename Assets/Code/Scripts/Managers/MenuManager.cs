using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    private TurretData selectedTurret;

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

    public void SelectTurret(TurretData turret) 
    {
        selectedTurret = turret;
        Menu.Instance.ShowTurretMenu(turret);
    }

    public void Deselect()
    {
        selectedTurret = null;
        Menu.Instance.ShowShopMenu();
    }
}
