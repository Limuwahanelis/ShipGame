using TMPro;
using UnityEngine;

public class SimpleIntUI : MonoBehaviour
{
    [SerializeField] TMP_Text _valueTextField;

    public void SetValue(int num)
    {
        _valueTextField.text = num.ToString();
    }
}
