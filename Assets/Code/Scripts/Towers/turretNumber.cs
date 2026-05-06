using UnityEngine;
using TMPro;

public class turretNumber : MonoBehaviour
{
    [Header("Refrences")]  
    [SerializeField] private TextMeshPro countText;

    [Header("Attributes")]
    public static int BasicCount = 0;
    private void Start()
    {
        BasicCount++ ; 
        countText.text =  BasicCount.ToString();    
    }


}
