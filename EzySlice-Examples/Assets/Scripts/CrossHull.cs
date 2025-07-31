using System.Collections.Generic;
using UnityEngine;
public class CrossHull
{
    public List<List<Vector3>> vertices;
    // Usually we add vertices one by one, if this vertex is in the contour we just set the isClosing to true
    // add the next vertex will be add to the same contour
    public List<bool> isClosing;
    // current contour ID to be enclosed
    public int enclosingId;
    public List<int> notEnClosedContoursID;

    public CrossHull()
    {
        vertices = new List<List<Vector3>>();
        isClosing = new List<bool>();
        notEnClosedContoursID = new List<int>();
        enclosingId = -1; // -1 there is no enclosing contour currently
    }
    public CrossHull(List<List<Vector3>> verts, List<int> ids)
    {
        vertices = verts; 
        enclosingId = -1; // -1 there is no enclosing contour currently
    }
    void AddContour(List<Vector3> contour)
    {
        vertices.Add(contour);
        isClosing.Add(false);
    }
    public void SelfCheck()
    {
        for (int i = 0; i < vertices.Count; i++)
        {
            if (!isClosing[i])
            {
                notEnClosedContoursID.Add(i);
                Debug.Log("Contour " + i + " is not closed!");
            }
        } 
    }
     
    //before adding vertices, check if the vertices belong to the existing contour
    //Here we assume each time we add only two vertices
    public void AddVertices(Vector3 v1, Vector3 v2)
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
        // if both vertices belong to the same contour then the contour will be closed, good!
        if (index1 != -1 && index1 == index2)
        { 
            isClosing[index1] = true; // set the contour as closed
            return;
        }
        //if both vertices belong to two distinct contours, merge them
        if (index1 != -1 && index2 != -1 && index1 != index2)
        {
            int keep = Mathf.Min(index1, index2);
            int merge = Mathf.Max(index1, index2);

            vertices[keep].AddRange(vertices[merge]);
            vertices.RemoveAt(merge);
            isClosing.RemoveAt(merge); 
            return;
        }
        //if one of the vertices belongs to the existing contour, add the other vertex to it
        if (index1 != -1)
        {
            vertices[index1].Add(v2);
            
        }
        //if one of the vertices belongs to the existing contour, add the other vertex to it
        else if (index2 != -1)
        {
            vertices[index2].Add(v1);
             
        }
        else
        {// otherwise just add a new contour
            List<Vector3> newContour = new List<Vector3> { v1, v2 };
            AddContour(newContour);
        }
    }

}