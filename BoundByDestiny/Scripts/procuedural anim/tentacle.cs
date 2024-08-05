using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRend;
    private GameObject lineObj;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;
    private GameObject anu;

    public Transform TargetDir;
    public float targetDist;
    public float SmoothSpeed;
    public float trailSpeed;
    

    public float WiggleSpeed;
    public float WiggleMagnitude;
    public Transform WiggleDir;

    // Start is called before the first frame update
    void Start()
    {
        //lineObj = lineRend.AddComponent<LineRenderer>();
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    // Update is called once per frame
    void Update()
    {
        WiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * WiggleSpeed) * WiggleMagnitude);

        segmentPoses[0] = TargetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + TargetDir.right * targetDist, ref segmentV[i], SmoothSpeed + i/trailSpeed);
        }
        
            lineRend.SetPositions(segmentPoses);    
        
        
    }
}
