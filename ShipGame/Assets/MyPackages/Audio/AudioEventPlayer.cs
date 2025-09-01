using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventPlayer : MonoBehaviour
{
    [SerializeField] ItemSpawner _audioSourceSpawner;
    public void PlayeAudioEvent(AudioEvent audioEvent)
    {
        SpawnableItem source = _audioSourceSpawner.GetItem();
        audioEvent.Play(source.GetComponent<AudioSource>());
        source.ReturnToPool(source.GetComponent<AudioSource>().clip.length * source.GetComponent<AudioSource>().pitch);
    }
    private void Reset()
    {
        _audioSourceSpawner = GameObject.FindGameObjectWithTag("Audio source spawner").GetComponent<ItemSpawner>();
    }
}
