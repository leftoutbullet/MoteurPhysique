using UnityEngine;

public class ShrinkObject : MonoBehaviour
{
    private Vector3 targetScale; // Dynamic target scale (half of the current scale).
    public float shrinkSpeed = 1.0f; // Speed of shrinking.
    private bool isShrinking = false;

    void Update()
    {
        if (isShrinking)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * shrinkSpeed);

            // Stop shrinking when the object is very close to the target scale.
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
                isShrinking = false;
            }
        }
    }

    public void StartShrinking()
    {
        // Set the target scale to half of the current scale.
        targetScale = transform.localScale * 0.5f;
        isShrinking = true;
    }

    public void ResetScale(Vector3 newScale)
    {
        transform.localScale = newScale;
        isShrinking = false;
    }
}
