using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    #region SINGLETON_IMPLEMENTATION
    static Reposition Instance;
    private void Awake()
    {
        Instance = this;
    }
    public static Reposition GetInstance()
    {
        return Instance;
    }
    #endregion

    public void RepositionInFront()
    {
        var gazeOrigin = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        this.transform.position = new Vector3(gazeOrigin.x + 2*gazeDirection.x, gazeOrigin.y + 2*gazeDirection.y, gazeOrigin.z + 2*gazeDirection.z);
    }
}
