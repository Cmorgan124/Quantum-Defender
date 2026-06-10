using UnityEngine;
using TMPro;

//Puts a the unit # on top of the tower to help the player find a specific tower better.

public class TowerNumber : MonoBehaviour
{
    [Header("Refrences")]  
    [SerializeField] private TextMeshPro countText;
    [SerializeField] public TowerData datascript;
    private void Start()
    {
        countText.text = TowerData.basicCount.ToString();    
    }


}
