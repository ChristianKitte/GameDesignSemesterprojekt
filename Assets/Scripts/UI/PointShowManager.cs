using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointShowManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Color hintColor;
    [SerializeField] private Color pointPositiveColor;
    [SerializeField] private Color pointNegativeColor;
    [SerializeField] private float fadeOutSpeed = 0.01f;
    [SerializeField] private float hintSize = 50.0f;
    [SerializeField] private float pointSize = 150.0f;

    private float visibility;
    private Color currentColor;
    private int _points;

    // Start is called before the first frame update
    void Start()
    {
        promptText.color = new Color(0, 0, 0, 0.0f);
        currentColor = pointPositiveColor;
        _points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (visibility > 0.0f)
        {
            visibility = visibility - fadeOutSpeed;

            setColor(currentColor);
        }
    }

    public void showPoints(int points)
    {
        visibility = 1.0f;
        _points = points;

        if (_points <= 0)
        {
            setColor(pointNegativeColor);
        }
        else
        {
            setColor(pointPositiveColor);
        }

        promptText.fontSize = pointSize;
        promptText.text = _points.ToString();
    }

    public void showHint(string hint, int seconds)
    {
        visibility = 1.0f;
        _points = seconds;

        setColor(hintColor);
        promptText.fontSize = hintSize;
        promptText.text = $"{hint} - {seconds.ToString()} Sekunden";
    }

    private void setColor(Color color)
    {
        currentColor = color;

        color.a = visibility;
        promptText.color = color;

        /*
        if (_points <= 0)
        {
            promptText.color = new Color(1, 0, 0, visibility);
        }
        else
        {
            promptText.color = new Color(0, 1, 0, visibility);
        }
        */
    }
}