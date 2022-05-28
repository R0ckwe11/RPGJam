using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        public ButtonScript startButton;
        public ButtonScript exitButton;
        public List<SpriteRenderer> stuffToShowHide;
        public SpriteRenderer winLostSpriteRenderer;
        public Sprite youWinSprite;
        public Sprite youLostSprite;

        private SpriteRenderer thisSpriteRenderer;
        private Collider2D boxCollider;

        // Start is called before the first frame update
        void Start()
        {
            thisSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            boxCollider = gameObject.GetComponent<BoxCollider2D>();
            startButton.ButtonClicked += StartClicked;
            exitButton.ButtonClicked += ExitClicked;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartClicked(object sender, EventArgs e)
        {
            // gameLogic.StartGame();
            Hide();
        }

        public void ExitClicked(object sender, EventArgs e)
        {
            Application.Quit();
        }

        public void Show()
        {
            thisSpriteRenderer.enabled = true;
            winLostSpriteRenderer.enabled = true;
            foreach (var spr in stuffToShowHide)
            {
                spr.enabled = true;
            }
            boxCollider.enabled = true;
            startButton.Visible = true;
            exitButton.Visible = true;
        }

        public void Hide()
        {
            thisSpriteRenderer.enabled = false;
            winLostSpriteRenderer.enabled = false;
            foreach (var spr in stuffToShowHide)
            {
                spr.enabled = false;
            }
            boxCollider.enabled = false;
            startButton.Visible = false;
            exitButton.Visible = false;
        }

        public void Endgame(bool youWin)
        {
            winLostSpriteRenderer.sprite = youWin ? youWinSprite : youLostSprite;
            Show();
        }
    }
}
