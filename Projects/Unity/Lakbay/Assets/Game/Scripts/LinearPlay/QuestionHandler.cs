/*
 * Date Created: Thursday, September 16, 2021 7:18 AM
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

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.LinearPlay {
    using Core;
    using Core.UI;

    public class QuestionHandler : Controller {
        public ContentContainer questionContentContainer;
        public ContentContainer choiceContentContainer;
        public GameObject choiceContainer;
        public bool answerOnPick = true;
        protected Question _question;
        public Question question => _question;
        public UnityEvent<QuestionHandler> onAnswer = new UnityEvent<QuestionHandler>();

        public override void Awake() {
            base.Awake();
            // var ta = Game.repository.GetAsset<TextAsset>("Assets/Game/Assets/Documents/sample_question.yaml");
            // question = ta.DeserializeAsYaml<Question>();

        }

        public virtual void Show(Question question) {
            Hide();
            _question = question;
            questionContentContainer.Display(question.content);
            foreach(var choice in question.choices) {
                var choicePresenter = Instantiate(choiceContentContainer, choiceContainer.transform);
                choicePresenter.Display(choice.content);

                var button = choicePresenter.GetComponentInChildren<Button>();
                if(button) {
                    Func<Choice, UnityAction> addAnswer = (c) => () => {
                        if(answerOnPick) {
                            question.Answer(choice);
                            Answer();

                        } else {
                            question.AddAnswer(c);

                        }

                    };
                    button.onClick.AddListener(addAnswer(choice));

                }

            }

        }

        public virtual void Hide() {
            questionContentContainer.Clear();
            choiceContainer.DestroyChildren();

        }

        public virtual void Answer() {
            onAnswer?.Invoke(this);

        }

    }

}