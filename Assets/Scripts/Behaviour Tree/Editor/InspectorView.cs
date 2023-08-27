using UnityEngine.UIElements;

namespace ArtGallery.BehaviourTree.Editor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        UnityEditor.Editor editor;

        public void UpdateSelection(NodeView nodeView)
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(nodeView.GetNode());

            IMGUIContainer container = new IMGUIContainer( () => 
            { 
                if(editor.target)
                {
                    editor.OnInspectorGUI();
                }
            });
            
            Add(container);
        }
    }   
}
