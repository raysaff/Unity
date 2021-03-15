using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimationByCurves : MonoBehaviour
{
    [SerializeField] private AnimationCurve _myCurve = null;

    private void OnMouseDown()
    {
        StartCoroutine(AnimationScale());
    }

    IEnumerator AnimationScale()
    {
        for (float i=0; i<1; i+=Time.deltaTime)
        {
            
            transform.localScale = Vector3.one * _myCurve.Evaluate(i);
            yield return null;
        }

        transform.localScale = Vector3.one * _myCurve.Evaluate(1f);
    }
}
