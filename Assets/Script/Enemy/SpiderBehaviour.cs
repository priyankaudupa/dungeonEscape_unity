using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{
    private Spider _spider;
    private void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
    }
    public void Fire()
    {
        //Debug.Log("Spider should fire");
        _spider.Attack();
    }
}
