using UnityEngine;
using UnityEngine.UI;

public class PowerSliderShow : MonoBehaviour
{
    [Header("UI References")]
    public Slider powerSlider; // The slider for power adjustment
    public Image backgroundImage; // The background image to fade

    void Start()
    {
        if (powerSlider == null || backgroundImage == null)
        {
            Debug.LogError("PowerSlider or BackgroundImage is not assigned.");
            return;
        }

        // Initialize the slider and set the value changed listener
        powerSlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Set the initial background color based on the slider's starting value
        UpdateBackgroundColor(powerSlider.value);
    }

    /// <summary>
    /// Called whenever the slider's value changes.
    /// </summary>
    /// <param name="value">The current slider value.</param>
    void OnSliderValueChanged(float value)
    {
        UpdateBackgroundColor(value);
    }

    /// <summary>
    /// Updates the background color based on the slider value.
    /// </summary>
    /// <param name="value">The slider value (1 to 0).</param>
    void UpdateBackgroundColor(float value)
    {
        // Interpolate between black and white based on the slider value
        Color newColor = Color.Lerp(Color.white, Color.black, value);
        backgroundImage.color = newColor;
    }
}
