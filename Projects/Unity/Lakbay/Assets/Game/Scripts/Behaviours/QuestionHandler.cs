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

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class QuestionHandler : Controller {
        public ContentPresenter questionContentPresenter;
        public ContentPresenter choiceContentPresenter;
        public GameObject choiceContainer;
        public bool answerOnPick = true;
        public Question question;
        public UnityEvent<QuestionHandler> onAnswer = new UnityEvent<QuestionHandler>();

        public override void Awake() {
            base.Awake();
            // var ta = Game.repository.GetAsset<TextAsset>("Assets/Game/Assets/Documents/sample_question.yaml");
            // question = ta.DeserializeAsYaml<Question>();

        }

        public virtual void Show() {
            Hide();
            questionContentPresenter.Display(question.content);
            foreach(var choice in question.choices) {
                var choicePresenter = Instantiate(choiceContentPresenter, choiceContainer.transform);
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
            questionContentPresenter.Clear();
            choiceContainer.DestroyChildren();

        }

        public virtual void Answer() {
            onAnswer?.Invoke(this);

        }

    }

}