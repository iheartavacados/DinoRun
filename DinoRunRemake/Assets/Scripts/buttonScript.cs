using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{

    Vector3 startPosition;
    Vector3 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        endPosition = new Vector3(-0.75f, 2.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void disappearButton()
    {
        GameObject.Find("Button").SetActive(false);
    }
}
