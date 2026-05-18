using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [Header("Refrences")]
    [SerializeField] public Tower[] towers;

    private int selectedTower = 0;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //getter for selected tower
    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    //setter for selected tower
    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }
}
