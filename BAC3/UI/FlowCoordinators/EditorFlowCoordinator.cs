using BAC3.Managers;
using BAC3.UI.ViewControllers;
using BeatSaberMarkupLanguage;
using HMUI;
using IPA.Utilities;
using System.Linq;
using Tweening;
using UnityEngine;

namespace BAC3.UI.FlowCoordinators
{
    internal class EditorFlowCoordinator : FlowCoordinator
    {
        private readonly EditorMainViewController _editorMainViewController;
        private readonly AvatarManager _avatarManager;
        private GameObject _avatar;

        public EditorFlowCoordinator(EditorMainViewController editorMainViewController, AvatarManager avatarManager)
        {
            _editorMainViewController = editorMainViewController;
            _avatarManager = avatarManager;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("BAC3 Editor");
                showBackButton = true;

                ProvideInitialViewControllers(_editorMainViewController);

                _avatar = Instantiate(_avatarManager.AvatarPrefab);
                _avatar.name = "BAC3EditorAnimatedAvatar";
                _avatar.transform.position = new Vector3(0f, -0.6f, 4f);
                _avatar.transform.eulerAngles = new Vector3(0f, 200f, 0f);     
            }
            _avatar.SetActive(true);

            var animatedAvatarPoseController = _avatar.GetComponent<AnimatedAvatarPoseController>();
            var avatarPoseController = _avatar.GetComponentInChildren<AvatarPoseController>();
            FieldAccessor<AnimatedAvatarPoseController, AvatarPoseController>.Set(animatedAvatarPoseController, "_avatarPoseController", avatarPoseController);

            var avatarTweenController = _avatar.GetComponent<AvatarTweenController>();
            var tweeningManager = Resources.FindObjectsOfTypeAll<TimeTweeningManager>().First();
            FieldAccessor<AvatarTweenController, TimeTweeningManager>.Set(avatarTweenController, "_tweeningManager", tweeningManager);
            avatarTweenController.PresentAvatar();
        }

        protected override void DidDeactivate(bool removedFromHierarchy, bool screenSystemDisabling)
        {
            _avatar.SetActive(true);
            _avatar.GetComponent<AvatarTweenController>().HideAvatar();
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
