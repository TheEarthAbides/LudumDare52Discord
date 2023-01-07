// The property drawer class should be placed in an editor script, inside a folder called Editor.

// Tell the RangeDrawer that it is a drawer for properties with the RangeAttribute.
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SpawnManager.GamePlayFrame))]
public class FrameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var catRect = new Rect(position.x, position.y, 30, position.height);
        var pigRect = new Rect(position.x + 35, position.y, 50, position.height);
        var dogRect = new Rect(position.x + 70, position.y, position.width - 90, position.height);
        var ratRect = new Rect(position.x + 105, position.y, 30, position.height);
        var raccoonRect = new Rect(position.x + 140, position.y, 50, position.height);
        var vultureRect = new Rect(position.x + 175, position.y, position.width - 90, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(catRect, property.FindPropertyRelative("Cat"), GUIContent.none);
        EditorGUI.PropertyField(pigRect, property.FindPropertyRelative("Pig"), GUIContent.none);
        EditorGUI.PropertyField(dogRect, property.FindPropertyRelative("Dog"), GUIContent.none);
        EditorGUI.PropertyField(ratRect, property.FindPropertyRelative("Rat"), GUIContent.none);
        EditorGUI.PropertyField(raccoonRect, property.FindPropertyRelative("Raccoon"), GUIContent.none);
        EditorGUI.PropertyField(vultureRect, property.FindPropertyRelative("Vulture"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}