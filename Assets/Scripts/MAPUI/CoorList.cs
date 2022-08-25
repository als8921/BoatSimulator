using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoorList : MonoBehaviour
{

    public void DeleteCoorListBtn()
    {
        Destroy(transform.parent.gameObject);
    }
}
