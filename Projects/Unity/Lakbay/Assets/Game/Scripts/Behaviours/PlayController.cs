/*
 * Date Created: Wednesday, August 25, 2021 11:08 PM
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
using UnityEngine.UI;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public interface IPlayable {
        Controller OnPlay();
        Controller OnPause();

    }

    public class PlayController : Controller {
        public static PlayController instance => GetInstance<PlayController>();

        public virtual void Play(IPlayable playable) {
            var cont = playable.OnPlay();
            cont.timeScale = 1.0f;

        }

        // public virtual void Play(Controller controller) => Play(controller as IPlayable);

        public virtual void Pause(IPlayable playable) {
            var cont = playable.OnPause();
            cont.timeScale = 0.0f;

        }

        // public virtual void Pause(Controller controller) => Pause(controller as IPlayable);

    }

}