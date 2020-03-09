using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChanger : MonoBehaviour
{
    SpriteRenderer m_renderer;
    // Start is called before the first frame update
    void Start()
    {
        m_renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_renderer.color = new Color(0, 0, 0, 1);

    }
}
