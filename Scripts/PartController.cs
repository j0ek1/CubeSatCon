using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PartController : MonoBehaviour
{
    public Collider[] current = null;
    public SceneManage sceneManager;
    public PartSO part;
    public bool isOn;
    public GameObject wasOn;

    private Vector3 currentPos = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;
    private float destroyTimer = 0f;

    private void Awake()
    {
        sceneManager = GameObject.FindObjectOfType<SceneManage>();
    }

    private void Update()
    {
        currentPos = transform.position;
        if (currentPos == lastPos && !isOn)
        {
            destroyTimer += Time.deltaTime;
        }
        else
        {
            destroyTimer = 0f;
        }
        lastPos = currentPos;

        if (destroyTimer > 60f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other) // Current collider updated every frame
    {
        if (other.gameObject.CompareTag("ColliderPart"))
        {
            //current = other;
        }
    }

    private void OnTriggerExit(Collider other) // Return current collider variable to null when there are no collisions present
    {
        current = null;
    }

    public void PassData() // On release of the object, send data to scene manager
    {
        current = null;
        current = Physics.OverlapSphere(transform.position, .1f);
        foreach (var col in current)
        {
            Debug.Log("sent: " + col);
        }
        sceneManager.OnReleaseCheck(part, this);

    }

    public void PassZone(int zone) // Pass the zone which should be highlighted when an object is picked up
    {
        sceneManager.EnableZone(zone);
    }

}
