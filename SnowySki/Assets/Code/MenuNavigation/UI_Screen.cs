using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI_System
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Animator))]
    public class UI_Screen : MonoBehaviour
    {
        [Header("Main Properties")]
        public Selectable StartSelectable;

        [Header("Screen Events")]
        public UnityEvent OnScreenStart = new UnityEvent();
        public UnityEvent OnScreenClose = new UnityEvent();

        private Animator _animator;

        // Start is called before the first frame update
        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Start()
        {
            if (StartSelectable)
            {
                EventSystem.current.SetSelectedGameObject(StartSelectable.gameObject);
            }
        }

        /// <summary>
        /// Trigger event and use animator to fade in screen
        /// </summary>
        public virtual void StartScreen()
        {
            if (OnScreenStart != null)
            {
                OnScreenStart.Invoke();
            }

            handleAnimator("show");
        }

        /// <summary>
        /// trigger event and use animator to fade out screen
        /// </summary>
        public virtual void CloseScreen()
        {
            if (OnScreenClose != null)
            {
                OnScreenClose.Invoke();
            }

            handleAnimator("hide");
        }

        /// <summary>
        /// Tell animator to trigger transition
        /// </summary>
        /// <param name="trigger"></param>
        private void handleAnimator(string trigger)
        {
            if (_animator)
            {
                _animator.SetTrigger(trigger);
            }
        }

    }

}
