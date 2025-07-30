using System.Collections.Generic;
using UnityEngine;
public class CrossHull
{
    public List<List<Vector3>> vertices;
    // Usually we add vertices one by one, if this vertex is in the contour we just set the isClosing to true
    // add the next vertex will be add to the same contour
    public bool isClosing;
    // current contour ID to be enclosed
    public int enclosingId;

    public CrossHull()
    {
        vertices = new List<List<Vector3>>();
        isClosing = false;
        enclosingId = -1; // -1 there is no enclosing contour currently
    }
    public CrossHull(List<List<Vector3>> verts, List<int> ids)
    {
        vertices = verts;
        isClosing = false;
        enclosingId = -1; // -1 there is no enclosing contour currently
    }
    // if is the first vertex to be added (First I mean the first one of the intersection)
    // public void AddVertex(Vector3 v, bool isFirst)
    // {
    //     int index = -1;
    //     if (isFirst)
    //     {
    //         for (int i = 0; i < vertices.Count; i++)
    //         {
    //             // if the vertex is already in the contour, just set the isClosing to true
    //             if (vertices[i].Contains(v))
    //             {
    //                 index = i;
    //                 break;
    //             }
    //         }
    //         if (index != -1)
    //         {
    //             // if the vertex is already in the contour, just set the isClosing to true
    //             isClosing = true;
    //             enclosingId = index;
    //             return;
    //         }
    //         else
    //         {
    //             // new contour
    //             List<Vector3> newContour = new List<Vector3> { v };
    //             vertices.Add(newContour);
    //         }

    //     }
    //     else
    //     {
    //         if (isClosing == true)
    //         { //last vertex belongs to a contour with id = enclosingID
    //           // check if the current vertx belongs to another contour 
    //           // if so, merge the contours
    //           // else just add the vertex to the existing contour

    //             for (int i = 0; i < vertices.Count; i++)
    //             {
    //                 if (vertices[i].Contains(v))
    //                 {
    //                     index = i;
    //                     break;
    //                 }
    //             }
    //             if (index == -1)
    //             {
    //                 // if the vertex does not belong to any contour, just add it to the enclosing contour
    //                 vertices[enclosingId].Add(v);
    //                 return;
    //             }
    //             vertices[enclosingId].AddRange(vertices[index]);
    //             vertices.RemoveAt(index);
    //             return;
    //         }
    //         else
    //         {


    //         }

    //     }

    // }
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
        // if both vertices belong to the same contour do nothing
        // I hope it does not happen, but if it does, we can just ignore it
        if (index1 != -1 && index1 == index2)
        {
            Debug.Log("Both vertices belong to the same contour, this contour will be losed!");
            Debug.Log("Contour ID: " + index1);
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