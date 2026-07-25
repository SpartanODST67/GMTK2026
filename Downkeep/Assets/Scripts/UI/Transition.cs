using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Transition : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] float transitionCompleteThreshold = 0.1f;
    [SerializeField] float speed = 1f;
    public bool playOnStart = false;
    [SerializeField] Vector3 onStartTransitionEndPos;
    [SerializeField] UnityEvent onStartTransitionComplete;

    void Start()
    {
        if(playOnStart)
        {
            Slide(rectTransform.anchoredPosition, onStartTransitionEndPos, onStartTransitionComplete);
        }
    }

    public void Slide(Vector3 start, Vector3 end, UnityEvent onComplete)
    {
        StartCoroutine(SlideRoutine(start, end, onComplete));
    }

    IEnumerator SlideRoutine(Vector3 start, Vector3 position, UnityEvent onComplete)
    {
        rectTransform.anchoredPosition = start;

        while (Vector3.Distance(rectTransform.anchoredPosition, position) > transitionCompleteThreshold)
        {
            if (Mathf.Abs(Vector3.Distance(rectTransform.anchoredPosition, position)) <= speed)
                break;

            rectTransform.anchoredPosition = Vector3.MoveTowards(
                rectTransform.anchoredPosition,
                position,
                speed
            );

            yield return null;
        }

        rectTransform.anchoredPosition = position;
        onComplete.Invoke();
    }
}
