using UnityEngine;
using TMPro;

public class turretNumber : MonoBehaviour
{
    [Header("Refrences")]  
    [SerializeField] private TextMeshPro countText;
    [SerializeField] public TurretData datascript;
    private void Start()
    {
        countText.text = TurretData.basicCount.ToString();    
    }


}
