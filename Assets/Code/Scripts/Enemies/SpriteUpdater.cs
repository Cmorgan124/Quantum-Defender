using UnityEngine;

[System.Serializable]
public class SpriteSet
{
    public Sprite up, down, side;
}

public class SpriteUpdater : MonoBehaviour
{
    [Header("SpriteCollections")]
    public SpriteSet noRing;
    public SpriteSet oneRing;
    public SpriteSet twoRing;
    public SpriteSet threeRing;

    private SpriteSet currentSet;

    [Header("References")]
    public SpriteRenderer spriteRenderer;

    private EnemyMovement movementScript;
    private Health healthScript;

    //gets other scripts
    private void Start()
    {
        movementScript = GetComponent<EnemyMovement>();
        healthScript = GetComponent<Health>();
    }

    //gets current sprite set, direction, and updates based off of those
    private void Update()
    {
        SpriteSet currentSet = GetCurrentSet();

        Vector2 dir = movementScript.currentDirection;

        UpdateVisuals(dir, currentSet);
    }

    //getter for health, and setter for sprite set
    private SpriteSet GetCurrentSet()
    {
        int hp = healthScript.hitPoints;
        if (gameObject.name.Contains("Atom"))
        {
            if (hp == 6) return threeRing;
            if (hp == 5) return twoRing;
            if (hp >= 4) return oneRing;  
        }
        if (gameObject.name.Contains("Tank"))
        {
            if (hp == 11) return threeRing;
            if (hp == 10) return twoRing;
            if (hp >= 9) return oneRing;            
        }
        return noRing; 
    }

    //chooses which sprite based off of health and direction enemy is facing
    private void UpdateVisuals(Vector2 direction, SpriteSet set)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Horizontal movement
            spriteRenderer.sprite = set.side;
            spriteRenderer.flipX = direction.x < 0; // Flip if moving Left
        }
        else
        {
            // Vertical movement
            spriteRenderer.sprite = (direction.y > 0) ? set.up : set.down;
            spriteRenderer.flipX = false; // Reset flip for up/down
        }
    }
}
