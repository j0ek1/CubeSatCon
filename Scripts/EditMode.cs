using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditMode : MonoBehaviour
{
    public RayToggleR rayTogglerR;
    public RayToggleL rayTogglerL;
    public SceneManage sceneManager;
    public GameObject scaleCanvas;
    public GameObject rotateCanvas;
    public GameObject partsCanvas;
    public GameObject partBG;
    public GameObject xrRig;
    [SerializeField] private Image prevButton;
    [SerializeField] private Image prevPartMenu;
    public Audio sound;
    public PartController[] parts;

    private int scale = 1;
    private bool rotating = false;

    void Update()
    {
        // Enable edit mode and part menu canvas
        scaleCanvas.SetActive(rayTogglerR.editMode);
        rotateCanvas.SetActive(rayTogglerR.editMode);
        partsCanvas.SetActive(rayTogglerL.partMenu);
        partBG.SetActive(rayTogglerL.partMenu);

        partsCanvas.transform.position = new Vector3(partsCanvas.transform.position.x, partsCanvas.transform.position.y, xrRig.transform.position.z + 1f);
        partBG.transform.position = partsCanvas.transform.position;
    }

    public void ScaleX(int _scale)
    {
        // Change scale of parent object
        scale = _scale;
        transform.localScale = new Vector3(scale, scale, scale);

        // For each part that is not fixed to the cubesat frame, change scale individually
        parts = GameObject.FindObjectsOfType<PartController>();
        foreach (PartController part in parts)
        {
            if (!part.isOn)
            {
                part.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }

    public void RotateX(int _rotate)
    {
        if (!rotating)
        {
            rotating = true;
            StartCoroutine(Rotator(Vector3.up * _rotate, 0.3f));
        } 
    }
    IEnumerator Rotator(Vector3 byAngles, float inTime) // Rotate object by angle over time
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = toAngle;
        rotating = false;
    }

    public void ChangeColor(Image button)
    {
        button.color = new Color(0.3349057f, 1, 0.4129106f);
        prevButton.color = Color.white;
        prevButton = button;
    }
    public void ChangeColorP(Image partMenu)
    {
        partMenu.color = new Color(0.3349057f, 1, 0.4129106f);
        if (prevPartMenu != partMenu)
        {
            prevPartMenu.color = Color.white;
        }
        prevPartMenu = partMenu;
    }
}