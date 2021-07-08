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
    public class GlassesSettingsDrawer
    {

        public static void Draw(SerializedProperty glassesSettingsProperty)
        {
            DrawHeadPoseCameraField(glassesSettingsProperty);
            DrawGlassesFOVField(glassesSettingsProperty);
            DrawGlassesMirrorModeField(glassesSettingsProperty);
            DrawGlassesAvailabilityLabel();
        }

        private static void DrawHeadPoseCameraField(SerializedProperty glassesSettingsProperty)
        {
            var headPoseCameraProperty = glassesSettingsProperty.FindPropertyRelative("headPoseCamera");
            bool hasCamera = headPoseCameraProperty.objectReferenceValue;

            if (!hasCamera)
            {
                EditorGUILayout.HelpBox("Head Tracking requires an active Camera assignment. Changing the Camera assignment at runtime is not supported.", MessageType.Warning);
            }
            Rect theCameraRect = EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(headPoseCameraProperty, new GUIContent("Camera"));
            EditorGUILayout.EndHorizontal();
            EditorGUI.LabelField(theCameraRect, new GUIContent("",
                "The Camera driven by the glasses head tracking system."));
        }

        private static void DrawGlassesFOVField(SerializedProperty glassesSettingsProperty)
        {
            ++EditorGUI.indentLevel;

            var overrideFOVProperty = glassesSettingsProperty.FindPropertyRelative("overrideFOV");
            var glassesFOVProperty = glassesSettingsProperty.FindPropertyRelative("customFOV");

            overrideFOVProperty.boolValue = EditorGUILayout.Toggle(new GUIContent("Override FOV"), overrideFOVProperty.boolValue);

            if (overrideFOVProperty.boolValue)
            {
                EditorGUILayout.HelpBox("Overriding this value is not recommended - proceed with caution." +
                    System.Environment.NewLine + System.Environment.NewLine +
                    "Changing the FOV value affects the image projected by the glasses, " +
                    "either cropping/shrinking it while boosting sharpness, or expanding it while reducing sharpness. ",
                    MessageType.Warning);

                glassesFOVProperty.floatValue = EditorGUILayout.Slider(
                    new GUIContent("Field of View", "The field of view of the eye cameras. Higher values trade perceived sharpness for increased projection FOV."),
                    glassesFOVProperty.floatValue, GlassesSettings.MIN_FOV, GlassesSettings.MAX_FOV);
            }

            --EditorGUI.indentLevel;
        }

        private static void DrawGlassesMirrorModeField(SerializedProperty glassesSettingsProperty)
        {
            ++EditorGUI.indentLevel;

            var mirrorModeProperty = glassesSettingsProperty.FindPropertyRelative("glassesMirrorMode");

            mirrorModeProperty.enumValueIndex = EditorGUILayout.Popup("Mirror Mode", mirrorModeProperty.enumValueIndex, mirrorModeProperty.enumDisplayNames);

            --EditorGUI.indentLevel;
        }

        private static void DrawGlassesAvailabilityLabel()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            EditorGUILayout.LabelField($"Glasses: {(Glasses.glassesAvailable ? "Ready" : "Unavailable")}");
        }
    }
}