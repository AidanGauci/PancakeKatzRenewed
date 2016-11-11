using UnityEngine;
using System.Collections;

public class PETER_PlayerCamera : MonoBehaviour
{

    public Transform PlayerModel;
    public Transform CamX;
    public Transform CamY;
    public Transform CamAim;
    public Transform CamTargetAim;
    public Transform CamTargetPos;
    public Transform CamTargetRay;
    public Camera CamMain;

    public float CamMovSpeed;   // Speed at which the Camera positions itself at CamTargetPos
    public float CamRotSpeed;   // Speed at which the Camera rotates itself towards CamTargetAim

    public float mouseSens;     // How much the camera moves from mouse movement
    public float camMoveLimit;  // Maximum amount the camera can move in a frame
    public float camVertLimit;  // Angle from the top and bottom past which the camera cannot move

	// Use this for initialization
	void Start ()
    {
        // Clamps all public variables to reasonable limits
        CamMovSpeed = Mathf.Clamp(CamMovSpeed, 0, 100);
        CamRotSpeed = Mathf.Clamp(CamRotSpeed, 0, 100);
        mouseSens = Mathf.Clamp(mouseSens, 0, 1000);
        camVertLimit = Mathf.Clamp(camVertLimit, 0, 89);
        camMoveLimit = Mathf.Clamp(camMoveLimit, 0, 150);
    }
	
	// Update is called once per frame
	void Update ()
    {

        // Calculates x and y rotation amount based on mouse movement
        float xRot = Mathf.Clamp(Input.GetAxis("Mouse X") * Time.deltaTime * mouseSens, -camMoveLimit, camMoveLimit);
        float yRot = Mathf.Clamp(-Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSens, -camMoveLimit, camMoveLimit);

        // Apply the x rotation to the target
        CamX.transform.Rotate(0, xRot, 0);

        // App the y rotation to the target (for positive y rotation)
        if (yRot > 0)
        {
            if (CamY.transform.eulerAngles.x + yRot <= 90 - camVertLimit
                || CamY.transform.eulerAngles.x + yRot >= 90 - camVertLimit + camMoveLimit + 1)
            {
                CamY.transform.Rotate(yRot, 0, 0);
            }
            else if (CamY.transform.eulerAngles.x <= 90 - camVertLimit)
            {
                CamY.transform.Rotate(90 - camVertLimit - GameObject.Find("CamY").transform.eulerAngles.x, 0, 0);
            }
        }

        // App the y rotation to the target (for negative y rotation)
        else if (yRot < 0)
        {
            if (CamY.transform.eulerAngles.x + yRot >= 270 + camVertLimit
                || CamY.transform.eulerAngles.x + yRot <= 270 + camVertLimit - camMoveLimit - 1)
            {
                CamY.transform.Rotate(yRot, 0, 0);
            }
            else if (CamY.transform.eulerAngles.x >= 270 + camVertLimit)
            {
                CamY.transform.Rotate(270 + camVertLimit - GameObject.Find("CamY").transform.eulerAngles.x, 0, 0);
            }
        }



        // translate camera to match TargetPos
        float frameMov = CamMovSpeed * Time.deltaTime;
        CamTargetRay.transform.position = Vector3.Lerp(CamTargetRay.transform.position, CamTargetPos.transform.position, frameMov);
        CamMain.transform.position = CamTargetRay.position;
        
        // translate camera forward if it's colliding with objects tagged "enviroCamCollider"
        Vector3 camRayDir = CamTargetRay.position - CamAim.position;
        RaycastHit[] camRayHits = Physics.RaycastAll(CamAim.position, camRayDir, camRayDir.magnitude);
        for (int i = 0; camRayHits.Length > i; i++)
        {
            if (camRayHits[i].transform.tag == "enviroCamCollider")
            {
                Debug.Log("fuck a my assa");
                CamMain.transform.position = camRayHits[i].point;
                CamMain.transform.position = Vector3.Lerp(CamMain.transform.position, CamAim.position, 0.1f);
                break;
            }
        }

        // rotate camera to face TargetAim
        float frameRot = CamRotSpeed * Time.deltaTime;
        CamAim.position = Vector3.Lerp(CamAim.position, CamTargetAim.position, frameRot);
        CamMain.transform.LookAt(CamAim.position);



        /// =-=-=-=-=-=
        /// PROBABLY REDUNDENT WHEN ANIMATION IS IMPLEMENTED
        /// =-=-=-=-=-=
        // rotate Playe Model to face same direction as camera
        Vector3 ModelRot = CamAim.position;
        ModelRot.y = PlayerModel.position.y;
        PlayerModel.LookAt(ModelRot);

    }

}
