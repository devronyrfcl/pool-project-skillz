using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TextEffect : MonoBehaviour
{
    [Header("References")]
    public Image image; // Reference to the Image component
    public TextMeshProUGUI excitementText; // Reference to the TextMeshProUGUI component
    public Sprite[] sprites; // Array of sprites for random assignment
    public string[] excitementMessages; // Array of random excitement messages
    public AudioClip[] excitementSounds; // Array of random sounds to play
    public AudioClip clapSound; // Clap sound to play
    public AnimationCurve clapVolumeCurve; // Curve for controlling the clap sound volume over time

    private Vector3 defaultTextSize = new Vector3(0, 1, 1); // Default size of the text
    private Vector2 defaultImageSize = new Vector2(1, 0); // Default size of the image (height is 0)

    void Start()
    {
        // Set the initial states of the image and text
        ResetEffect();
    }

    /// <summary>
    /// Called to trigger the excitement effect.
    /// </summary>
    public void TriggerEffect()
    {
        // Start the effect animation coroutine
        StartCoroutine(ShowExcitementEffect());
    }

    /// <summary>
    /// Coroutine to handle showing the excitement effect.
    /// </summary>
    private IEnumerator ShowExcitementEffect()
    {
        // Randomize sprite, excitement text, and sound
        int randomSpriteIndex = Random.Range(0, sprites.Length);
        int randomTextIndex = Random.Range(0, excitementMessages.Length);
        int randomSoundIndex = Random.Range(0, excitementSounds.Length);

        // Assign random sprite to image
        image.sprite = sprites[randomSpriteIndex];

        // Set random excitement text
        excitementText.text = excitementMessages[randomTextIndex];

        // Play random excitement sound
        AudioSource.PlayClipAtPoint(excitementSounds[randomSoundIndex], Camera.main.transform.position);

        // Play the clap sound with volume control
        AudioSource clapAudioSource = new GameObject("ClapSound").AddComponent<AudioSource>();
        clapAudioSource.clip = clapSound;
        clapAudioSource.loop = false;

        // Start playing clap sound with a volume curve
        StartCoroutine(PlayClapSoundWithVolumeCurve(clapAudioSource));

        // Animate the image height
        LeanTween.value(image.gameObject, 0f, 250f, 1.5f).setOnUpdate((float value) =>
        {
            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, value);
        }).setEase(LeanTweenType.easeOutBounce);

        // Animate the text size
        LeanTween.scale(excitementText.gameObject, new Vector3(1, 1, 1), 1.5f).setEase(LeanTweenType.easeOutBounce);

        // Wait for 1.5 seconds for animation to complete
        yield return new WaitForSeconds(1.5f);

        // Reset back to default states with animation
        ResetEffectWithAnimation();
    }

    /// <summary>
    /// Coroutine to play the clap sound with a volume curve.
    /// </summary>
    private IEnumerator PlayClapSoundWithVolumeCurve(AudioSource clapAudioSource)
    {
        clapAudioSource.Play(); // Start playing the clap sound
        
        float duration = 1.5f; // Duration of the sound and volume change
        float elapsedTime = 0f;

        // Gradually change the volume of the clap sound based on the curve
        while (elapsedTime < duration)
        {
            float volume = clapVolumeCurve.Evaluate(elapsedTime / duration); // Get volume from curve
            clapAudioSource.volume = volume; // Set the volume
            elapsedTime += Time.deltaTime; // Increment time
            yield return null;
        }

        // Ensure the volume is set to 1 at the end
        clapAudioSource.volume = clapVolumeCurve.Evaluate(1f);

        // Wait for sound to finish
        yield return new WaitForSeconds(clapAudioSource.clip.length);

        // Destroy the clap AudioSource object after sound finishes
        Destroy(clapAudioSource.gameObject);
    }

    /// <summary>
    /// Resets the effect to default size and state with animation.
    /// </summary>
    private void ResetEffectWithAnimation()
    {
        // Animate the image height back to 0
        LeanTween.value(image.gameObject, 250f, 0f, 1.5f).setOnUpdate((float value) =>
        {
            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, value);
        }).setEase(LeanTweenType.easeInBounce);

        // Animate the text size back to default
        LeanTween.scale(excitementText.gameObject, defaultTextSize, 1.5f).setEase(LeanTweenType.easeInBounce);

        // Wait for 1 second before resetting the text and image state
        StartCoroutine(WaitAndReset());
        
    }

    

    /// <summary>
    /// Coroutine to wait for 1 second before resetting the text and image state.
    /// </summary>
    private IEnumerator WaitAndReset()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(2f);

        // Reset excitement text
        excitementText.text = "";

        // Reset image sprite to null (no sprite)
        image.sprite = null;
    }
    
        private void ResetEffect()
        {
            // Reset image height
            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, 0);

            // Reset text size
            excitementText.transform.localScale = defaultTextSize;

            // Reset excitement text
            excitementText.text = "";

            // Reset image sprite
            image.sprite = null;
        }
   
}
