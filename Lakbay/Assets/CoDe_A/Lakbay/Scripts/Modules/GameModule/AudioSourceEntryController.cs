using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.GameModule {
    [System.Serializable]
    public class AudioSourceEntry {
        public string outputAudioMixerGroup = "";
        public List<AudioSource> audioSources = new List<AudioSource>();

    }

    public class AudioSourceEntryController : MonoBehaviour {
        public bool updateOutputAudioMixerGroup = true;
        public List<AudioSourceEntry> audioSourceEntries = new List<AudioSourceEntry>();

        private void Update() {
            if(!updateOutputAudioMixerGroup) return;

            foreach(AudioSourceEntry ase in audioSourceEntries) {
                string mg = ase.outputAudioMixerGroup;

                foreach(AudioSource asrc in ase.audioSources) {
                    asrc.outputAudioMixerGroup = AudioController.gameMixer.FindMatchingGroups(mg)[0];

                }

            }

        }

    }

}
