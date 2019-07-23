using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float timeToWaitForDodging = 0.2f;
    //Cached References
    Rigidbody myRigidbody;
    bool isButtonReady = true;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isButtonReady)
        {
            StartCoroutine(TranslateLeftRight(Input.GetAxisRaw("Horizontal")));
        }
        
    }
    //called in fixedUpdate because we work with physic
    void FixedUpdate()
    {
        AutoForward();
    }
    IEnumerator TranslateLeftRight(float directon)
    {
        transform.Translate(directon,0,0);
        isButtonReady = false;
        yield return new WaitForSeconds(timeToWaitForDodging);
        isButtonReady = true;
    }
    void AutoForward()
    {
        myRigidbody.MovePosition( transform.position + Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
