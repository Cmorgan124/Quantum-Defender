using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main; //other script access

    [Header("Refrences")]
    [SerializeField] public Tower[] towers;

    private int selectedTower = 0;
    private void Awake()
    {
        main = this; //main LevelManager instance
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
