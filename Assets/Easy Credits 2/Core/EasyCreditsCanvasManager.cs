using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyCreditsCanvasManager : MonoBehaviour
{
    private Canvas canvas;
    private int DistanceToMove = 0;
    private GameObject panel;
    private float startDelay = 1;

    public Color backgroundColor = Color.black;
    public int scrollSpeed = 2;
    [HideInInspector]
    public bool scroll = true;
    [HideInInspector]
    public int offset = 0;
    [HideInInspector]
    public GameObject LastElement;

    private void Update()
    {
        startDelay -= Time.deltaTime;
        if (startDelay < 0)
        {
            if(scroll)
            panel.transform.Translate(transform.up * Time.deltaTime * scrollSpeed);
        }      
    }

    public void PlaceLogo(Sprite image, int scale)
    {
        GameObject TitleHolder = new GameObject("Title");
        TitleHolder.transform.parent = panel.transform;
        TitleHolder.AddComponent<SpriteRenderer>();
        TitleHolder.GetComponent<SpriteRenderer>().sprite = image;
        TitleHolder.transform.position += new Vector3(TitleHolder.transform.position.x, TitleHolder.transform.position.y + offset, 0);
        TitleHolder.transform.localScale += new Vector3(scale, scale, scale);
    }

    public void CreateCanvas()
    {
        canvas = this.gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        this.gameObject.AddComponent<CanvasScaler>();
        this.gameObject.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        Camera cam = Camera.main;
        canvas.worldCamera = cam;
        cam.orthographic = true;
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = backgroundColor;
    }

    public void CreatePannel()
    {
        panel = new GameObject();
        panel.name = "Panel";
        panel.transform.parent = this.transform;
        panel.transform.position = new Vector3(0, offset, 0);
    }

    public void PopulatePannel(string wordToWrite, int distanceToMove, Font font, int fontSize, Color color, bool last)
    {
        GameObject Element = new GameObject();
        Element.name = "Text Element";
        Element.transform.parent = panel.transform;
        Element.transform.position = panel.transform.position + new Vector3(0, DistanceToMove, 0);

        Text elementText = Element.AddComponent<Text>();
        elementText.text = wordToWrite;
        elementText.font = font;
        elementText.color = color;
        elementText.alignment = TextAnchor.MiddleCenter;
        elementText.fontSize = fontSize;
        elementText.verticalOverflow = VerticalWrapMode.Overflow;
        elementText.horizontalOverflow = HorizontalWrapMode.Overflow;

        Element.GetComponent<RectTransform>().localScale = new Vector3(.2f, .2f, .2f);
        DistanceToMove = DistanceToMove - distanceToMove;
        if(last)
        {
            LastElement = Element;
            Element.AddComponent<SpriteRenderer>();
        }

    }
}
