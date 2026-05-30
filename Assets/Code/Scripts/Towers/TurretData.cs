using Unity.VisualScripting;
using UnityEngine;

public class TurretData : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Turret turretscript;
    [SerializeField] public TurretRange rangescript;

    [Header("Turret Data")]
    public string turretName;
    public static int basicCount = 0;
    public int kills = 0;
    public int turretCost = 100;
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


    void Start()
    {
        if(turretCost == 100)
        {
          basicCount++;   
        }

        turretName = "Basic Turret " + basicCount.ToString();
        smValue = Mathf.RoundToInt(turretCost * 0.7f);
    }

    void Update()
    {
        kills = turretscript.Kills;
    }
}
