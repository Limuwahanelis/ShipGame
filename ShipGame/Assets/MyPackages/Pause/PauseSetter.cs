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
    private static PauseSetter _setterResponsibleForPause;
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

        if (PauseSettings.IsGamePaused)
        {
            if (_hasSetPause) OnResume?.Invoke();
            else OnPause?.Invoke();
            if (_setterResponsibleForPause==this)
            {
                PauseSettings.SetPause(false);
                _setterResponsibleForPause = null;
                _hasSetPause = false;
                
            }
            else
            {
                _hasSetPause = true;
            }
        }
        else
        {
             OnPause?.Invoke();
            _setterResponsibleForPause = this;
            _hasSetPause = true;
            PauseSettings.SetPause(true, true);
        }
    }
    public static void ForceUnpause()
    {
        _setterResponsibleForPause = null;
        PauseSettings.SetPause(false);
    }
    public void SetPause(bool value)
    {
        //if (_isForcedPause) return;
        if (value) OnPause?.Invoke();
        else OnResume?.Invoke();
        if(PauseSettings.IsGamePaused)
        {
            // chech if this setter set pause - so unpause
            if (_setterResponsibleForPause==this)
            {
                PauseSettings.SetPause(value, value);
                _setterResponsibleForPause = null;
            }
        }
        else
        {
            PauseSettings.SetPause(value, value);
            _setterResponsibleForPause = this;
        }


    }
}