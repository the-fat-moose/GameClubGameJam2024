using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public Image fade;

    private void Awake()
    {
        StartCoroutine(FadeIn(0.5f));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator FadeIn(float duration)
    {
        fade.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1f);
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, counter / duration);

            fade.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        fade.enabled = false;
    }
}
