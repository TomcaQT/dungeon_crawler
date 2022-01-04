using Items;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoostingItemData))]
public class BoostingItemDataEditor : Editor
{
    string[] _choices = new [] { "Hp","Energy","Damage","Resistency","AttackSpeed", "BulletSpeed","Money"};
    int _choiceIndex = 0;

    public override void OnInspectorGUI ()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        EditorGUILayout.LabelField("Available stats to boost");
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
        var someClass = target as BoostingItemData;
        // Update the selected choice in the underlying object
        someClass.ToBoost = _choices[_choiceIndex];
        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}

