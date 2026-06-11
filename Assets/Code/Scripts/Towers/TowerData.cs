using Unity.VisualScripting;
using UnityEngine;

//Holds all the unique data for the towers

public class TowerData : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Turret turretscript;
    [SerializeField] public TowerRange rangescript;

    //The Data (name, unit #, kills, cost, sell/move value)
    [System.NonSerialized] public string towerName;
    [System.NonSerialized] public static int basicCount = 0;
    [System.NonSerialized] public int kills = 0;
    public int towerCost = 100;
    public int smValue {get; private set;}

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
        towerName = gameObject.name;
        smValue = Mathf.RoundToInt(towerCost * 0.7f);
    }

    //Connecter for kill stat ui
    void Update()
    {
        kills = turretscript.Kills;
    }
}
