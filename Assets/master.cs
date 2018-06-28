using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;
public class master : MonoBehaviour {
    drag d;
    StreamWriter actionlog;
    // Use this for initialization
    Object[] allthings;
    float rotx = 0;
    float roty = 0;
    Quaternion fromrot;
    Quaternion torot;
    public GameObject cam;
    int i = 0;
    public Text b;
    public Text objname;
    bool r = false;

    
    void Start () {
        actionlog = new StreamWriter("Assets/action.txt",true);
        actionlog.WriteLine("start");
        //actionlog.Close();
        allthings = Resources.FindObjectsOfTypeAll(typeof(drag));
        print("dsdsd"+allthings.Length);
        
    }
    public void backtologin()
    {
        SceneManager.LoadScene("login", LoadSceneMode.Single);
        
    }
    public void prev()
    {
        i -= 1;
        actionlog.WriteLine("move list backward");
    }
    public void  next()
    {
        i += 1;
        actionlog.WriteLine("move list forward");
    }
    public void rotate()
    {
        r=!r;
        if (r) { b.text = "rotating"; actionlog.WriteLine("change to rotating"); }
        else { b.text = "not rotating"; actionlog.WriteLine("change to moving"); }
    }

    public void center()
    {
        
        cam.transform.LookAt(d.gameObject.transform);
        actionlog.WriteLine("center");
    }
    public void rotatecamleft()
    {

        cam.transform.RotateAround(d.transform.position, cam.transform.up, 1);
        actionlog.WriteLine("rotate cam left");
    }
    public void rotatecamright()
    {
        cam.transform.RotateAround(d.transform.position, cam.transform.up, -1);
        actionlog.WriteLine("rotate cam right");

    }
    public void rotatecamup()
    {
        cam.transform.RotateAround(d.transform.position, cam.transform.right, 1);
        actionlog.WriteLine("rotate cam up");

    }
    public void rotatecamdown()
    {
        cam.transform.RotateAround(d.transform.position, cam.transform.right, -1);
        actionlog.WriteLine("rotate cam down");

    }
    public void camin()
    {
        cam.transform.position += cam.transform.forward * 1;
        actionlog.WriteLine("zoom in");

    }
    public void camout()
    {
        cam.transform.position += cam.transform.forward * -1;
        actionlog.WriteLine("zoom out");


    }
    public void backto()
    {
        d.Reset();


    }

    // Update is called once per frame
    void Update () {
        if (i >= allthings.Length) i = 0;
        else if (i < 0) i = allthings.Length - 1;
        
        for (int x = 0; x < allthings.Length; x++)
        {
            drag temd = (drag)allthings[x];
            temd.theone = false;
        }
        d= (drag)allthings[i];
        d.theone = true;
        objname.text = d.gameObject.name;
        d.rotate = r;


        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            camin();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            camout();
        }
        if (Input.GetMouseButtonDown(1) && Input.GetMouseButtonDown(0))
        {
            actionlog.WriteLine("moving camera");
        }
        else if (Input.GetMouseButtonDown(0)){
            actionlog.WriteLine("drag to move");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            actionlog.WriteLine("drag to rotate");
        }
        if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
        {
            rotx = Input.GetAxis("Mouse X") * 10f * Mathf.Deg2Rad;
            roty = Input.GetAxis("Mouse Y") *10f * Mathf.Deg2Rad;
            cam.transform.RotateAround(d.transform.position, cam.transform.up, -rotx);
            cam.transform.RotateAround(d.transform.position, cam.transform.right, roty);
        }


    }
    private void OnDestroy()
    {
        actionlog.Close();
    }
}
