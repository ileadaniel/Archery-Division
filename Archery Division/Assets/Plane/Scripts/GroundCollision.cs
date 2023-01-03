using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundCollision : MonoBehaviour
{
    [SerializeField]
    private Text actionText = null;

    [SerializeField]
    private Text arrowsInGroundText = null;

    private int nr_arrows = 0;

    public GameObject healthC;

    public void ArrowAction(string action)
    {
        actionText.text = action;
        
            nr_arrows++;
            var health = healthC.GetComponent<HealthController>();
            health.updatePercentage();
        
        arrowsInGroundText.text = nr_arrows + "";
    }

    public void reset_arrow_count()
    {
        nr_arrows = 0;
    }
}