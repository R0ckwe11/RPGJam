using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ButtonScript : MonoBehaviour
    {
        public Text buttonText;
        public Sprite normalSpr;
        public Sprite disabledSpr;
        public Sprite hoverSpr;
        public Sprite clickedSpr;
        public AudioClip hoverSound;
        public AudioClip clickSound;
        public AudioClip offClickSound;
        public event EventHandler ButtonClicked;

        public bool Disabled
        {
            get => disabled;
            set
            {
                disabled = value;
                spriteRenderer.sprite = value ? disabledSpr : normalSpr;
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                spriteRenderer.enabled = visible;
                boxCollider.enabled = visible;
            }
        }

        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCollider;
        private bool disabled;
        private bool visible = true;

        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseDown()
        {
            if (Disabled) return;
            spriteRenderer.sprite = clickedSpr;
            if (clickSound)
            {
                AudioSource.PlayClipAtPoint(clickSound, new Vector3(0, 0, 0));
            }
            ButtonClicked?.Invoke(this, null);
        }

        void OnMouseUp()
        {
            if (Disabled) return;
            spriteRenderer.sprite = hoverSpr;
            if (offClickSound)
            {
                AudioSource.PlayClipAtPoint(offClickSound, new Vector3(0, 0, 0));
            }
        }

        void OnMouseEnter()
        {
            if (Disabled) return;
            spriteRenderer.sprite = hoverSpr;
            if (hoverSound)
            {
                AudioSource.PlayClipAtPoint(hoverSound, new Vector3(0, 0, 0));
            }
        }

        void OnMouseExit()
        {
            if (!Disabled)
            {
                spriteRenderer.sprite = normalSpr;
            }
        }
    }
}
