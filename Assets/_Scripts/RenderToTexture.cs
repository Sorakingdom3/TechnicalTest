using System.Collections;
using UnityEngine;

public class RenderToTexture : MonoBehaviour
{
    [SerializeField] Camera _renderCam;

    [SerializeField] Shader _replacementShader;
    [SerializeField] GameObject _displayPlane;
    RenderTexture _renderTexture;

    private void Start()
    {
        _renderTexture = new RenderTexture(256, 256, 24);
        _renderCam.targetTexture = _renderTexture;

        _displayPlane.GetComponent<Renderer>().material.mainTexture = _renderTexture;
    }


    private void Update()
    {
        StartCoroutine(DoRender());



    }

    private IEnumerator DoRender()
    {
        yield return new WaitForEndOfFrame();
        _renderCam.SetReplacementShader(_replacementShader, "");
        _renderCam.Render();
    }

    private void OnDisable()
    {
        _renderCam.targetTexture = null;
        _renderTexture.Release();
        Destroy(_renderTexture);
    }
}
