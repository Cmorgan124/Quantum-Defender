using UnityEngine;
using System.Collections.Generic;

//A singleton that holds all of the prefabs for the shop

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    Dictionary<string, int> towerCounts = new Dictionary<string, int>(){};

    [Header("Refrences")]
   
    [SerializeField] private Menu menu;
    [SerializeField] public Tower[] towers;

    private int selectedTower = 0;
    private GameObject towerObj;

    //Singleton logic
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

    //Places towers for initial buy, as well as for move option
    public void PlaceTower(Plot plot)
    {
        if (SelectManager.Instance != null && SelectManager.Instance.IsMovingTower)
        {
            SelectManager.Instance.CompleteMove(plot.transform.position, plot);
            return; 
        }

        if (plot.placedTower || menu.towerMenuPanel.activeSelf)
        {
            return;
        }

        Tower towerToBuild = GetSelectedTower();

        if(towerToBuild.cost > LevelManager.Instance.currency)
        {
            Debug.Log("Can't afford");
            return;
        }

        LevelManager.Instance.SpendCurrency(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, plot.transform.position, Quaternion.identity);
        plot.placedTower = towerObj;
        TowerData towerData = towerObj.GetComponent<TowerData>();
        towerData.currentPlot = plot;
        string familyKey = towerData.towerFamily;
        if (!towerCounts.ContainsKey(familyKey))
        {
            towerCounts.Add(familyKey, 0);
        }
        towerCounts[familyKey]++;
        int towerEdition = towerCounts[familyKey];
        towerData.towerName = familyKey + " #" + towerEdition;
    }
}
