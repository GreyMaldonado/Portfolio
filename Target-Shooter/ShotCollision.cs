using UnityEngine;
using System.Collections;

public class ShotCollision : MonoBehaviour
{
    public int scoreIncrement = 1;

    private int penSkill;
    private Vector3 startPos;
    private float distance;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        penSkill = PlayerPrefs.GetInt("penSkill");
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, startPos);
        if(distance > 50)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            if(penSkill > 0)
            {
                penSkill--;
            }
            else
            {
                Destroy(gameObject);
            }
            //Debug.Log("Target");
            ScoreManager.instance.AddScore(scoreIncrement);
        }
        else if(other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Debug.Log("Ground");
        }
        
    }

}
