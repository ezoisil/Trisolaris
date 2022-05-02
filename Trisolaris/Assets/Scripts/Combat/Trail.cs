using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTrail());
    }
    IEnumerator StartTrail()
    {
        yield return new WaitForSeconds(.001f);
        trailRenderer.enabled = true;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
