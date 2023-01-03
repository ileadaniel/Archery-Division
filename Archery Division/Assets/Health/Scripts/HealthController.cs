using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;

    private float currentHealth = 0;

    [SerializeField]
    private float respawnTime = 2;

    private float waitTime = 1;

    [SerializeField]
    private GameObject healthPanel = null;

    [SerializeField]
    private GameObject respawnPanel = null;

    [SerializeField]
    private Text enemiesKilledText = null;

    [SerializeField]
    private Text respawningText = null;

    [SerializeField]
    private Text healthText = null;

    [SerializeField]
    private Text actionText = null;

    [SerializeField]
    private Text arrowsInEnemyText = null;

    [SerializeField]
    private Text arrowsInGroundText = null;

    [SerializeField]
    private Text arrowsThrownText = null;

    [SerializeField]
    private Text percentageText = null;


    [SerializeField]
    private Text timeText = null;

    [SerializeField]
    private RectTransform healthBar = null;

    private float healthBarStartWidth = 0;

    private MeshRenderer meshRenderer = null;
    
    private bool isDead = false;

    private int nrDeaths = 0;

    private int nr_arrows = 0;

    public float time;

    private string timeTaken;

    public GameObject weaponC, groundC;

    public double averagesDoubles;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        currentHealth = maxHealth;
        healthBarStartWidth = healthBar.sizeDelta.x;
        respawnPanel.SetActive(false);
        UpdateUI();
    }

    void Update()
    {

        if (!isDead)
        {
            time += Time.deltaTime;
            var minutes = Mathf.Floor(time / 60);
            var seconds = time % 60;
            var fraction = (time * 100) % 100;
            timeTaken = string.Format("{0:00} : {1:00} : {2:00}", minutes, seconds, fraction);
            timeText.text = timeTaken;
        }
    }

    public void ApplyDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            nrDeaths++;

            StartCoroutine(RespawnAfterTime());

        }

        UpdateUI();
    }

    private IEnumerator RespawnAfterTime()
    {
        healthPanel.SetActive(false);
        respawnPanel.SetActive(true);
        respawningText.text = "Enemy died!";
        yield return new WaitForSeconds(waitTime);
        time = 0;
        timeText.text = string.Format("{0:00} : {1:00} : {2:00}", time, time, time);
        respawningText.text = "Respawning!";
        meshRenderer.enabled = false;
        enemiesKilledText.text = nrDeaths + "";

        RemoveClonedArrowsFromDeadEnemy();

        yield return new WaitForSeconds(respawnTime);

        ResetHealth();
        
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        meshRenderer.enabled = true;
        healthPanel.SetActive(true);
        respawnPanel.SetActive(false);
        UpdateUI();
        ArrowAction("Enemy died and respawned", false);
    }

    private void UpdateUI()
    {
        float percentOutOf = (currentHealth / maxHealth) * 100;
        float newWidth = (percentOutOf / 100) * healthBarStartWidth;

        healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
        healthText.text = currentHealth + "/" + maxHealth;

      
    }

    private void RemoveClonedArrowsFromDeadEnemy()
    {
        Debug.Log("Removing arrows that hit the Enemy which has died");
        var current = this.transform;
        while (current.parent) // Go up until obj does not have a parent
            current = current.parent;

        foreach (Transform child in current)
        {
            if (child.name != "Canvas") {
                //Debug.Log(child.name);
                Destroy(child.gameObject);

            }
        }
    }

    public void updatePercentage()
    {
        int arrows_thrown = Int32.Parse(arrowsThrownText.text);
        int arrows_in_ground = Int32.Parse(arrowsInGroundText.text);
      
        double hitAccuracy;
        if (nr_arrows != 0)
        {
            hitAccuracy = ((double)nr_arrows / (double)arrows_thrown) *100;
          
        }
        else
            hitAccuracy = 0;

        averagesDoubles = Math.Round(hitAccuracy, 1);
        percentageText.text = averagesDoubles + "%";

    }

    public void ArrowAction(string action, bool didHit)
    {
        actionText.text = action;
        if (didHit)
        {
            nr_arrows++;
            updatePercentage();
        }
        else
        {
            Debug.Log("Statistics for enemy " + nrDeaths + " :\n" 
                +"Arrows: " + arrowsThrownText.text + "\n"
                + "Target miss: " + arrowsInGroundText.text + "\n"
                + "Target hit: " + arrowsInEnemyText.text + "\n"
                + "Accuracy: " + averagesDoubles + "%\n"
                + "Time = "+timeTaken);
            nr_arrows = 0;
            arrowsInGroundText.text = 0 + "";
            arrowsThrownText.text = 0 + "";
            var weapon = weaponC.GetComponent<WeaponController>();
            weapon.reset_arrow_count();
            var ground = groundC.GetComponent<GroundCollision>();
            ground.reset_arrow_count();
        }
        arrowsInEnemyText.text = nr_arrows + "";
    }

}