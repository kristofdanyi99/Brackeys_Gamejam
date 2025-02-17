using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien_Ship_Script : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Vector3 scaleChange = new Vector3(0.01f, 0.01f, 0.01f);

    private void Update()
    {
        transform.localScale += scaleChange * Time.deltaTime;
    }
}
