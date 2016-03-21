using UnityEngine;
using System.Collections;

public class SetRenderQueue : MonoBehaviour {

    public int renderQueue;

    void Start() {
        GetComponent<Renderer>().sharedMaterial.renderQueue = renderQueue;
    }
}
