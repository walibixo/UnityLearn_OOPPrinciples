using System.Collections;
using UnityEngine;

public class BubbleGun : Gun
{
    protected override void Start()
    {
        base.Start();

        Init(5.0f, 1.5f, 2.5f);
    }

    public override void Shoot(Vector3 position, Vector3 direction, bool fromPlayer)
    {
        if (TryToShoot())
        {
            // Grow the bubble before shooting it
            var bubble = Instantiate(ProjectilePrefab, position, ProjectilePrefab.transform.rotation);

            // Scale the bubble up
            StartCoroutine(ScaleBubble(bubble.transform));

            // Shoot the bubble
            ShootOneProjectile(bubble, direction, fromPlayer);
        }
    }

    private IEnumerator ScaleBubble(Transform bubbleTransform)
    {
        bubbleTransform.localScale = Vector3.zero;

        float elapsedTime = 0f;
        float duration = 1f;
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one * 2;

        while (elapsedTime < duration && bubbleTransform != null)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            bubbleTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        if (bubbleTransform != null)
            bubbleTransform.localScale = targetScale;
    }

}
