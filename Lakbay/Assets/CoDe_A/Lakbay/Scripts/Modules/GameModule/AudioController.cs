using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.GameModule {
    public class AudioController : DatabaseController {

        public static readonly new string ADDRESSABLE_LABEL = "Audios";
        public static readonly string GAME_MIXER_ADDRESS = "Audio Mixers/Game.mixer";
        public static readonly string MASTER_NAME = "Master";
        public static readonly string MUSIC_NAME = "Music";
        public static readonly string SOUND_NAME = "Sound";
        public static readonly string MASTER_AUDIO_MIXER_GROUP = $"{MASTER_NAME}";
        public static readonly string MUSIC_AUDIO_MIXER_GROUP = $"{MASTER_NAME}/{MUSIC_NAME}";
        public static readonly string SOUND_AUDIO_MIXER_GROUP = $"{MASTER_NAME}/{SOUND_NAME}";
        public static readonly string MASTER_VOLUME_PARAMETER = "masterVolume";
        public static readonly string MUSIC_VOLUME_PARAMETER = "musicVolume";
        public static readonly string SOUND_VOLUME_PARAMETER = "soundVolume";
        // public static readonly float MULTIPLIER = 30.0f;
        public static readonly float MIN_VOLUME = -60.0f;
        public static readonly float MAX_VOLUME = 0.0f;
        public static readonly float VOLUME_LENGTH = MAX_VOLUME - MIN_VOLUME;

        public static AsyncOperationHandle<IList<AudioClip>> handle;
        private static Coroutine _loadCoroutine = null;
        private static float _loadingProgress = 0.0f;
        public static float loadingProgress => _loadingProgress;

        public static Dictionary<string, float> lastVolumes = new Dictionary<string, float>{
            {MASTER_VOLUME_PARAMETER, 0.0f},
            {MUSIC_VOLUME_PARAMETER, 0.0f},
            {SOUND_VOLUME_PARAMETER, 0.0f},
        };

        public static Dictionary<string, AudioClip> audios = new Dictionary<string, AudioClip>();

        public static AudioMixer gameMixer;

        private void Update() {

        }

        public static new async Task setUp() {
            if(!gameMixer) {
                AsyncOperationHandle<AudioMixer> h = GameModule.DatabaseController.loadAsset<AudioMixer>(GAME_MIXER_ADDRESS);
                await h.Task;

                gameMixer = h.Result;

            }

        }

        public static IEnumerator loadGameMixer(
            Action<int, int, float, AudioMixer> onProgress,
            Action<float, Dictionary<string, AudioMixer>> onComplete
        ) {
            yield return loadAssets<AudioMixer>(AudioController.ADDRESSABLE_LABEL, onProgress, onComplete);

        }

        public static IEnumerator loadAssets(
            Action<int, int, float, AudioClip> onProgress,
            Action<float, Dictionary<string, AudioClip>> onComplete
        ) {
            yield return loadAssets<AudioClip>(AudioController.ADDRESSABLE_LABEL, onProgress, onComplete);

        }

        public static void setLastVolume(string name) => lastVolumes[name] = getVolume(name);
        public static void setMasterLastVolume() => setLastVolume(MASTER_VOLUME_PARAMETER);
        public static void setMusicLastVolume() => setLastVolume(MUSIC_VOLUME_PARAMETER);
        public static void setSoundLastVolume() => setLastVolume(SOUND_VOLUME_PARAMETER);

        public static void setVolume(string name, float value) {
            setVolume(name, value, true);

        }

        public static void setVolume(string name, float value, bool setLastVolume) {
            // float rvalue = (VOLUME_LENGTH * value) - VOLUME_LENGTH;
            float rvalue = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20.0f;
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

            return Mathf.Max((lastVolume + VOLUME_LENGTH) / VOLUME_LENGTH, 0.0001f);

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

        public static void play(AudioClip audioClip, string outputAudioMixerGroup) {
            string name = $"Audio Source - {audioClip.GetInstanceID()}";
            GameObject go = GameObject.Find(name);
            AudioSource asrc = null;
            AudioSourceEntryController asec = null;

            if(!go) {
                go = new GameObject(name);
                asrc = go.AddComponent<AudioSource>();
                asec = go.AddComponent<AudioSourceEntryController>();
                asrc.playOnAwake = false;
                asrc.clip = audioClip;

            } else {
                asrc = go.GetComponent<AudioSource>();
                asec = go.GetComponent<AudioSourceEntryController>();

            }
            
            AudioSourceEntry ase = new AudioSourceEntry();
            ase.outputAudioMixerGroup = outputAudioMixerGroup;
            ase.audioSources.Add(asrc);

            asrc.Play();

        }

        public static void play(AudioClip audioClip) => play(audioClip, SOUND_AUDIO_MIXER_GROUP);

    }

}
