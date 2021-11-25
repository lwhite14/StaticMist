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
        //transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
        //transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothing * Time.deltaTime);
        //LerpTransform(transform, target, smoothing * Time.deltaTime);  
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

    void LerpTransform(Transform t1, Transform t2, float t)
    {
        t1.position = Vector3.Slerp(t1.position, t2.position, t);
        t1.rotation = Quaternion.Slerp(t1.rotation, t2.rotation, t);
        t1.localScale = Vector3.Slerp(t1.localScale, t2.localScale, t);
    }
}

