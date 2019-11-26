using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI_System
{
    public class UI_System : MonoBehaviour
    {
        private Component[] _screens = new Component[0];
        private UI_Screen _currScreen;
        private UI_Screen _prevScreen;

        [Header("Main Properties")]
        public UI_Screen StartScreen;

        [Header("System events")]
        public UnityEvent OnSwitchedScreen = new UnityEvent();

        [Header("Fader Properties")]
        public Image FaderImage;

        public float FadeInDuration;
        public float FadeOutDuration;

        /// <summary>
        /// The currently active UI_Screen
        /// </summary>
        public UI_Screen CurrScreen
        {
            get { return _currScreen; }
        }
        /// <summary>
        /// Previously active UI_Screen
        /// </summary>
        public UI_Screen PrevScreen
        {
            get { return _prevScreen; }
        }

        // Start is called before the first frame update
        void Start()
        {
            _screens = GetComponentsInChildren<UI_Screen>(true);
            initializeScreens();

            if (StartScreen)
            {
                SwitchScreen(StartScreen);
            }

            if (FaderImage)
            {
                FaderImage.gameObject.SetActive(true);
            }
            FadeIn();
        }

        /// <summary>
        /// Switch to UI_Screen regardless of if this is the current scene
        /// </summary>
        /// <param name="screen"></param>
        public void ForceSwitchScreen(UI_Screen screen)
        {
            switchScreen(screen);
        }

        /// <summary>
        /// Switch to another UI_Screen. Won't switch if the current scene is the same as the one to switch to.
        /// </summary>
        /// <param name="screen"></param>
        public void SwitchScreen(UI_Screen screen)
        {
            if (screen == _currScreen)
            {
                return;
            }

            switchScreen(screen);
        }

        /// <summary>
        /// Go to previous UI_Screen
        /// </summary>
        public void GoToPrevScreen()
        {
            if (_prevScreen)
            {
                SwitchScreen(_prevScreen);
            }
        }

        /// <summary>
        /// Fade from Fader image to UI_Screen
        /// </summary>
        public void FadeIn()
        {
            if (FaderImage)
            {
                FaderImage.CrossFadeAlpha(0.0f, FadeInDuration, false);
            }
        }

        /// <summary>
        /// Fade from UI_Screen to Fader
        /// </summary>
        public void FadeOut()
        {
            if (FaderImage)
            {
                FaderImage.CrossFadeAlpha(1.0f, FadeOutDuration, false);
            }
        }

        private void switchScreen(UI_Screen screen)
        {
            if (screen)
            {
                if (_currScreen)
                {
                    _currScreen.CloseScreen();
                    _prevScreen = _currScreen;
                }

                _currScreen = screen;
                _currScreen.gameObject.SetActive(true);
                _currScreen.StartScreen();

                if (OnSwitchedScreen != null)
                {
                    OnSwitchedScreen.Invoke();
                }
            }
        }

        /// <summary>
        /// Set all UI_Screens active
        /// </summary>
        private void initializeScreens()
        {
            for (int i = 0; i < _screens.Length; ++i)
            {
                _screens[i].gameObject.SetActive(true);
            }
        }
    }

}