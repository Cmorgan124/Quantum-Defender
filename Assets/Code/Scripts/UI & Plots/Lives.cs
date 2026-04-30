using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour
{
    public int lives = 50;
    public TextMeshProUGUI livesText;

    //displays current lives remaing, and decreases on enemy "escape"
    void Update()
    {
        livesText.text = "Lives remaining" + lives.ToString(); 
    }

}
