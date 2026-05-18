using UnityEngine;

public class TurretData : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Turret turretscript;

    [Header("Turret Data")]
    //Filler
    public string turretName;
    public static int basicCount = 0;
    public int kills = 0;
    void Start()
    {
        basicCount++ ; 
        turretName = "Basic Turret " + basicCount.ToString();
    }

    void Update()
    {
        kills = turretscript.Kills;
    }

}
