using System.Collections;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField] private Color dayTimeColor;
    [SerializeField] private Color nightTimeColor;

    [SerializeField] private Color currentColor;

    [SerializeField] private float changeTime = 20f;

    // Start is called before the first frame update
    private void Start()
    {
        currentColor = dayTimeColor;

        StartCoroutine(ChangeColor());
    }

    // Update is called once per frame
    private void Update()
    {
        RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, currentColor, Time.deltaTime * 0.1f);
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(changeTime);

        if(currentColor == dayTimeColor)
            currentColor = nightTimeColor;
        else
            currentColor = dayTimeColor;

        StartCoroutine(ChangeColor());

    }
}
