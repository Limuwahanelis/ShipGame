using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGunsComponent : GunsComponent
{
    [System.Serializable]
    public class ShipGunsListWrapper
    {
        public GameObject gameObject;
        public List<ShipGun> guns= new List<ShipGun>();
    }
    [SerializeField] List<ShipGunsListWrapper> _gunsSeprated;
    [SerializeField] LevelableUpgradeSO _gunsUpgrade;
    [SerializeField] LevelableUpgradeSO _reloadUpgrade;
    [SerializeField] protected float _fireCooldown = 1f;
    [SerializeField] Image _cooldownImage;
    private int _ccurrentGunSetindex = 0;
    private int _numberOfGunsWhichCanBeFired;
    private float _timer;
    float _cooldownModifier=1f;
    private bool _canFire=true;
    private int _crewOnShip;
    private void Start()
    {
        _audioEventPlayer = FindFirstObjectByType<AudioEventPlayer>();
        foreach (ShipGunsListWrapper shipGunsList in _gunsSeprated)
        {
            shipGunsList.gameObject.SetActive(false);
        }
        _gunsSeprated[_ccurrentGunSetindex].gameObject.SetActive(true);

    }
    private void Update()
    {
        if (PauseSettings.IsGamePaused) return;
        Vector3 tmp = transform.position;
        tmp.z = -1;
        transform.up = Camera.main.ScreenToWorldPoint(HelperClass.MousePos) - tmp;
    }
    public void SetCrewOnShip(int num)
    {
        
        _numberOfGunsWhichCanBeFired = num;
        if (num < _gunsSeprated[_ccurrentGunSetindex].guns.Count)
        {
           
            for (int i = 0; i < _gunsSeprated[_ccurrentGunSetindex].guns.Count; i++)
            {
                if(i< _numberOfGunsWhichCanBeFired) _gunsSeprated[_ccurrentGunSetindex].guns[i].SetRangeIndicator(true);
                else _gunsSeprated[_ccurrentGunSetindex].guns[i].SetRangeIndicator(false);
            }
        }
        else
        {
            for (int i = 0; i < _gunsSeprated[_ccurrentGunSetindex].guns.Count; i++)
            {
                 _gunsSeprated[_ccurrentGunSetindex].guns[i].SetRangeIndicator(true);
            }
            _cooldownModifier = 1 + (num - _gunsSeprated[_ccurrentGunSetindex].guns.Count) / 10f;
        }
    }
    public void ActivateGunSet(LevelableUpgradeSO upgrade,int upgradeLevel)
    {
        if (upgrade != _gunsUpgrade) return;

        _ccurrentGunSetindex=upgradeLevel;

        foreach (ShipGunsListWrapper shipGunsList in _gunsSeprated)
        {
            shipGunsList.gameObject.SetActive(false);
        }
        _gunsSeprated[_ccurrentGunSetindex].gameObject.SetActive(true);
        for (int i = 0; i < _gunsSeprated[_ccurrentGunSetindex].guns.Count; i++)
        {
            if (i < _numberOfGunsWhichCanBeFired) _gunsSeprated[_ccurrentGunSetindex].guns[i].SetRangeIndicator(true);
            else _gunsSeprated[_ccurrentGunSetindex].guns[i].SetRangeIndicator(false);
        }
        if (_numberOfGunsWhichCanBeFired <= _gunsSeprated[_ccurrentGunSetindex].guns.Count) _cooldownModifier = 1;
        else _cooldownModifier = 1 + (_numberOfGunsWhichCanBeFired - _gunsSeprated[_ccurrentGunSetindex].guns.Count) / 10f;
    }
    public override void FireGuns()
    {
        if (!_canFire) return;
        _canFire = false;
       
        _audioEventPlayer.PlayeAudioEvent(_gunFireAudioEvent);
        int toFire = math.min(_numberOfGunsWhichCanBeFired, _gunsSeprated[_ccurrentGunSetindex].guns.Count);
        for (int i = 0; i < toFire; i++)
        {
            _gunsSeprated[_ccurrentGunSetindex].guns[i].Fire();
        }
        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {
        while(_timer<_fireCooldown)
        {
            if (PauseSettings.IsGamePaused) yield return null;
            else
            {
                _timer += Time.deltaTime * (_cooldownModifier+UpgradesManager.GetUpgradeLevel(_reloadUpgrade.Id)/10f);
                _cooldownImage.fillAmount = _timer / _fireCooldown;
                yield return null;
            }
        }
        _cooldownImage.fillAmount = 1;
        _canFire = true;
        _timer = 0;
    }
}
