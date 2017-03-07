using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [ExecuteInEditMode]
    public class PostEffect : MonoBehaviour
    {
        public Shader shader; 
        Material effect;

        [Range(-1f, 1f)]
        public float hue;
        [Range(-1f, 1f)]
        public float value;
        [Range(-1f, 1f)]
        public float saturation;
        [Range(0, 1f)]
        public float vigneting;
        [Range(0f, 1f)]
        public float rampIntensity;
        public Texture ramp;

        private void OnEnable()
        {
            effect = new Material(shader);
            effect.SetFloat("_Hue", hue);
            effect.SetFloat("_Value", value);
            effect.SetFloat("_Saturation", saturation);
            effect.SetFloat("_VignetingIntensity", vigneting);
            effect.SetFloat("_RampIntensity", rampIntensity);
            effect.SetTexture("_ColorRamp", ramp);
        }

        private void Update()
        {
            effect.SetFloat("_Hue", hue);
            effect.SetFloat("_Value", value);
            effect.SetFloat("_Saturation", saturation);
            effect.SetFloat("_VignetingIntensity", vigneting);
            effect.SetFloat("_RampIntensity", rampIntensity);
            effect.SetTexture("_ColorRamp", ramp);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, effect);
        }
    }
