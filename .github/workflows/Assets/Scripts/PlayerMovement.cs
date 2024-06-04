using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 0;
    float horInput;
    float vertInput;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horInput, 0, vertInput) * speed * Time.deltaTime;
        rb.velocity = movement;
    }
}
