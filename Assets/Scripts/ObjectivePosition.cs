using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MarkerHolder>().AddObjectiveMarker(this);
    }


}
