using UnityEngine;
using UnityEditor;
using System.Collections;

public class CamFollow : MonoBehaviour {


    public Transform Wall;
    private float WallWidth;
    private int Rows;
    private float Space;

    void Start()
    {
        this.Rows = PlayerPrefs.GetInt("Rows");
        WallWidth = Wall.localScale.y;
        Space = Mathf.Tan(Mathf.Abs(-38) * Mathf.PI / 180) * Camera.main.transform.position.z;
    }
    void Update()
    {
        Camera.main.fieldOfView = Rows * 3;
        Camera.main.transform.position = new Vector3(((Rows - 1) * WallWidth)/2f , -((Rows - 1) * WallWidth)/2f + Space, -554);
    }

}
