using Unity.VisualScripting;
using UnityEngine;

//Holds all the unique data for the towers

public class TowerData : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public TowerRange rangescript;

    //The Data (name, unit #, kills, cost, sell/move value, and range)
    public string towerFamily;
    public int towerCost = 100;
    public float range = 5f;
    public Plot currentPlot = null;
    public int smValue {get; private set;}
    [System.NonSerialized] public int kills;
    [System.NonSerialized] public string towerName;

    [Header("Upgrade Stuff")]
    public int currentUpgradeLevel = 0;
    
    [System.Serializable]
    public struct UpgradeNode
    {
        public string nodeName;
        [TextArea(2,3)] public string nodeDescription;
        public Sprite nodeIcon;
        public GameObject resultPrefab;
        public int upgradeCost;
    }

    [Header("Upgrade Path")]
    public UpgradeNode[] upgradeTree = new UpgradeNode[4];


    //Names tower and finds it's value
    void Awake()
    {
        smValue = Mathf.RoundToInt(towerCost * 0.7f);
    }
}
