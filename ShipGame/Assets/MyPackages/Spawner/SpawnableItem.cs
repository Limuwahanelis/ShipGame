using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnableItem : MonoBehaviour
{
    private IObjectPool<SpawnableItem> _pool;
    private Coroutine _cor;

    private IEnumerator ReturnToPoolCor(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnToPool();
        _cor = null;
    }

    public void SetPool(ObjectPool<SpawnableItem> pool) => _pool = pool;
    public void ReturnToPool() => _pool.Release(this);
    public void ReturnToPool(float timeDelay)
    {
        if (_cor != null) return;
        _cor=StartCoroutine(ReturnToPoolCor(timeDelay));
    }
}
