using System.Collections.Generic;
using UnityEngine;
public class CrossHull
{
    public List<List<Vector3>> vertices;

    public CrossHull()
    {
        vertices = new List<List<Vector3>>();
    }
    public CrossHull(List<List<Vector3>> verts, List<int> ids)
    {
        vertices = verts;
    }

    //before adding vertices, check if the vertices belong to the existing contour
    //Here we assume each time we add only two vertices
    public void checkContourId(Vector3 v1, Vector3 v2)
    {
        int index1 = -1;
        int index2 = -1;
        //check if the vertices belong to the existing contour
        for (int i = 0; i < vertices.Count; i++)
        {
            if (vertices[i].Contains(v1))
            {
                index1 = i;
            }
            if (vertices[i].Contains(v2))
            {
                index2 = i;
            }
        }
        // if both vertices belong to the same contour do nothing
        // I hope it does not happen, but if it does, we can just ignore it
        if (index1 != -1 && index1 == index2)
        {
            Debug.Log("Both vertices belong to the same contour, werid");
            return;
        }
        //if both vertices belong to two distinct contours, merge them
        if (index1 != -1 && index2 != -1 && index1 != index2)
        {

            vertices[index1].AddRange(vertices[index2]);
            vertices.RemoveAt(index2);
            return;
        }
        //if one of the vertices belongs to the existing contour, add the other vertex to it
        if (index1 != -1)
        {
            vertices[index1].Add(v2);
            return;
        }
        //if one of the vertices belongs to the existing contour, add the other vertex to it
        if (index2 != -1)
        {
            vertices[index2].Add(v1);
            return;
        }
        // otherwise just add a new contour
        List<Vector3> newContour = new List<Vector3> { v1, v2 };
        vertices.Add(newContour);

    }

}