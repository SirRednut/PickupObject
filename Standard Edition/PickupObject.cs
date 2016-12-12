using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

    [SerializeField] // Store the GameObject in front of the player for future reference, and pickup Muzzles
    private GameObject objectInFront;

    public  float range = 5.0f;

    // Location to pull picked up objects to
    public GameObject pickupPoint; // Must be assigned, an object in front of player/camera
    public Camera camera; // Must be assigned, main camera

    [SerializeField]
    private GameObject objectSelected;

    public float minForce = 300;
    public float maxForce = 3500;
    public float currForce;

    public int additionFactor = 500;

    public float timeSinceThrow;

    // Use this for initialization
    void Start ()
    {
        currForce = minForce;
	}
	
	// Update is called once per frame
	void Update ()
    {
        DetectObjectInFront();

        if (objectSelected != null)
        {
            PlaceObjectSelectedInFront();
            
        }

        timeSinceThrow++;
    }


    // Call this to select which object is going to be picked up --Callable--
    //--Callable--
    public void SelectObjectInFront()
    {
        if (objectInFront != null && objectSelected == null && timeSinceThrow >= 50)
        {
            objectSelected = objectInFront;
            objectSelected.GetComponent<Rigidbody>().useGravity = false; // Turn off gravity for the object
            objectSelected.GetComponent<Rigidbody>().angularDrag = 5;
            objectSelected.GetComponent<Rigidbody>().drag = 5;
        }
    }


    // Call this to drop the current object --Callable--
    //--Callable--
    public void DeselectCurrentObject()
    {
        if (objectSelected != null)
        {
            objectSelected.GetComponent<Rigidbody>().useGravity = true; // Turn gravity back on to drop it
            SetObjectTransparency(false); // Turn off object's transparency
            objectSelected.GetComponent<Rigidbody>().angularDrag = 0.05f;
            objectSelected.GetComponent<Rigidbody>().drag = 0;
            objectSelected = null; // Empty the variable
        }
    }


    // Keep the selected object in front of the player
    private void PlaceObjectSelectedInFront()
    {
        //objectSelected.transform.position = Vector3.Lerp(objectSelected.transform.position, pickupPoint.transform.position, 5 * Time.deltaTime); // Lerp the position smoothly to in front of the player
        objectSelected.GetComponent<Rigidbody>().MovePosition(Vector3.Slerp(objectSelected.transform.position, pickupPoint.transform.position, 10 * Time.deltaTime));
        //objectSelected.transform.rotation = pickupPoint.transform.rotation; // Match the rotation to the player
        objectSelected.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(objectSelected.transform.rotation, pickupPoint.transform.rotation, 10 * Time.deltaTime));
        SetObjectTransparency(true); // Turns on object transparency
    }


    // Change the appearance of the picked up object so the player can see through it when holding
    private void SetObjectTransparency(bool shouldBeTransparent)
    {
        Material m = objectSelected.GetComponent<MeshRenderer>().material; // Grab the material

        if (shouldBeTransparent)
        {
            // Set the rendering mode to transparent and mess around with a bunch of stuff to make it actually work
            m.SetFloat("_Mode", 3);

            // From here until the next comment, I am simply re-setting the materials variables to the default of a transparent, apparently it doesn't do this automatically on mode change
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.SetInt("_ZWrite", 0);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            m.renderQueue = 3000;
            // ---------- //

            m.SetColor("_Color", new Color(m.color.r, m.color.g, m.color.b, 0.5f));
        }
        else // Set the material to opaque again and restore transparency
        {
            m.SetColor("_Color", new Color(m.color.r, m.color.g, m.color.b, 1.0f));
            m.SetFloat("_Mode", 0);
        }
    }

    // Call this to increase the power of the throw while an item is held --Callable--
    //--Callable--
    public void IncreasePower()
    {
        if (objectSelected != null)
        {
            // If there is force to be had, have it, but slowly
            if (currForce < maxForce)
            {
                currForce += additionFactor * Time.deltaTime;
            }
        }
    }

    // Call this to throw the held object --Callable--
    //--Callable--
    public void Fire()
    {
        if (objectSelected != null) // Make sure something is selected
        {
            objectSelected.GetComponent<Rigidbody>().AddForce(camera.transform.forward * currForce); // Fling that object!
            DeselectCurrentObject(); // Deselect after throw
            timeSinceThrow = 0; // Reset timer
            currForce = minForce; // Reset force
        }
    }

    private void DetectObjectInFront()
    {
        RaycastHit hit; // Create Empty RaycastHit
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Point the raycast from center of the screen forward

        if (Physics.Raycast(ray, out hit)) // If the raycast hits an object, return it's hit
        {
            Transform objectHit = hit.transform; // Store what object was hit using it's transform
            targetPosition = hit.point;

            // Check distance and tag of object hit
            if (Vector3.Distance(camera.transform.position, objectHit.position) <= range && hit.transform.gameObject.tag == "PhysicsObject")
            {
                // If object hit fits criteria, store it in objectInFront
                objectInFront = hit.transform.gameObject;
            }
            else
            {
                // If nothing fits the criteria, set objectInFront to null
                objectInFront = null;
            }
        }
    }
}