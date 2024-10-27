using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public Image image;
    public float flashSpeed;
    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.onTakeDamage += Flash;
    }

    // Update is called once per frame
    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        image.enabled = true; // 이미지 켜고
        image.color = new Color(1f, 100f / 255f, 100f / 255f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startAlpha = 0.3f;
        float a = startAlpha;

        while(a > 0)
        {
            a -=(startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 100f/ 255f, 100f / 255f, a);
            yield return null; // 반환할게 없으면 null
        }

        image.enabled = false;
    }
}
