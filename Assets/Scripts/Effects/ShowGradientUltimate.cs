using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class ShowGradientUltimate : MonoBehaviour {

    public Gradient color;
    public float lastUltTime;


    private Material material;

	void Start () {
        material = GetComponent<Renderer>().material;    
	}
	
	void Update () {
	
	}
}
