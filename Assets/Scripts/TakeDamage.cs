using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            FindObjectOfType<Health>().TakeDamage(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            FindObjectOfType<GameManager>().RestartLevel();
        }
    }
}