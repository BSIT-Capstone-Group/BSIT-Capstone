/*
 * Date Created: Saturday, September 25, 2021 5:08 AM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Core;
using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Core {
    [Serializable]
    public struct Configuration {
        [Serializable]
        public struct Audio {
            [Serializable]
            public struct Volume {
                private float _value;
                public float value {
                    get => _value;
                    set => Mathf.Clamp(_value, 0.0f, 1.0f);

                }

            }

            private Dictionary<string, Volume> _volumes;
            public Dictionary<string, Volume> volumes {
                get => _volumes;
                set => _volumes = value;

            }

        }

        [Serializable]
        public struct Accessibility {
            private string _language;
            public string language {
                get => _language;
                set {
                    var locales = Helper.GetLocales();
                    var locale = locales.Find(
                        (l) => l.Identifier.CultureInfo.ToString() == value);
                    if(locale) _language = value;

                }

            }
            [YamlIgnore]
            public Locale languageLocale {
                get {
                    var locales = Helper.GetLocales();
                    string lang = language;
                    var locale = locales.Find(
                        (l) => l.Identifier.CultureInfo.ToString() == lang
                    );
                    return locale;

                }

            }

        }

        private Audio _audio;
        public Audio audio {
            get => _audio;
            set => _audio = value;

        }

        private Accessibility _accessibility;
        public Accessibility accessibility {
            get => _accessibility;
            set => _accessibility = value;

        }

    }

}