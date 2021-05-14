using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float bombSize = 1;
    public int fire = 2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                    transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f);
            }
            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                    transform.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f);

            }
    }
}