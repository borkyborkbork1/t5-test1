/*
 * Copyright (C) 2020 Tilt Five, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TiltFive;
using TiltFive.Logging;

namespace TiltFive
{

    /// <summary>
    /// The Tilt Five manager.
    /// </summary>
    [DisallowMultipleComponent]
    public class TiltFiveManager : TiltFive.SingletonComponent<TiltFiveManager>
    {
        /// <summary>
        /// The scale conversion runtime configuration data.
        /// </summary>
        public ScaleSettings scaleSettings;

        /// <summary>
        /// The game board runtime configuration data.
        /// </summary>
        public GameBoardSettings gameBoardSettings;

        /// <summary>
        /// The glasses runtime configuration data.
        /// </summary>
		public GlassesSettings glassesSettings;

        /// <summary>
        /// The wand runtime configuration data for the primary wand.
        /// </summary>
        public WandSettings primaryWandSettings;

        /// <summary>
        /// The wand runtime configuration data for the secondary wand.
        /// </summary>
        public WandSettings secondaryWandSettings;

        /// <summary>
        /// The log settings.
        /// </summary>
		public LogSettings logSettings = new LogSettings();

#if UNITY_EDITOR
        /// <summary>
        /// <b>EDITOR-ONLY</b> The editor settings.
        /// </summary>
		public EditorSettings editorSettings = new EditorSettings();

#endif
        /// <summary>
        /// Awake this instance.
        /// </summary>
		void Awake()
        {
            // Apply log settings
            Log.LogLevel = logSettings.level;
            Log.TAG = logSettings.TAG;

            // adjust theCamera's far clip plane wrt the contentScale (e.g. theCamera translation).
            glassesSettings.headPoseCamera.farClipPlane /= (scaleSettings.physicalMetersPerWorldSpaceUnit * gameBoardSettings.gameBoardScale);

        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        /// <returns>On complete.</returns>
		IEnumerator Start()
        {
            yield return StartCoroutine("CommitBuffers");
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
		void Update()
        {
            if (!Glasses.Validate(glassesSettings))
			{
                Glasses.Reset(glassesSettings);
            }

            Glasses.Update(glassesSettings, scaleSettings, gameBoardSettings);
            Wand.Update(primaryWandSettings, scaleSettings, gameBoardSettings);
            Wand.Update(secondaryWandSettings, scaleSettings, gameBoardSettings);
            Input.Update();
        }

        /// <summary>
        /// Called when the GameObject is enabled.
        /// </summary>
		void OnEnable()
        {
            Glasses.Reset(glassesSettings);
        }

        /// <summary>
        /// Commits the dat at end fo frame for rendering the framebuffer.
        /// </summary>
        /// <returns>On complete.</returns>
        IEnumerator CommitBuffers()
        {
            yield return null;
            WaitForEndOfFrame cachedWaitForEndOfFrame = new WaitForEndOfFrame();

            while (true)
            {
                yield return cachedWaitForEndOfFrame;
            }
        }

#if UNITY_EDITOR

        /// <summary>
        /// <b>EDITOR-ONLY</b>
        /// </summary>
		void OnValidate()
        {
            Log.LogLevel = logSettings.level;
            Log.TAG = logSettings.TAG;

            scaleSettings.contentScaleRatio = Mathf.Clamp(scaleSettings.contentScaleRatio, ScaleSettings.MIN_CONTENT_SCALE_RATIO, float.MaxValue);
        }

        /// <summary>
        /// Draws Gizmos in the Editor Scene view.
        /// </summary>
		void OnDrawGizmos()
        {
            if (gameBoardSettings.currentGameBoard != null)
            {
                gameBoardSettings.currentGameBoard.DrawGizmo(scaleSettings, gameBoardSettings);
            }
        }

#endif
    }

}
