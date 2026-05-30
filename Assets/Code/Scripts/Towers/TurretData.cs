using UnityEngine;

public class TurretData : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Turret turretscript;
    [SerializeField] public TurretRange rangescript;
    [Header("Turret Data")]
    //Filler
    public string turretName;
    public static int basicCount = 0;
    public int kills = 0;
    public int turretCost = 100;
    public int smValue {get; private set;}
    void Start()
    {
        basicCount++ ; 

        turretName = "Basic Turret " + basicCount.ToString();
        smValue = Mathf.RoundToInt(turretCost * 0.7f);
    }

    void Update()
    {
        kills = turretscript.Kills;
    }
}
