/* Name: Nguyen Tran Gia Khuong
 Member Nguyen Tran Gia Khuong - ITITDK21060
 Purpose: A Candy Crush clone
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    public int hitPoints;
    private SpriteRenderer sprite;
    private GoalManager goalManager;
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        MakeLightter();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        goalManager = FindObjectOfType<GoalManager>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
        {
            if (goalManager != null) 
            {
                goalManager.CompareGoal(this.gameObject.tag);
                goalManager.UpdateGoals();
            }
            Destroy(this.gameObject);
        }
    }

    public void MakeLightter()
    {
        // take the current color
        Color color = sprite.color;
        // Get the current color's alpha value and cut it in half
        float newAlpha = color.a * .5f; 
        sprite.color = new Color(color.r, color.g, color.b, newAlpha);
    }
 
}
