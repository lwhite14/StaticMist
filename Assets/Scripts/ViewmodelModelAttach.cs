using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewmodelModelAttach : MonoBehaviour
{

    public Transform targetPositional;
    public Transform targetRotational;
    public float smoothing = 5.0f;
    bool isAttaching = true;

    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, targetPositional.position, smoothing * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotational.rotation, smoothing * Time.deltaTime);
    }

    public void SetIsAttaching(bool newAttaching) 
    {
        isAttaching = newAttaching;
    }

    public void SetTargets(Transform newPosTarget, Transform newRotTarget) 
    {
        targetPositional = newPosTarget;
        targetRotational = newRotTarget;
    }
}

