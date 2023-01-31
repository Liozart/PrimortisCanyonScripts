using UnityEngine;
using ThunderRoad;
using System.Collections;

namespace LioScripts
{
    public class RuneFloat : MonoBehaviour
    {
        float amplitude = 0.2f;
        float frequency = 0.2f;
        
        Vector3 posOffset = new Vector3();
        Vector3 tempPos = new Vector3();
        
        void Start()
        {
            posOffset = transform.position;
        }
        
        void Update()
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            transform.position = tempPos;
        }
    }
}
