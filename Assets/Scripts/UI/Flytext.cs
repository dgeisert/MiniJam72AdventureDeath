using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flytext : MonoBehaviour
{
    public static void CreateFlytext(Vector3 pos, string str, Color color, float size = 8, float duration = 1.5f, float up = 1)
    {
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Flytext"));
        go.transform.position = pos;
        go.GetComponent<Flytext>().Init(str, color, size, duration, up * size / 8f);
    }

    private TextMeshPro text;
    private float time = 0, duration;
    private float upValue;
    public void Init(string str, Color color, float size, float duration, float up)
    {
        text = gameObject.GetComponent<TextMeshPro>();
        text.alignment = TextAlignmentOptions.Center;
        text.fontSize = size;
        text.text = str;
        text.color = color;
        time = duration;
        this.duration = duration;
        upValue = up;
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position - Camera.main.transform.position);
        transform.position += Vector3.up * 0.01f * upValue * time / duration / duration;
        time -= Time.deltaTime;
        if (time < 0.5f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime * 2);
        }
    }
}