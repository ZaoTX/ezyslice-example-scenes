using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class SliceManager : MonoBehaviour
{
    public GameObject parentObj;
    public GameObject slicePlane;
    List<GameObject> getChildren() {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parentObj.transform)
        {
            if (child.gameObject.activeSelf)
            {
                children.Add(child.gameObject);
            }
        }

        return children;
    }

    void CutObject(GameObject victim) {
        if (victim != null) {
            SlicedHull hull = victim.Slice(slicePlane.transform.position, slicePlane.transform.up);
            if (hull != null)
            {
                GameObject upperHull = hull.CreateUpperHull(victim);
                GameObject lowerHull = hull.CreateLowerHull(victim);

                victim.SetActive(false);
            }
        }
    }

    void CutAllChildren() { 
        List<GameObject> children = getChildren();
        foreach (GameObject victim in children)
        {
            CutObject(victim);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CutAllChildren();
        }
    }
}
