using UnityEngine;
using System.Collections;

public class GraphMaker : MonoBehaviour {

    // Use this for initialization
    void Start () {
        DrawAxis();
        DrawMarks();
        DrawArrows();
    }

    void DrawAxis()
    {
        makeLine(new Vector3(-5, -4, 0), new Vector3(-5, 0.8f, 0));
        makeLine(new Vector3(-5, -4, 0), new Vector3(6, -4, 0));
    }

    void DrawMarks()
    {
        //Y marks
        makeLine(new Vector3(-4.9f, -3.2f, 0), new Vector3(-5.1f, -3.2f, 0));
        makeLine(new Vector3(-4.9f, -2.4f, 0), new Vector3(-5.1f, -2.4f, 0));
        makeLine(new Vector3(-4.9f, -1.6f, 0), new Vector3(-5.1f, -1.6f, 0));
        makeLine(new Vector3(-4.9f, -0.8f, 0), new Vector3(-5.1f, -0.8f, 0));
        makeLine(new Vector3(-4.9f, 0, 0), new Vector3(-5.1f, 0, 0));

        //X marks
        makeLine(new Vector3(-4.5f, -4.1f, 0), new Vector3(-4.5f, -3.9f, 0));
        makeLine(new Vector3(-4f, -4.1f, 0), new Vector3(-4f, -3.9f, 0));
        makeLine(new Vector3(-3.5f, -4.1f, 0), new Vector3(-3.5f, -3.9f, 0));
        makeLine(new Vector3(-3f, -4.1f, 0), new Vector3(-3f, -3.9f, 0));
        makeLine(new Vector3(-2.5f, -4.1f, 0), new Vector3(-2.5f, -3.9f, 0));
        makeLine(new Vector3(-2f, -4.1f, 0), new Vector3(-2f, -3.9f, 0));
        makeLine(new Vector3(-1.5f, -4.1f, 0), new Vector3(-1.5f, -3.9f, 0));
        makeLine(new Vector3(-1f, -4.1f, 0), new Vector3(-1f, -3.9f, 0));
        makeLine(new Vector3(-0.5f, -4.1f, 0), new Vector3(-0.5f, -3.9f, 0));
        makeLine(new Vector3(0f, -4.1f, 0), new Vector3(0f, -3.9f, 0));
        makeLine(new Vector3(0.5f, -4.1f, 0), new Vector3(0.5f, -3.9f, 0));
        makeLine(new Vector3(1f, -4.1f, 0), new Vector3(1f, -3.9f, 0));
        makeLine(new Vector3(1.5f, -4.1f, 0), new Vector3(1.5f, -3.9f, 0));
        makeLine(new Vector3(2f, -4.1f, 0), new Vector3(2f, -3.9f, 0));
        makeLine(new Vector3(2.5f, -4.1f, 0), new Vector3(2.5f, -3.9f, 0));
        makeLine(new Vector3(3f, -4.1f, 0), new Vector3(3f, -3.9f, 0));
        makeLine(new Vector3(3.5f, -4.1f, 0), new Vector3(3.5f, -3.9f, 0));
        makeLine(new Vector3(4f, -4.1f, 0), new Vector3(4f, -3.9f, 0));
        makeLine(new Vector3(4.5f, -4.1f, 0), new Vector3(4.5f, -3.9f, 0));
        makeLine(new Vector3(5f, -4.1f, 0), new Vector3(5f, -3.9f, 0));
    }

    void DrawArrows()
    {
        makeLine(new Vector3(-5, 0.8f, 0), new Vector3(-4.9f, 0.6f,0));
        makeLine(new Vector3(-5, 0.8f, 0), new Vector3(-5.1f, 0.6f, 0));
        makeLine(new Vector3(6, -4, 0), new Vector3(5.8f, -4.1f, 0));
        makeLine(new Vector3(6, -4, 0), new Vector3(5.8f, -3.9f, 0));
    }
    void makeLine(Vector3 begin, Vector3 end)
    {
        LineRenderer line = new GameObject().AddComponent<LineRenderer>();
        line.transform.parent = this.transform.parent;
        line.SetWidth(0.025F, 0.025F);
        line.SetColors(Color.red, Color.red);
        line.SetPosition(0, begin);
        line.SetPosition(1, end);
        line.SetVertexCount(2);
        line.material = new Material(Shader.Find("Particles/Additive"));
    }

    // Update is called once per frame
    void Update () {
	
	}
}
