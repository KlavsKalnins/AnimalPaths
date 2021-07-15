using System.Collections;
using UnityEngine;


public class Footprint : MonoBehaviour
{
    public bool isLeft;
    [SerializeField] private Sprite[] animalFootprints;
    public Color startColor;
    public Color wrongColor;
    public Color highlightColor;
    [SerializeField] private SpriteRenderer sr;
    
    bool changingColor = false;

    private void OnEnable()
    {
        sr.color = startColor;
    }

    public void LerpColorTo(int i)
    {
        switch (i)
        {
            case 0:
                StartCoroutine(lerpColor(sr.color, highlightColor, 1));
                break;
            case 1:
                StartCoroutine(lerpColor(sr.color, wrongColor, 1));
                break;
            case 2:
                StartCoroutine(lerpColor(sr.color, startColor, 1));
                break;
        }
    }
    
    IEnumerator lerpColor(Color fromColor, Color toColor, float duration)
    {
        if (changingColor)
        {
            yield break;
        }
        changingColor = true;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            float colorTime = counter / duration;
            Debug.Log(colorTime);
            
            sr.color = Color.Lerp(fromColor, toColor, counter / duration);
            
            yield return null;
        }
        changingColor = false;
    }

    public void SetSprite(int index)
    {
        if (index <= animalFootprints.Length)
            GetComponent<SpriteRenderer>().sprite = animalFootprints[index];
    }
}