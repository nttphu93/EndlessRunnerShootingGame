using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float timeToWaitForDodging = 0.2f;
    [SerializeField] float jumpPower = 5f;
    //Cached References
    Rigidbody myRigidbody;
    bool isButtonReady = true;
    bool isJumpReady = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //called in fixedUpdate because we work with physic
    void FixedUpdate()
    {
        Debug.Log(myRigidbody.velocity.y);
        AutoForward();
        if (isButtonReady)
        {
            StartCoroutine(TranslateLeftRight(Input.GetAxisRaw("Horizontal")));
        }
        if(Input.GetKeyDown(KeyCode.Space) && isJumpReady)
        {
            Jump();
        }
        if(myRigidbody.velocity.y == 0) { isJumpReady = true; }
    }
    IEnumerator TranslateLeftRight(float directon)
    {
        transform.Translate(directon,0,0);
        isButtonReady = false;
        yield return new WaitForSeconds(timeToWaitForDodging);
        isButtonReady = true;
    }
    void Jump()
    {
        isJumpReady = false;
        myRigidbody.velocity = Vector3.up * jumpPower * Time.deltaTime;
    }
    void AutoForward()
    {
        myRigidbody.MovePosition( transform.position + Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
