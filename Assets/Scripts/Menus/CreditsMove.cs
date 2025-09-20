using TMPro;
using UnityEngine;
using System.Collections;

public class CreditsMove : CreditsMenu
{
    public TextMeshProUGUI myText;
    private RectTransform textRectTransform;
    [SerializeField] private float speed = 50f; // Adjust as needed

    private Coroutine moveCoroutine;
    private Vector2 initialPosition;

    void Start()
    {
        textRectTransform = myText.GetComponent<RectTransform>();
        initialPosition = textRectTransform.anchoredPosition; // Store starting position
    }

    // Call this when CreditsPanel opens
    public void StartMovingText(float distance, float duration)
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        Vector2 targetPosition = textRectTransform.anchoredPosition + new Vector2(0, distance);
        moveCoroutine = StartCoroutine(MoveTextCoroutine(targetPosition, duration));
    }

    IEnumerator MoveTextCoroutine(Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = textRectTransform.anchoredPosition;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            textRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textRectTransform.anchoredPosition = targetPosition;
    }

    // Call this when CreditsPanel closes
    public void ResetTextPosition()
    {
        textRectTransform.anchoredPosition = initialPosition;
    }
}
