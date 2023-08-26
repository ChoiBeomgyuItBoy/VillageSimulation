using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArtGallery.BehaviourTree.Editor
{
    public class BehaviourTreeEditor : EditorWindow
    {
        BehaviourTreeView treeView = null;
        InspectorView inspectorView = null;

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

        private void OnSelectionChange()
        {
            BehaviourTree tree = Selection.activeObject as BehaviourTree;

            if(tree == null)
            {
                if(Selection.activeGameObject)
                {
                    TreeController controller = Selection.activeGameObject.GetComponent<TreeController>();

                    if(controller != null)
                    {
                        tree = controller.GetBehaviourTree();
                    }
                }
            }

            if(Application.isPlaying)
            {
                if(tree != null)
                {
                    treeView.PopulateView(tree);
                }
            }
            else
            {
                if(tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
                {
                    treeView.PopulateView(tree);
                }
            }
        }

        private void OnInspectorUpdate()
        {
            treeView.UpdateNodeStates();
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

            treeView = root.Q<BehaviourTreeView>();
            inspectorView = root.Q<InspectorView>();
            treeView.onNodeSelected += OnNodeSelectionChanged;

            OnSelectionChange();
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange change)
        {
            switch (change)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
        }

        private void OnNodeSelectionChanged(NodeView nodeView)
        {
            inspectorView.UpdateSelection(nodeView);
        }
    }
}