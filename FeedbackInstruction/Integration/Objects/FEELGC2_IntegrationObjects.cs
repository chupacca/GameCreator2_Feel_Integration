using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

using MoreMountains.Feedbacks;
using MoreMountains.Tools;

namespace FEELGC2_IntegrationObjects
{
    // Quality of life object for integrating with the FEEL asset
    public class FEELGC2_Components 
    {
        // Components related to the GameObject `TargetObject`
        public MMF_Player cMMFPlayer;

        /*
            * CONSTRUCTOR that should initalize the global variables and 
            * add necessary components to the given GameObject argument
            *
            * TargetObject [GameObject]: Target GameObject that will either have components
            *                            added to you 
        */
        public FEELGC2_Components(GameObject TargetObject)
        {
            // If the `MMF Player` component DOESN'T EXIST, ... 
            if (TargetObject.GetComponent<MMF_Player>() == null)
            {
                // ...ADD the `MMF Player` component
                //    (this will also GET `MMF Player` component)
                cMMFPlayer = TargetObject.AddComponent<MMF_Player>();
            }
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////
    // THESE CLASSES ARE FOR THE TAG SELECTOR IN THE GAME CREATOR 2 INSTRUCTION I MADE //
    // https://answers.unity.com/questions/1378822/list-of-tags-in-the-inspector.html  //
    // I also saved the html file for future reference (in case the page gets deleted) //
    /////////////////////////////////////////////////////////////////////////////////////
    public class TagSelectorAttribute : PropertyAttribute
    {
        public bool UseDefaultTagFieldDrawer = false;
    }
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);

                var attrib = this.attribute as TagSelectorAttribute;

                if (attrib.UseDefaultTagFieldDrawer)
                {
                    property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                }
                else
                {
                    //generate the taglist + custom tags
                    List<string> tagList = new List<string>();
                    tagList.Add("<NoTag>");
                    tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
                    string propertyString = property.stringValue;
                    int index = -1;
                    if (propertyString == "")
                    {
                        //The tag is empty
                        index = 0; //first index is the special <notag> entry
                    }
                    else
                    {
                        //check if there is an entry that matches the entry and get the index
                        //we skip index 0 as that is a special custom case
                        for (int i = 1; i < tagList.Count; i++)
                        {
                            if (tagList[i] == propertyString)
                            {
                                index = i;
                                break;
                            }
                        }
                    }

                    //Draw the popup box with the current selected index
                    index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

                    //Adjust the actual string value of the property based on the selection
                    if (index == 0)
                    {
                        property.stringValue = "";
                    }
                    else if (index >= 1)
                    {
                        property.stringValue = tagList[index];
                    }
                    else
                    {
                        property.stringValue = "";
                    }
                }

                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
}
