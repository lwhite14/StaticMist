using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waypoint : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public int maxDistance = 8;

    Image img;
    Text meter;
    Animator anim;

    int distance;
    float minX;
    float maxX;
    float minY;
    float maxY;

    void Start()
    {
        img = GameObject.Find("Waypoint").GetComponent<Image>();
        meter = GameObject.Find("DistanceText").GetComponent<Text>();
        anim = GameObject.Find("Waypoint").GetComponent<Animator>();

        minX = -(img.GetPixelAdjustedRect().width / 2);
        maxX = Screen.width - minX;

        minY = -(img.GetPixelAdjustedRect().height / 2);
        maxY = Screen.height - minY;       
    }

    private void Update()
    {
        if ((target != null) && (img != null) && (meter != null) && (anim != null))
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

            if (Vector3.Dot((target.position - transform.position), transform.forward) <= 0)
            {
                if (pos.x < Screen.width / 2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }

            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            distance = (int)Vector3.Distance(target.position, transform.position);
            if (distance < maxDistance)
            {
                anim.SetBool("isAppeared", true);
                anim.SetBool("disappearQuick", false);
            }
            else
            {
                anim.SetBool("isAppeared", false);
            }

            img.transform.position = pos;
            meter.text = distance.ToString() + "m";
        }
        else
        {
            anim.SetBool("isAppeared", false);
            anim.SetBool("disappearQuick", true);
        }

    }
}
// Scripts keeps the waypoint image on a target transform. 
// (May not be used in the final version)