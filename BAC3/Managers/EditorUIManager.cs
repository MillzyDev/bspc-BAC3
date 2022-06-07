using BAC3.UI.FlowCoordinators;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using System;
using Zenject;

namespace BAC3.Managers
{
    internal class EditorUIManager : IInitializable, IDisposable
    {
        AvatarManager _avatarManager;
        MenuButton _menuButton;
        EditorFlowCoordinator flow;

        EditorUIManager(AvatarManager avatarManager)
        {
            _avatarManager = avatarManager;
        }

        public void Initialize()
        {
            _menuButton = new MenuButton("BAC3", "Open the BAC3 Editor", OnMenuButtonClick);
            MenuButtons.instance.RegisterButton(_menuButton);
        }

        void OnMenuButtonClick()
        {
            if (flow == null)
                flow = BeatSaberUI.CreateFlowCoordinator<EditorFlowCoordinator>();

            flow.Show();
        }

        public void Dispose()
        {
            MenuButtons.instance.UnregisterButton(_menuButton);
        }

        
    }
}
