using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject reloadPanel = null;

    [SerializeField]
    private float reloadTime = 0;

    [SerializeField]
    private Arrow arrowPrefab = null;

    [SerializeField]
    private Text actionText = null;

    [SerializeField]
    private Transform spawnPoint = null;

    private Arrow currentArrow = null;

    private string enemyTag = "";

    private bool isReloading = false;

    private void Awake()
    {
        reloadPanel.SetActive(false);
    }

    public void SetEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public void Reload()
    {
        if (isReloading || currentArrow != null) return;
        isReloading = true;
        StartCoroutine(ReloadAfterTime());
    }

    private IEnumerator ReloadAfterTime()
    {
        reloadPanel.SetActive(true);
        actionText.text = "Bow loading with an arrow";
        yield return new WaitForSeconds(reloadTime);
        reloadPanel.SetActive(false);
        actionText.text = "Bow loaded";
        currentArrow = Instantiate(arrowPrefab, spawnPoint);
        currentArrow.transform.localPosition = Vector3.zero;
        currentArrow.transform.localRotation = Quaternion.Euler(Vector3.zero);
        currentArrow.SetEnemyTag(enemyTag);
        isReloading = false;
    }

    public void Fire(float firePower)
    {
        if (isReloading || currentArrow == null) return;
        var force = spawnPoint.TransformDirection(Vector3.forward * firePower);
        currentArrow.Fly(force);
        currentArrow = null;
        Reload();
    }

    public bool IsReady()
    {
        return (!isReloading && currentArrow != null);
    }

}
