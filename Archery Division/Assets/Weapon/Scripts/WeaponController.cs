using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Text firePowerText = null;

    [SerializeField]
    private Weapon weapon = null;

    [SerializeField]
    private Text actionText = null;

    [SerializeField]
    private Text arrowsThrownText = null;

    [SerializeField]
    private string enemyTag = "";

    [SerializeField]
    private float maxFirePower = 0;

    [SerializeField]
    private float firePowerSpeed = 0;

    private float firePower = 0;

    [SerializeField]
    private float rotateSpeed = 0;

    [SerializeField]
    private float minRotation = 0;

    [SerializeField]
    private float maxRotation = 0;

    private float mouseY = 0;

    private int arrows_thrown = 0;

    private bool fire = false;

    void Start()
    {
        weapon.SetEnemyTag(enemyTag);
        weapon.Reload();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseY -= Input.GetAxis("Mouse Y") * rotateSpeed;
        mouseY = Mathf.Clamp(mouseY, minRotation, maxRotation);
        weapon.transform.localRotation = Quaternion.Euler(mouseY, weapon.transform.localEulerAngles.y, weapon.transform.localEulerAngles.z);

        if (Input.GetMouseButtonDown(0))
        {
            fire = true;
            actionText.text = "Bow charging";
        }

        if (fire && firePower < maxFirePower)
        {
            firePower += Time.deltaTime * firePowerSpeed;
            actionText.text = "Bow charging";
        }

        if (fire && firePower >= maxFirePower)
        {
            actionText.text = "Bow charged to maximum";
        }

        if (fire && Input.GetMouseButtonUp(0))
        {
            weapon.Fire(firePower);
            firePower = 0;
            fire = false;
            actionText.text = "Launcing arrow";
            arrows_thrown++;
            arrowsThrownText.text = arrows_thrown + "";
        }

        if (fire)
        {
            firePowerText.text = firePower.ToString();
        }
    }

    public void reset_arrow_count()
    {
        arrows_thrown = 0;
    }

}