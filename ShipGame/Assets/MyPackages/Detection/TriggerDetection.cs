using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    List<ITriggerDetectable> _detectables= new List<ITriggerDetectable>();
    private Rigidbody _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    Logger.Log(other.gameObject.name);
    //}
    private void OnTriggerEnter(Collider other)
    {
        Logger.Log("enter "+other.gameObject.name);
        _rb = other.attachedRigidbody;
        if (_rb != null)
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        Logger.Log("left " + other.gameObject.name);
    }
}
