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
        public static readonly float MIN_VOLUME = -80.0f;
        public static readonly float MAX_VOLUME = 20.0f;

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
                AsyncOperationHandle<AudioMixer> h = DatabaseModule.DatabaseController.loadAsset<AudioMixer>("Audio Mixers/Game.mixer");
                await h.Task;

                gameMixer = h.Result;

            }

        }

        public static void setVolume(string name, float value) {
            float rvalue = (value * 100.0f) - (100.0f - MAX_VOLUME);
            gameMixer.SetFloat(name, rvalue);

        }

        public static void mute(string name) {
            if(isMuted(name)) return;
            lastVolumes[name] = getVolume(name);
            
            setVolume(name, 0.0f);

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

            return (lastVolume + (100 - MAX_VOLUME)) / 100.0f;

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

    }

}
