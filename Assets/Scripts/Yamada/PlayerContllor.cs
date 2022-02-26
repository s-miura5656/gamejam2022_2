using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContllor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(-0.01f, 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0.01f, 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.01f);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(0.0f, 0.0f, -0.01f);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(-0.02f, 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0.02f, 0.0f, 0.0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(0.0f, 0.0f, 0.02f);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(0.0f, 0.0f, -0.02f);
            }
        }
    }
}
