using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;
using System.Linq;

namespace CoDe_A.Lakbay.Modules.UIModule {
    // [ExecuteInEditMode]

    [ExecuteInEditMode]
    public class SliderController : MonoBehaviour {
        public Slider slider;
        public TMP_Text sliderValueText;
        public float steppedValue = 0.0f;
        public string toString = "P0";

        private void OnEnable() {
            this.setUpSlider();

        }

        private void OnValidate() {
            this.setUpSlider();

        }

        private void Awake() {
            this.setUpSlider();

        }

        private void Update() {

        }

        public void onValueChanged(float value) {
            value = this.slider.value;
            value = stepValue(value, steppedValue, this.slider.minValue, this.slider.maxValue);
            // v = Mathf.Max(Mathf.Min(v, this.slider.maxValue), this.slider.minValue);
            value = Mathf.Min(Mathf.Max(value, this.slider.minValue), this.slider.maxValue);
            this.slider.value = value;

            if(this.sliderValueText) {
                string svalue = this.slider.value.ToString(this.toString);
                if(!this.sliderValueText.text.Equals(svalue)) {
                    this.sliderValueText.SetText(svalue);

                }

            }

        }

        public void setUpSlider() {
            if(!this.slider) this.slider = this.GetComponent<Slider>();

            try {
                this.slider.onValueChanged.RemoveListener(this.onValueChanged);

            } catch {}

            this.slider.onValueChanged.AddListener(this.onValueChanged);
            this.slider.onValueChanged.Invoke(this.slider.value);

        }

        private static float stepValue(float value, float steppedValue, params float[] includedValues) {
            steppedValue *= 100;
            value *= 100;
            includedValues = includedValues.Select<float, float>((f) => f * 100).ToArray();

            float rvalue = value;
            float rem = value % steppedValue;

            if(rem != 0) rvalue = value - rem;
            if(new List<float>(includedValues).Contains(value)) rvalue = value;

            return rvalue / 100;

        }

    }

}
