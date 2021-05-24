using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.GameModule {
    public class AudioController : MonoBehaviour {
        public static readonly string MASTER_VOLUME_PARAMETER = "masterVolume";
        public static readonly string MUSIC_VOLUME_PARAMETER = "musicVolume";
        public static readonly string SOUND_VOLUME_PARAMETER = "soundVolume";
        // public static readonly float MULTIPLIER = 30.0f;
        public static readonly float MIN_VOLUME = -60.0f;
        public static readonly float MAX_VOLUME = 0.0f;
        public static readonly float VOLUME_LENGTH = MAX_VOLUME - MIN_VOLUME;

        public static Dictionary<string, float> lastVolumes = new Dictionary<string, float>{
            {MASTER_VOLUME_PARAMETER, 0.0f},
            {MUSIC_VOLUME_PARAMETER, 0.0f},
            {SOUND_VOLUME_PARAMETER, 0.0f},
        };

        public static AudioMixer gameMixer;

        // private async Task Awake() {
        //     await setUp();

        // }

        public static async Task setUp() {
            if(!gameMixer) {
                AsyncOperationHandle<AudioMixer> h = GameModule.DatabaseController.loadAsset<AudioMixer>("Audio Mixers/Game.mixer");
                await h.Task;

                gameMixer = h.Result;

            }

        }

        public static void setLastVolume(string name) => lastVolumes[name] = getVolume(name);
        public static void setMasterLastVolume() => setLastVolume(MASTER_VOLUME_PARAMETER);
        public static void setMusicLastVolume() => setLastVolume(MUSIC_VOLUME_PARAMETER);
        public static void setSoundLastVolume() => setLastVolume(SOUND_VOLUME_PARAMETER);

        public static void setVolume(string name, float value) {
            setVolume(name, value, true);

        }

        public static void setVolume(string name, float value, bool setLastVolume) {
            float rvalue = (VOLUME_LENGTH * value) - VOLUME_LENGTH;
            gameMixer.SetFloat(name, rvalue);
            if(setLastVolume) AudioController.setLastVolume(name);

        }

        public static void mute(string name) {
            if(isMuted(name)) return;
            setLastVolume(name);
            
            setVolume(name, 0.0f, false);

        }

        public static void unmute(string name) {
            if(!isMuted(name)) return;
            
            setVolume(name, lastVolumes[name]);

        }

        public static bool isMuted(string name) {
            return getVolume(name) <= 0.0f;

        }

        public static float getVolume(string name) {
            float lastVolume = 0.0f;
            gameMixer.GetFloat(name, out lastVolume);

            return (lastVolume + VOLUME_LENGTH) / VOLUME_LENGTH;

        }

        public static void setMasterVolume(float value) {
            setVolume(MASTER_VOLUME_PARAMETER, value);

        }

        public static void setMusicVolume(float value) {
            setVolume(MUSIC_VOLUME_PARAMETER, value);

        }

        public static void setSoundVolume(float value) {
            setVolume(SOUND_VOLUME_PARAMETER, value);

        }

        public static void muteMaster() { mute(MASTER_VOLUME_PARAMETER); }
        public static void muteMusic() { mute(MUSIC_VOLUME_PARAMETER); }
        public static void muteSound() { mute(SOUND_VOLUME_PARAMETER); }

        public static void unmuteMaster() { unmute(MASTER_VOLUME_PARAMETER); }
        public static void unmuteMusic() { unmute(MUSIC_VOLUME_PARAMETER); }
        public static void unmuteSound() { unmute(SOUND_VOLUME_PARAMETER); }

        public static float getMasterVolume() { return getVolume(MASTER_VOLUME_PARAMETER); }
        public static float getMusicVolume() { return getVolume(MUSIC_VOLUME_PARAMETER); }
        public static float getSoundVolume() { return getVolume(SOUND_VOLUME_PARAMETER); }

        public static void play(AudioClip audioClip) {
            string name = $"Audio Source - {audioClip.GetInstanceID()}";
            GameObject go = GameObject.Find(name);
            AudioSource asrc = null;

            if(!go) {
                go = new GameObject(name);
                asrc = go.AddComponent<AudioSource>();
                asrc.playOnAwake = false;
                asrc.clip = audioClip;

            } else {
                asrc = go.GetComponent<AudioSource>();

            }

            asrc.Play();
            Destroy(go.gameObject, asrc.clip.length);

        }

    }

}
