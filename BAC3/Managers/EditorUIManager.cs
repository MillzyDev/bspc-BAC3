using BAC3.UI.FlowCoordinators;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using System;
using Zenject;

namespace BAC3.Managers
{
    internal class EditorUIManager : IInitializable, IDisposable
    {
        private MenuButton _menuButton;
        private readonly EditorFlowCoordinator _editorFlowCoordinator;

        EditorUIManager(EditorFlowCoordinator editorFlowCoordinator)
        {
            _editorFlowCoordinator = editorFlowCoordinator;
        }

        public void Initialize()
        {
            _menuButton = new MenuButton("BAC3", "Open the BAC3 Editor", OnMenuButtonClick);
            MenuButtons.instance.RegisterButton(_menuButton);
        }

        void OnMenuButtonClick()
        {
            _editorFlowCoordinator.Show();
        }

        public void Dispose()
        {
            MenuButtons.instance.UnregisterButton(_menuButton);
        }
    }
}
