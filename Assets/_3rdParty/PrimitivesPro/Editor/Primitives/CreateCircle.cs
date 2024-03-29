// Version 2.1
// �2013 Reindeer Games
// All rights reserved
// Redistribution of source code without permission not allowed

using PrimitivesPro.Editor;
using UnityEditor;

[CustomEditor(typeof(PrimitivesPro.GameObjects.Circle))]
public class CreateCircle : CreatePrimitive
{
    private bool useFlipNormals = false;

    [MenuItem(MenuDefinition.Circle)]
    public static void Create()
    {
        var obj = PrimitivesPro.GameObjects.Circle.Create(1.0f, 20);
        obj.SaveStateAll();

        Selection.activeGameObject = obj.gameObject;
    }

    protected override bool ShowHeightHandles()
    {
        return false;
    }

    public override void OnInspectorGUI()
    {
        if (!base.IsValid())
        {
            return;
        }

        var obj = Selection.activeGameObject.GetComponent<PrimitivesPro.GameObjects.Circle>();

        if (target != obj)
        {
            return;
        }

        Utils.Toggle("Show scene handles", ref obj.showSceneHandles);
        bool colliderChange = Utils.MeshColliderSelection(obj);

        EditorGUILayout.Separator();

        useFlipNormals = obj.flipNormals;
        bool uiChange = false;

        uiChange |= Utils.SliderEdit("Radius", 0, 100, ref obj.radius);
        uiChange |= Utils.SliderEdit("Segments", 3, 100, ref obj.segments);

        EditorGUILayout.Separator();

        uiChange |= Utils.Toggle("Flip normals", ref useFlipNormals);
        uiChange |= Utils.Toggle("Share material", ref obj.shareMaterial);
        uiChange |= Utils.Toggle("Fit collider", ref obj.fitColliderOnChange);

        Utils.StatWindow(Selection.activeGameObject);

        Utils.ShowButtons<PrimitivesPro.GameObjects.Circle>(this);

        if (uiChange || colliderChange)
        {
            if (obj.generationMode == 0 && !colliderChange)
            {
                obj.GenerateGeometry();

                if (useFlipNormals)
                {
                    obj.FlipNormals();
                }
            }
            else
            {
                obj.GenerateColliderGeometry();
            }
        }
    }
}
