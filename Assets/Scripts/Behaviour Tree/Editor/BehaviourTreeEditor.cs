using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;

namespace ArtGallery.BehaviourTree.Editor
{
    public class BehaviourTreeEditor : EditorWindow
    {
        [MenuItem("Window/Behaviour Tree Editor")] 
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(BehaviourTreeEditor), false, "Behaviour Tree Editor");
        }

        [OnOpenAssetAttribute(1)]
        public static bool OpenBehaviourTree(int instanceID, int line)
        {
            BehaviourTree tree = EditorUtility.InstanceIDToObject(instanceID) as BehaviourTree;

            if(tree != null)
            {
                ShowEditorWindow();
                return true;
            }

            return false;
        }

        private void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
            (
                "Assets/Scripts/Behaviour Tree/Editor/BehaviourTreeEditor.uxml"
            );

            visualTree.CloneTree(root);

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>
            (
                "Assets/Scripts/Behaviour Tree/Editor/BehaviourTreeEditor.uss"
            );

            root.styleSheets.Add(styleSheet);
        }
    }   
}