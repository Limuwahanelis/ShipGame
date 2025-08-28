using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetection : MonoBehaviour
{
    public UnityEvent OnColliderDetected;
    public UnityEvent<Collider2D> OnColliderDetectedCol;
    public UnityEvent OnColliderLeft;
    public UnityEvent<Collider2D> OnColliderLeftCol;
    [SerializeField] Collider2D _colliderToDetect;
    [SerializeField] bool _checkForSpecificCollider;
    [SerializeField] bool _disableDetectedGameObjectOnDetection;

    List<ITriggerDetectable> _detectables= new List<ITriggerDetectable>();
    private Rigidbody _rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_checkForSpecificCollider)
        {
            if (other == _colliderToDetect)
            {
                if (_disableDetectedGameObjectOnDetection) other.gameObject.SetActive(false);
                OnColliderDetected?.Invoke();
                OnColliderDetectedCol?.Invoke(other);
            }
        }
        else
        {
            OnColliderDetected?.Invoke();
            OnColliderDetectedCol?.Invoke(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_checkForSpecificCollider)
        {
            if (other == _colliderToDetect)
            {
                if (_disableDetectedGameObjectOnDetection) other.gameObject.SetActive(false);
                OnColliderLeft?.Invoke();
                OnColliderLeftCol?.Invoke(other);
            }
        }
        else
        {
            OnColliderLeft?.Invoke();
            OnColliderLeftCol?.Invoke(other);
        }
    }
}
