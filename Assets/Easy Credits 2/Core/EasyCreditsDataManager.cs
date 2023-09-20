using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyCreditsDataManager : MonoBehaviour
{
    private TextAsset CSV;
    private string[] data;
    private bool startingPassComplete = false;

    public Sprite GameTitleImage;
    public int gameTitleScale = 0;
    public string GameTitle;
    public Font gameTitleFont;
    [Range(0.0f, 50.0f)]
    public int GameTitleLineSpacing = 10;
    [Range(0.0f, 50.0f)]
    public int GameTitlefontSize = 22;
    public Color GameTitleColor = Color.grey;
    [Space]
    [Range(0.0f, 50.0f)]
    public int SectionLineSpacing = 20;
    [Space]
    public Font roleFont;
    [Range(0.0f, 50.0f)]
    public int RoleLineSpacing = 10;
    [Range(0.0f, 50.0f)]
    public int RolefontSize = 22;
    public Color roleColor = Color.grey;
    [Space]
    public Font titleFont;
    [Range(0.0f, 50.0f)]
    public int TitleLineSpacing = 5;
    [Range(0.0f, 50.0f)]
    public int TitlefontSize = 18;
    public Color titleColor = Color.white;
    [Space]
    [Range(-100.0f, 100.0f)]
    public int offset = 0;

    public float exitTime = 0;
    public int SceneToExitTo;

    public string LegalLine = "Credits powered by Easy Credits";

    [HideInInspector]
    public bool scroll = true;

    private void Start()
    {
        this.GetComponent<EasyCreditsCanvasManager>().scroll = scroll;
        this.GetComponent<EasyCreditsCanvasManager>().offset = offset;
        this.GetComponent<EasyCreditsCanvasManager>().CreateCanvas();
        this.GetComponent<EasyCreditsCanvasManager>().CreatePannel();
        CreateData();
    }
    public void CreateData()
    {
        if(GameTitleImage)
        {
            this.GetComponent<EasyCreditsCanvasManager>().PlaceLogo(GameTitleImage, gameTitleScale);
            this.GetComponent<EasyCreditsCanvasManager>().PopulatePannel(GameTitle, GameTitleLineSpacing, gameTitleFont, GameTitlefontSize, GameTitleColor, false);
        }
        else
        if(GameTitle != "")
            this.GetComponent<EasyCreditsCanvasManager>().PopulatePannel(GameTitle, GameTitleLineSpacing, gameTitleFont, GameTitlefontSize, GameTitleColor, false);

        CSV = Resources.Load<TextAsset>("Credits"); // load credits file
        data = CSV.text.Split(new char[] { '\n' }); // create array of rows

        string[] topRowData = data[0].Split(new char[] { ',' }); // create string with role data
        for (int i = 0; i < topRowData.Length; i++) // loop through roles
        {
            if (topRowData[i] != "") // if role isnt null
            {
                if (startingPassComplete == false)
                    startingPassComplete = true;
                else
                    this.GetComponent<EasyCreditsCanvasManager>().PopulatePannel("", SectionLineSpacing, roleFont, RolefontSize, roleColor, false);
                this.GetComponent<EasyCreditsCanvasManager>().PopulatePannel(topRowData[i], RoleLineSpacing, roleFont, RolefontSize, roleColor, false);
                for (int x = 1; x < data.Length; x++)
                {
                    string[] RowData = data[x].Split(new char[] { ',' }); // create string with role data
                    if (RowData[i] != "")
                    {
                        this.GetComponent<EasyCreditsCanvasManager>().PopulatePannel(RowData[i], TitleLineSpacing, titleFont, TitlefontSize, titleColor, false);
                    }
                }
            }
        }
        this.GetComponent<EasyCreditsCanvasManager>().PopulatePannel(LegalLine, TitleLineSpacing, titleFont, TitlefontSize, titleColor, true);
    }
    private bool isVis = false;
    private void Update()
    {
        if (this.GetComponent<EasyCreditsCanvasManager>().LastElement.GetComponent<Renderer>().isVisible)
        {
            isVis = true;
        }
        if(isVis)
        {
            if (this.GetComponent<EasyCreditsCanvasManager>().LastElement.GetComponent<Renderer>().isVisible == false)
            {
                StartCoroutine(Exit());
            }
        }
        if (!scroll)
        {
            StartCoroutine(Exit());
        }
    }
    IEnumerator Exit()
    {
        yield return new WaitForSeconds(exitTime);
        Application.LoadLevel(SceneToExitTo);
    }
}

