using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [Tooltip("Der verwendete Slider zur Steuerung der Anzeige")] [SerializeField]
    private Slider slider;

    [Tooltip("Der Minimale angezeigte Wert der Bar")] [SerializeField]
    private int minimum;

    [Tooltip("Der maximale angezeigte Wert der Bar")] [SerializeField]
    private int maximum;

    [FormerlySerializedAs("value")] [Tooltip("Der reale aktuelle Wert der Bar")] [SerializeField]
    private int currentValue;

    [Tooltip("Die Anzeigefläche der Komponente (TMP Text)")] [SerializeField]
    private TMP_Text TextComponent;

    public void SetBarMinimum(int value)
    {
        this.minimum = value;
        this.slider.minValue = value;
    }

    public int GetBarMinimum()
    {
        return this.minimum;
    }

    public void SetBarMaximum(int value)
    {
        this.maximum = value;
        this.slider.maxValue = value;
    }

    public int GetBarMaximum()
    {
        return this.maximum;
    }

    public void CountDown(int value, bool refreshValueText = true)
    {
        // Steuerung Bar und Slider
        if ((slider.value - value) < this.minimum)
        {
            slider.value = this.minimum;
        }
        else if (slider.value - value > this.maximum)
        {
            slider.value = this.maximum;
        }
        else
        {
            slider.value = slider.value - value;
        }

        // Steuerung realer Wert
        this.currentValue = this.currentValue - 1;
        if (refreshValueText)
        {
            SetValueText(this.currentValue.ToString());
        }
    }

    public void CountUp(int value, bool refreshValueText = true)
    {
        // Steuerung Bar und Slider
        if ((slider.value + value) < this.minimum)
        {
            slider.value = this.minimum;
        }
        else if (slider.value + value > this.maximum)
        {
            slider.value = this.maximum;
        }
        else
        {
            slider.value = slider.value + value;
        }

        // Steuerung realer Wert
        this.currentValue = this.currentValue + value;
        if (refreshValueText)
        {
            SetValueText(this.currentValue.ToString());
        }
    }

    /// <summary>
    /// Setzt direkt einen neuen aktuellen Wert. Die Werte für das Minimum und Maximum der Anzeige
    /// wird nicht geändert. 
    /// </summary>
    /// <param name="value">Der neue aktuelle Wert</param>
    /// <param name="refreshValueText">True, wenn die Wertanzeige aktualisiert werden soll, ansonsten False</param>
    public void SetCurrentValue(int value, bool refreshValueText = true)
    {
        // Steuerung Bar und Slider
        if ((value) < this.minimum)
        {
            slider.value = this.minimum;
        }
        else if (value > this.maximum)
        {
            slider.value = this.maximum;
        }
        else
        {
            slider.value = value;
        }

        // Steuerung realer Wert
        this.currentValue = value;
        if (refreshValueText)
        {
            SetValueText(this.currentValue.ToString());
        }
    }

    public int GetCurrentValue()
    {
        return this.currentValue;
    }

    public void SetValueText(string value)
    {
        TextComponent.SetText(value);
    }
}