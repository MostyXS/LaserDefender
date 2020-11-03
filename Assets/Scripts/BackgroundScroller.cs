using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollingSpeed = 0.5f;
    Material MyMaterial;
    Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        MyMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollingSpeed); 
    }

    // Update is called once per frame
    void Update()
    {
        MyMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
