using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEmitter : MonoBehaviour
{
    [SerializeField] public CustomRenderTexture _texture;
        [SerializeField, Range(1, 16)] public int _stepsPerFrame = 4;

        void Start()
        {
            _texture.Initialize();
        }

        void Update()
        {
            _texture.Update(_stepsPerFrame);
        }
}
