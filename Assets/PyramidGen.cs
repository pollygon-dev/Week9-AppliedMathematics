using UnityEngine;

public class PyramidGen : MonoBehaviour
{
    public Material material;
    public float pyramidSize = 5;
    public Vector3 pyramidPos = Vector3.zero;
    public float zPos = 10;

    private void OnRenderObject()
    {
        DrawPyramid();
    }

    public void DrawPyramid()
    {
        if (material == null)
        {
            return;
        }

        GL.PushMatrix();
        material.SetPass(0);
        GL.Begin(GL.LINES);

        Vector3[] baseVerts = new Vector3[4];
        baseVerts[0] = new Vector3(pyramidSize * 0.5f, -pyramidSize * 0.5f, pyramidSize * 0.5f);
        baseVerts[1] = new Vector3(-pyramidSize * 0.5f, -pyramidSize * 0.5f, pyramidSize * 0.5f);
        baseVerts[2] = new Vector3(-pyramidSize * 0.5f, -pyramidSize * 0.5f, -pyramidSize * 0.5f);
        baseVerts[3] = new Vector3(pyramidSize * 0.5f, -pyramidSize * 0.5f, -pyramidSize * 0.5f);

        Vector3 apex = new Vector3(0, pyramidSize * 0.5f, 0);

        for (int i = 0; i < 4; i++)
        {
            baseVerts[i] += pyramidPos;
        }
        apex += pyramidPos;

        for (int i = 0; i < 4; i++)
        {
            var perspective1 = PerpectiveCamera.Instance.GetPerspective(zPos + baseVerts[i].z);
            var perspective2 = PerpectiveCamera.Instance.GetPerspective(zPos + baseVerts[(i + 1) % 4].z);

            GL.Vertex(new Vector3(baseVerts[i].x * perspective1, baseVerts[i].y * perspective1, 0));
            GL.Vertex(new Vector3(baseVerts[(i + 1) % 4].x * perspective2, baseVerts[(i + 1) % 4].y * perspective2, 0));
        }

        for (int i = 0; i < 4; i++)
        {
            var basePerspective = PerpectiveCamera.Instance.GetPerspective(zPos + baseVerts[i].z);
            var apexPerspective = PerpectiveCamera.Instance.GetPerspective(zPos + apex.z);

            GL.Vertex(new Vector3(baseVerts[i].x * basePerspective, baseVerts[i].y * basePerspective, 0));
            GL.Vertex(new Vector3(apex.x * apexPerspective, apex.y * apexPerspective, 0));
        }

        GL.End();
        GL.PopMatrix();
    }
}