using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using CoDe_A.Lakbay.Modules.GameModule;
using Coffee.UIExtensions;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.UIModule {
    public class UIController : MonoBehaviour {
        [Header("Indicators")]
        public TMP_Text modeText;
        public TMP_Text stageText;
        public TMP_Text coinText;
        public TMP_Text hintText;
        public TMP_Text lifeText;
        public Slider fuelBar;
        public Slider progressBar;

        public Image lifeImage;
        public List<Sprite> lifeSprites = new List<Sprite>();

        [Header("Controls")]
        public Button leftSteer;
        public Button rightSteer;
        public Button accelerate;
        public Button brake;

        [Header("Question")]
        public TMP_Text timeText;
        public GameObject imagesPanel;
        public GameObject imageButton;
        public GameObject maximizedImagePanel;
        public Image maximizedImage;
        public GameObject questionPanel;
        public TMP_Text questionText;
        public GameObject choicesPanel;
        public GameObject choiceButton;
        public Button hintButton;

        [Header("Post Stage")]
        public GameObject postStagePanel;
        public TMP_Text scoreText;
        public TMP_Text scoreDescriptionText;
        public TMP_Text levelText;
        public GameObject starsPanel;
        public Button nextStageButton;
        public Button viewAssessmentButton;
        public Button endGameButton;

        [Header("Post Linear Play")]
        public GameObject postLinearPlayPanel;
        public TMP_Text totalTimeText;
        public Button freeRoamButton;

        [Header("Notifications")]
        public Utilities.Notification notification;
        public Utilities.Notification pointNotification;

        [Header("Controllers")]
        public VehicleModule.VehicleController vehicleController;
        public PlayerModule.PlayerController playerController;

        [Header("Particles")]
        public UIParticle coinParticle;
        public UIParticle hintParticle;
        public UIParticle lifeParticle;

        [Header("Audios")]
        public AudioClip coinSound;
        public AudioClip hintSound;
        public AudioClip lifeSound;
        public AudioClip lifeBreakSound;

        [Header("Events")]
        public UnityEvent<UIController, float> onFuelTopUp = new UnityEvent<UIController, float>();

        private void Awake() {
            this.playerController.onCoinChange.AddListener(this.onPlayerControllerCoinChange);
            this.playerController.onHintChange.AddListener(this.onPlayerControllerHintChange);
            this.playerController.onLifeChange.AddListener(this.onPlayerControllerLifeChange);
            this.playerController.onLifeIntegrityChange.AddListener(this.onPlayerControllerLifeIntegrityChange);

            this.vehicleController.onFuelChange.AddListener(this.onVehicleControllerFuelChange);

            Image hi = this.progressBar.handleRect.GetComponent<Image>();
            Image fi = this.progressBar.fillRect.GetComponent<Image>();
            Image li = this.stageText.transform.parent.GetComponent<Image>();
            string hc = "", fc = "", lc = "";

            if(GameController.currentModeType == GameController.Mode.NON_PRO) {
                fc = GameController.NON_PRO_COLORS[0]; hc = GameController.NON_PRO_COLORS[1];
                lc = GameController.NON_PRO_COLORS[1];

            } else {
                fc = GameController.PRO_COLORS[0]; hc = GameController.PRO_COLORS[1];
                lc = GameController.PRO_COLORS[1];

            }

            fi.color = Utilities.Color.create(fc);
            hi.color = Utilities.Color.create(hc);
            li.color = Utilities.Color.create(lc);

            // if(GameController.linearPlayStage != null) {
            //     GameController.linearPlayStage.localizedSetFile.AssetChanged += (ta) => {
            //         this.setQuestionText()

            //     };

            // }

        }

        private void Update() {
            float rf = SimpleInput.GetAxis("RefillFuel");
            this.onFuelTopUp.Invoke(this, rf);

            if(GameModule.GameController.currentModeData != null) {
                this.modeText.SetText(GameModule.GameController.currentModeType == GameModule.GameController.Mode.NON_PRO ? "Non-Pro" : "Pro");

                if(GameModule.GameController.currentLinearPlayLevel != null) {
                    int i = GameModule.GameController.currentModeData.linearPlayData.levels.IndexOf(GameModule.GameController.currentLinearPlayLevel);
                    int l = GameModule.GameController.currentModeData.linearPlayData.levels.Count;
                    this.stageText.SetText($"{i + 1} / {l}");

                }
            
            }

        }
        
        public void onPlayerControllerCoinChange(PlayerModule.PlayerController pc, float value) {
            this.coinText.SetText(pc.player.coin.ToString("N0"));
            this.coinParticle.Play();
            GameModule.AudioController.play(this.coinSound);

        }

        public void onPlayerControllerHintChange(PlayerModule.PlayerController pc, float value) {
            this.hintText.SetText(pc.player.hint.ToString("N0"));
            this.hintParticle.Play();
            AudioSource.PlayClipAtPoint(this.hintSound, Camera.main.transform.position);
            GameModule.AudioController.play(this.hintSound);

        }

        public void onPlayerControllerLifeChange(PlayerModule.PlayerController pc, float value) {
            this.lifeText.SetText(pc.player.life.ToString("N0"));
            this.lifeParticle.Play();
            GameModule.AudioController.play(this.lifeSound);

        }

        public void onPlayerControllerLifeIntegrityChange(PlayerModule.PlayerController pc, float value) {
            // this.lifeText.SetText(pc.player.life.ToString("N0"));
            // this.lifeParticle.Play();
            // this.lifeBreakSound.Play();

            float p = float.Parse((1.0f - (pc.player.lifeIntegrity / 3.0f)).ToString("N2"));
            float bp = float.Parse((1.0f / this.lifeSprites.Count).ToString("N2"));
            foreach(Sprite sprite in this.lifeSprites) {
                float cp = float.Parse((bp * (this.lifeSprites.IndexOf(sprite))).ToString("N2"));
                print($"{p} <= {cp}");

                if(p <= cp) {
                    this.lifeImage.sprite = sprite;
                    break;

                }

            }

            print($"life integrity: {pc.player.lifeIntegrity}");

            GameModule.AudioController.play(this.lifeBreakSound);

        }

        public void onVehicleControllerFuelChange(VehicleModule.VehicleController vc, float value) {
            this.fuelBar.maxValue = 1.0f;
            this.fuelBar.minValue = 0.0f;
            this.fuelBar.value = Mathf.Lerp(this.fuelBar.value, vc.vehicle.fuel / vc.vehicle.maxFuel, 1.0f);

            Image img = this.fuelBar.transform.Find("Fill Area").Find("Fill").GetComponent<Image>();
            
            Color c = new Color();
            if(this.fuelBar.value <= 0.25f) {
                c = new Color(255, 0, 0);

            } else if(this.fuelBar.value <= 0.5f) {
                ColorUtility.TryParseHtmlString("#DAA40A", out c);

            } else if(this.fuelBar.value <= 1.0f) {
                c = new Color(0, 255, 0);

            }

            img.color = c;
            // 

        }

        public void setQuestionText(QuestionModule.Question question) {
            this.questionText.SetText(question.text.Trim());

        }

        public GameObject[] setChoiceButtons(params QuestionModule.Choice[] choices) {
            Utilities.Helper.destroyChildren(this.choicesPanel.transform);

            List<GameObject> gameObjects = new List<GameObject>();
            foreach(QuestionModule.Choice choice in choices) {
                GameObject cb = Instantiate<GameObject>(this.choiceButton, this.choicesPanel.transform);
                gameObjects.Add(cb);
                TMP_Text t = cb.transform.GetChild(0).GetComponent<TMP_Text>();

                t.SetText(choice.text.Trim());

                if(Debug.isDebugBuild && choice.correct) {
                    t.color = new Color(0, 255, 0);

                }

            }

            return gameObjects.ToArray();

        }

        public GameObject[] setImageButtons(params Sprite[] sprites) {
            this.imagesPanel.SetActive(false);
            Utilities.Helper.destroyChildren(this.imagesPanel.transform);

            if(sprites.Length > 0) this.imagesPanel.SetActive(true);

            List<GameObject> gameObjects = new List<GameObject>();

            foreach(Sprite sprite in sprites) {
                GameObject go = UIController.Instantiate<GameObject>(this.imageButton, this.imagesPanel.transform);
                gameObjects.Add(go);
                
                go.GetComponent<Image>().sprite = sprite;

            }

            return gameObjects.ToArray();

        }

        public GameObject[] setImageButtons(params string[] paths) {
            List<Sprite> sprites = new List<Sprite>();

            foreach(string path in paths) {
                string npath = $"Images/{path}";
                // AsyncOperationHandle<Sprite> h = GameModule.DatabaseController.loadAsset<Sprite>(npath);
                // await h.Task;
                // sprites.Add(
                //     h.Result
                // );
                sprites.Add(
                    ImageController.images[npath]
                );

            }

            return this.setImageButtons(sprites.ToArray());

        }

        public void setMaximizedImage(Sprite sprite) {
            this.maximizedImagePanel.SetActive(false);

            if(sprite) this.maximizedImagePanel.SetActive(true);

            this.maximizedImage.sprite = sprite;

        }

    }

}
