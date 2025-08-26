using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrewMember : MonoBehaviour
{
    public Action OnCrewReturned;
    [SerializeField] float _moveSpeed;
    private Vector2 _posToPlunder;
    private Vector2 _spawnPos;

    public void SetSpawnPos(Vector2 spawnPoint)
    {
        transform.position = _spawnPos = spawnPoint + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.25f, 0.25f));
    }
    public void SetPosToPlunder(Vector2 posToPlunder)
    {
        _posToPlunder = posToPlunder;
        StartCoroutine(GoToPos(_spawnPos, _posToPlunder));
    }
    public void Return()
    {
        StartCoroutine(GoToPos(_posToPlunder, _spawnPos,true));
    }
    IEnumerator GoToPos(Vector2 startpos,Vector2 targetPos,bool firereturn=false)
    {

        float totalTime = Vector2.Distance(startpos, targetPos) / _moveSpeed;
        float t = 0;
        while (t < 1) 
        {
            transform.position= Vector2.Lerp(startpos, targetPos, t);
            t+= Time.deltaTime/totalTime;
            yield return null;
        }
        if (firereturn) OnCrewReturned?.Invoke();
        transform.position = targetPos;
    }
}
