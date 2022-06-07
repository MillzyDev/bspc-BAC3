using BAC3.UI.ViewControllers;
using BeatSaberMarkupLanguage;
using HMUI;
using UnityEngine;

namespace BAC3.UI.FlowCoordinators
{
    internal class EditorFlowCoordinator : FlowCoordinator
    {
        EditorMainViewController _editorMainViewController;

        GameObject _avatar;

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("BAC3 Editor");
                showBackButton = true;

                if (_editorMainViewController == null)
                    _editorMainViewController = BeatSaberUI.CreateViewController<EditorMainViewController>();

                ProvideInitialViewControllers(_editorMainViewController);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
        }

        public void Show()
        {
            FlowCoordinator parent = BeatSaberUI.MainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            BeatSaberUI.PresentFlowCoordinator(parent, this);
        }
    }
}
