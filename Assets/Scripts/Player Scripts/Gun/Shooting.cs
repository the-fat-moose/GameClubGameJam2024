using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Basic Gun Mechanics")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;

    [Header("Gunshot Drawing")]
    [SerializeField] Transform gunTip;
    [SerializeField] Material lineMat;
    private LineRenderer lr;

    RaycastHit hit;

    private void LateUpdate()
    {
        DrawShot();
    }

    public void Shoot(Transform shootPoint)
    {
        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, range))
        {
            if (gameObject.GetComponent<LineRenderer>() == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
            }
            lr.positionCount = 2;

            Target currentTarget = hit.transform.GetComponent<Target>();
            if (currentTarget != null)
            {
                currentTarget.TakeDamage(damage);
            }
        }
    }

    // Draw bullet line
    private void DrawShot()
    {
        if (!lr) return;
        lr.material = lineMat;
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, hit.point);
        StartCoroutine(DeleteShotLine());
    }

    private IEnumerator DeleteShotLine()
    {
        yield return new WaitForSeconds(0.1f);

        Destroy(lr);
    }
}
