using UnityEngine;
using TMPro;
using System.Collections;

public class MessageDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    private Coroutine currentRoutine;

    /// Displays a message with specified duration and font size.
    public void ShowMessage(string message, float duration, float fontSize)
    {
        if (messageText == null)
        {
            Debug.LogWarning("MessageDisplay: No TextMeshProUGUI assigned.");
            return;
        }
        // If a message is already displaying, stop it
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        messageText.text = message;
        messageText.fontSize = fontSize;
        messageText.gameObject.SetActive(true);
        currentRoutine = StartCoroutine(HideAfterDelay(duration));
    }

    private IEnumerator HideAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        messageText.gameObject.SetActive(false);
        currentRoutine = null;
    }
}
