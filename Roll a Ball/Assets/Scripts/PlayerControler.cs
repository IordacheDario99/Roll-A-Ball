using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    public LayerMask groundLayers;
    public SphereCollider col;

    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        col = GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movment = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movment * speed);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    
    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
            col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up")) {
            other.gameObject.SetActive (false);
            count ++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            other.gameObject.SetActive(false);
        }
    }


    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 22) {
            winText.text = "You win!";
        }
    }

}
