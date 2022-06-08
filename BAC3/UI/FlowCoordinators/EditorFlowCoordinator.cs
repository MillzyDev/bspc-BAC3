using BAC3.Managers;
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

                _avatar = Instantiate(AvatarManager.Instance.AvatarPrefab);
                _avatar.name = "BAC3EditorAvatar";
                _avatar.transform.position = new Vector3(0f, 0.3f, 4.5f);
                _avatar.transform.eulerAngles = new Vector3(0f, 200f, 0f);
            }

            _avatar.SetActive(true);
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            _avatar.SetActive(false);
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
