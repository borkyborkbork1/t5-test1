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
using UnityEngine;
using UnityEditor;

namespace TiltFive
{
    public class WandSettingsDrawer
    {
        public static void Draw(SerializedProperty wandSettingsProperty)
        {
            var drivenObject = wandSettingsProperty.FindPropertyRelative("drivenObject");
            var controllerIndex = wandSettingsProperty.FindPropertyRelative("controllerIndex");
            bool wandAvailable = drivenObject.objectReferenceValue;

            if (!wandAvailable)
            {
                EditorGUILayout.HelpBox($"Tracking for the {controllerIndex.enumDisplayNames[controllerIndex.enumValueIndex]} Wand requires an active GameObject assignment.", MessageType.Warning);
            }
            Rect primaryWandRect = EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(drivenObject, new GUIContent($"{controllerIndex.enumDisplayNames[controllerIndex.enumValueIndex]} Wand"));
            EditorGUILayout.EndHorizontal();
            EditorGUI.LabelField(primaryWandRect, new GUIContent("",
                "The GameObject driven by the wand tracking system."));

            DrawWandAvailableLabel((ControllerIndex)controllerIndex.enumValueIndex);
        }

        private static void DrawWandAvailableLabel(ControllerIndex controllerIndex)
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }

            Rect statusRect = EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Status: {(Input.GetWandAvailability(controllerIndex) ? "Ready" : "Unavailable")}");
            EditorGUILayout.EndHorizontal();
        }
    }
}