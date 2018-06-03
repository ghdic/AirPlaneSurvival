using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public Transform leftPos;
    public Transform rightPos;
    private Object missle;

    private void Start()
    {
        missle = Resources.Load("Missle");
    }

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(missle, leftPos.position, leftPos.rotation);
            Instantiate(missle, rightPos.position, rightPos.rotation);
        }
	}
}
