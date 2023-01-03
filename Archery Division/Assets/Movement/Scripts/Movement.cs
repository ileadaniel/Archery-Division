using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Text actionText = null;

    [SerializeField]
    private float movementSpeed = 10;

    private new Rigidbody rigidbody = null;

    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(h, 0, v);
        velocity = transform.TransformDirection(velocity);
        velocity *= movementSpeed;
        velocity -= rigidbody.velocity;

        rigidbody.AddForce(velocity);

        if (h == 1)
            actionText.text = "Player moving right";
        else if (h == -1)
            actionText.text = "Player moving left";

        if (v == 1)
            actionText.text = "Player moving forward";
        else if (v == -1)
            actionText.text = "Player moving backward";

    }

}