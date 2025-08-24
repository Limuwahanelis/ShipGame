using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PauseSetter : MonoBehaviour
{
    [SerializeField] InputActionReference _playerPause;
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    private bool _hasSetPause;
    private static bool _isForcedPause = false;
    public void SetPauseNoTimeStop(bool value)
    {
        if (_isForcedPause) return;
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
        if (!_hasSetPause) return;
        _hasSetPause = value;
        PauseSettings.SetPause(value,false);
    }
    public void SwitchPause()
    {
        if (_isForcedPause) return;
        if (PauseSettings.IsGamePaused) OnPause?.Invoke();
        else OnResume?.Invoke();
        if (!_hasSetPause) return;
        _hasSetPause = !PauseSettings.IsGamePaused;
        PauseSettings.SetPause(!PauseSettings.IsGamePaused, !PauseSettings.IsGamePaused);
    }
    public void SetPause(bool value)
    {
        if (_isForcedPause) return;
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
        if(!_hasSetPause) return;
        _hasSetPause = value;
        PauseSettings.SetPause(value,value);
    }
}