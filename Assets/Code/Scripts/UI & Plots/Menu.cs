using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI costUI;

    //sets current money and displays the cost of selected tower
    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        costUI.text = "Cost: " + BuildManager.main.GetSelectedTower().cost.ToString();
    }

}
