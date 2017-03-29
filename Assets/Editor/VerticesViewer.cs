using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class VerticesViewer : EditorWindow
{

    Transform selectedTransform;
    MeshFilter filter = null;
    Mesh selectedMesh = null;
    Texture2D dot_white = null;
    Texture2D dot_red = null;
    Vector2[] uv = null;
    int[] triangles = null;

    int windowSize = 300;

    Rect rect_group;
    Rect rect_area;
    Rect dotPos;
    GUIStyle customStyle;

    Rect rect_vertAmount;
    Rect rect_trisAmount;
    Rect rect_windowSize;
    string inputString = string.Empty;
    Rect rect_windowSizeApply;

    Rect avgCoordinate;
    Vector2 avgValue = Vector2.zero;

    public class Vert
    {
        public Vector2 position = Vector2.zero;
        public Vector2 real = Vector2.zero;
        public bool selected;

        public Vert(Vector2 newPosition, Vector2 newReal, bool isSelected)
        {
            this.position = newPosition;
            this.real = newReal;
            this.selected = isSelected;
        }
    }
    Vert[] verts;

    //--------------------
    private Event curEvent;
    private bool isHolding = false;
    private Vector2 dragStart = Vector2.zero;
    private Rect holdRect;

    private EditorWindow myWindow;


    [MenuItem("Tools/Vertices Viewer 1.0")]
    private static void showVerticesViewer()
    {
        EditorWindow.GetWindow<VerticesViewer>(false, "Vertices Viewer 1.0");
    }

    void OnEnable()
    {
        myWindow = EditorWindow.GetWindow<VerticesViewer>();
        SetRects();

        customStyle = new GUIStyle();
        customStyle.alignment = TextAnchor.MiddleCenter;
        customStyle.normal.textColor = Color.black;
        customStyle.wordWrap = true;


        dot_white = new Texture2D(3, 3);
        dot_red = new Texture2D(3, 3);

        int y = 0;
        while (y < dot_white.height)
        {
            int x = 0;
            while (x < dot_white.width)
            {
                dot_white.SetPixel(x, y, Color.white);
                dot_red.SetPixel(x, y, Color.red);



                ++x;
            }
            ++y;
        }
        dot_white.Apply();
        dot_red.Apply();
    }

    private void SetRects()
    {
        rect_group = new Rect(10, 10, windowSize, windowSize);
        rect_area = new Rect(0, 0, windowSize, windowSize);
        inputString = windowSize.ToString();
        dotPos = new Rect(0, 0, 3, 3);

        rect_vertAmount = new Rect(rect_group.xMax + 10, rect_group.y, 160, 30);
        rect_trisAmount = rect_vertAmount;
        rect_trisAmount.y += rect_vertAmount.height + 5;

        rect_windowSize = rect_trisAmount;
        rect_windowSize.y += rect_trisAmount.height + 15;

        rect_windowSizeApply = rect_windowSize;
        rect_windowSizeApply.y += rect_windowSize.height + 2;

        avgCoordinate = rect_windowSizeApply;
        avgCoordinate.y += rect_windowSizeApply.height + 30;
        avgCoordinate.height = 70;

        SetUVSize();
    }

    private void SetUVSize()
    {
        if (verts != null)
        {
            Vector2 newUV = Vector2.zero;

            for (int j = 0; j < verts.Length; j++)
            {
                newUV = uv[j];
                newUV.x *= windowSize;
                newUV.y = 1.0f - newUV.y;
                newUV.y *= windowSize;

                verts[j] = new Vert(newUV, uv[j], false);
            }
        }
    }

    void OnGUI()
    {
        if (Selection.activeTransform != selectedTransform)
        {
            selectedTransform = Selection.activeTransform;

            if (selectedTransform != null)
            {
                filter = Selection.activeGameObject.GetComponent<MeshFilter>();

                if (filter != null)
                {
                    selectedMesh = filter.sharedMesh;
                    uv = selectedMesh.uv;
                    triangles = selectedMesh.triangles;

                    verts = new Vert[uv.Length];

                    SetUVSize();
                }
            }
        }

        //Something selected
        if (Selection.activeTransform != null)
        {
            GUI.BeginGroup(rect_group);
            GUI.Box(rect_area, "");

            byte tempCounter = 0;
            int tempNext = 0;

            //just in case
            if (verts != null && verts.Length > 0)
            {
                //Line drawing
                for (int i = 0; i < triangles.Length; i++)
                {
                    if (tempCounter == 2) tempNext = i - 2;
                    else tempNext = i + 1;    //0 or 1

                    Vector2 from = new Vector2(verts[triangles[i]].position.x, FixHeight(verts[triangles[i]].position.y));
                    Vector2 to = new Vector2(verts[triangles[tempNext]].position.x, FixHeight(verts[triangles[tempNext]].position.y));

                    DrawLine(from, to, Color.gray);


                    tempCounter++;
                    if (tempCounter > 2)
                    {
                        tempCounter = 0;
                    }
                }
                //Vertices drawing

                for (int i = 0; i < verts.Length; i++)
                {
                    dotPos.center = new Vector2(verts[i].position.x, FixHeight(verts[i].position.y));

                    if (verts[i].selected) GUI.DrawTexture(dotPos, dot_red);
                    else GUI.DrawTexture(dotPos, dot_white);
                }
            }

            GUI.EndGroup();

            //Other information
            //Vertices amount
            GUI.Box(rect_vertAmount, "");
            GUI.Box(rect_vertAmount, "Unique Vertices: " + (verts.Length).ToString(), customStyle);
            //Tris
            GUI.Box(rect_trisAmount, "");
            GUI.Box(rect_trisAmount, "Triangles: " + (triangles.Length / 3).ToString(), customStyle);
            //Window size
            GUI.Box(rect_windowSize, "");
            inputString = GUI.TextField(rect_windowSize, inputString, customStyle);

            if (GUI.Button(rect_windowSizeApply, "")) NewWindowSizeApply();
            GUI.Label(rect_windowSizeApply, "Apply New Size", customStyle);

            GUI.Box(avgCoordinate, "");
            GUI.Box(avgCoordinate, "*AVERAGE*\n\nX: " + avgValue.x + "\nY: " + (1.0f - avgValue.y), customStyle);

            //----------------------------
            curEvent = Event.current;

            switch (curEvent.button)
            {
                case 0:
                    switch (curEvent.type)
                    {
                        case EventType.MouseDown:
                            dragStart = curEvent.mousePosition;
                            isHolding = true;
                            break;

                        case EventType.MouseUp:
                            CheckSelectedVerts();
                            isHolding = false;
                            break;

                            break;
                    }

                    break;

                case 1:
                    switch (curEvent.type)
                    {
                        case EventType.MouseDown:
                            UnSelectAll();
                            break;
                    }
                    break;
            }

            if (isHolding)
            {
                holdRect = new Rect(dragStart.x, dragStart.y, Event.current.mousePosition.x - dragStart.x, Event.current.mousePosition.y - dragStart.y);
                GUI.Box(holdRect, "");
            }
        }

        //Nothing selected
        else
        {
            GUI.Box(rect_group, "");
            GUI.Box(rect_group, "Nothing selected!", customStyle);
        }

        Repaint();
    }

    private void CheckSelectedVerts()
    {
        Rect compareHold = holdRect;
        compareHold.x -= 10;
        compareHold.y -= 10;

        Vector2 temp_am = Vector2.zero;
        int temp_total = 0;

        for (int i = 0; i < verts.Length; i++)
        {
            if (compareHold.Contains(verts[i].position)) verts[i].selected = true;


            if (verts[i].selected)
            {
                //Debug.Log("is selected");
                temp_am += verts[i].real;
                temp_total++;
            }
        }

        if (temp_total > 0)
        {
            avgValue = temp_am / temp_total;
        }
        else
        {
            avgValue = Vector2.zero;
        }
    }


    private void UnSelectAll()
    {
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i].selected = false;
        }
        avgValue = Vector2.zero;
    }

    private void NewWindowSizeApply()
    {
        if (!int.TryParse(inputString, out windowSize))
        {
            Debug.Log("VERTICES VIEWER: Numbers only! Setting to default 300.");
            windowSize = 300;
        }

        SetRects();
    }

    private float FixHeight(float y)
    {
        //return (y * windowSize);
        return y;
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    protected static bool clippingEnabled;
    protected static Rect clippingBounds;
    protected static Material lineMaterial;

    public void DrawLine(Vector2 pointA, Vector2 pointB, Color color)
    {
        if (clippingEnabled)
            if (!segment_rect_intersection(clippingBounds, ref pointA, ref pointB))
                return;

        if (!lineMaterial)
        {
            lineMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
                                        "SubShader { Pass {" +
                                        "   BindChannels { Bind \"Color\",color }" +
                                        "   Blend SrcAlpha OneMinusSrcAlpha" +
                                        "   ZWrite Off Cull Off Fog { Mode Off }" +
                                        "} } }");
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
        }

        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(color);
        GL.Vertex3(pointA.x, pointA.y, 0);
        GL.Vertex3(pointB.x, pointB.y, 0);
        GL.End();
    }

    float r = 0;
    bool retval = true;
    private bool clip_test(float p, float q, ref float u1, ref float u2)
    {
        retval = true;
        if (p < 0.0)
        {
            r = q / p;
            if (r > u2)
                retval = false;
            else if (r > u1)
                u1 = r;
        }
        else if (p > 0.0)
        {
            r = q / p;
            if (r < u1)
                retval = false;
            else if (r < u2)
                u2 = r;
        }
        else
            if (q < 0.0)
                retval = false;

        return retval;
    }

    private bool segment_rect_intersection(Rect bounds, ref Vector2 p1, ref Vector2 p2)
    {
        float u1 = 0.0f, u2 = 1.0f, dx = p2.x - p1.x, dy;
        if (clip_test(-dx, p1.x - bounds.xMin, ref u1, ref u2))
            if (clip_test(dx, bounds.xMax - p1.x, ref u1, ref u2))
            {
                dy = p2.y - p1.y;
                if (clip_test(-dy, p1.y - bounds.yMin, ref u1, ref u2))
                    if (clip_test(dy, bounds.yMax - p1.y, ref u1, ref u2))
                    {
                        if (u2 < 1.0)
                        {
                            p2.x = p1.x + u2 * dx;
                            p2.y = p1.y + u2 * dy;
                        }
                        if (u1 > 0.0)
                        {
                            p1.x += u1 * dx;
                            p1.y += u1 * dy;
                        }
                        return true;
                    }
            }
        return false;
    }
}