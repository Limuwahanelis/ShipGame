using TMPro;
using UnityEngine;

public class NotorityUI : MonoBehaviour
{
    [SerializeField] TMP_Text _notorityLevelTextField;
    [SerializeField] TMP_Text _notorityPointsTextField;

    public void SetNotorityLevel(int level)
    {
        _notorityLevelTextField.text=level.ToString();
    }
    public void SetNotorityPoints(int points)
    {
        _notorityPointsTextField.text = points.ToString();
    }
}
