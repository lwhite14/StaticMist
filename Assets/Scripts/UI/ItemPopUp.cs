using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopUp : MonoBehaviour
{
    public float appearDistance = 6.0f;
    Transform playerTransform;
    Camera mainCamera;
    Animator anim;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        anim = GetComponent<Animator>();

        GetComponent<Canvas>().worldCamera = mainCamera;
    }

    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        float distanceBetween = Vector3.Distance(playerTransform.position, gameObject.transform.position);
        if (distanceBetween <= appearDistance)
        {
            anim.SetBool("isAppear", true);
        }
        else if (anim.GetBool("isAppear") && distanceBetween > appearDistance) 
        {
            anim.SetBool("isAppear", false);
        }
    }
}
