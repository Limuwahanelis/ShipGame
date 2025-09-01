using UnityEngine;

public class PirateBase : MonoBehaviour,IInteractable
{

    [SerializeField] PauseSetter _pauseSetter;
    [SerializeField] GameObject _shopPanel;
    [SerializeField] Color _interactableColor;
    [SerializeField] SpriteRenderer _renderer;
    private Color _normalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _normalColor = _renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPlayerInteractions(Collider2D col)
    {
        PlayerInteractions playerInteractions = col.attachedRigidbody.GetComponent<PlayerInteractions>();
        playerInteractions.SetInteractable(this);
        _renderer.color = _interactableColor;
    }
    public void RemovePlayerInteractions(Collider2D col)
    {
        PlayerInteractions playerInteractions = col.attachedRigidbody.GetComponent<PlayerInteractions>();
        playerInteractions.SetInteractable(null);
        _renderer.color = _normalColor;
    }

    public void Interact()
    {
        _pauseSetter.SetPause(true);
        _shopPanel.SetActive(true);
    }

}
