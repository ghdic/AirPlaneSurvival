using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransform : MonoBehaviour {

    private Transform[] tr;

    private void Start()
    {
        tr = GetComponentsInChildren<Transform>();
        
        //  10초마다 위치 바꿈
        StartCoroutine(ChangeTransform());
    }

    //위치 바꿈
    IEnumerator ChangeTransform()
    {
        foreach(Transform tf in tr)
        {
            if (tf == this.transform)
            {
                continue;
            }
            else tf.localPosition = new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f), Random.Range(-50.0f, 500.0f));
        }
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(ChangeTransform());
    }
}
