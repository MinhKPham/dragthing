using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
public class drag : MonoBehaviour {
    public float d = 0f;
    public bool rotate=false;
    public bool theone = true;
    public float rotspeed = 3f;
    public Text ro;
    public Text po;
    float rotx = 0;
    float roty = 0;
    float rotz = 0;
    Vector3 lastpo;
    Quaternion lastro;

    Quaternion fromrot;
    Quaternion torot;
    // Use this for initialization
    void Start () {
        lastpo = transform.position;
        lastro = transform.rotation;
        
    }
    

    // Update is called once per frame
    void Update () {
        if (!theone)
        {
            lastpo = transform.position;
            lastro = transform.rotation;
        }
        if (theone)
        {
            ro.text = lastro.ToString();
            po.text = lastpo.ToString();
        }
        

    }
    public void Reset()
    {
        transform.position=lastpo;
        transform.rotation=lastro;
    }
    void increasez()
    {
        rotz += 1;
    }
    void dereasez()
    {
        rotz -= 1;
    }
    
    private void OnMouseOver()
    {
        
        //actionlog.Close();
        if (!theone) return;
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = (Vector3.Project(Camera.main.transform.position- transform.position, Camera.main.transform.forward)).magnitude;

        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            
            rotx = Input.GetAxis("Mouse X") * rotspeed * Mathf.Deg2Rad;
            roty = Input.GetAxis("Mouse Y") * rotspeed * Mathf.Deg2Rad;
            //fromrot = transform.rotation;
            //torot = Quaternion.Euler(roty, rotx, rotz);
            //transform.rotation = Quaternion.Lerp(fromrot, torot, Time.deltaTime * 100);
            transform.RotateAround(Camera.main.transform.up, -rotx);
            transform.RotateAround(Camera.main.transform.right, roty);
            StreamWriter actionlog = new StreamWriter("Assets/action.txt", true);
            


        }
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            
            
            transform.position = objPosition;
            print(objPosition);
        }

    }
    
}
