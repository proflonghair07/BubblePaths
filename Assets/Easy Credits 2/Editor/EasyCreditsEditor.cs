#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EasyCreditsDataManager))]
public class EasyCreditsEditor : Editor
{
    public Texture oneratLogo;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EasyCreditsDataManager easyCredits = (EasyCreditsDataManager)target;

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Defaults (Scrolling)"))
        {
            easyCredits.SectionLineSpacing = 20;
            easyCredits.RoleLineSpacing = 10;
            easyCredits.TitleLineSpacing = 5;
            easyCredits.RolefontSize = 22;
            easyCredits.TitlefontSize = 18;
            easyCredits.roleColor = Color.grey;
            easyCredits.titleColor = Color.white;
            easyCredits.offset = -55;
            easyCredits.scroll = true;
            easyCredits.exitTime = 0;
        }
        if (GUILayout.Button("Single Screen"))
        {
            easyCredits.SectionLineSpacing = 3;
            easyCredits.RoleLineSpacing = 6;
            easyCredits.TitleLineSpacing = 4;
            easyCredits.RolefontSize = 17;
            easyCredits.TitlefontSize = 13;
            easyCredits.roleColor = Color.grey;
            easyCredits.titleColor = Color.white;
            easyCredits.offset = 40;
            easyCredits.scroll = false;
            easyCredits.exitTime = 10;
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("Easy Credits 2.0 - Created By @oneratdylan");
        if(GUILayout.Button("Join the Onerat Discord"))
        {
            Application.OpenURL("https://discord.com/invite/oneratgames");
        }

    }
}
#endif
