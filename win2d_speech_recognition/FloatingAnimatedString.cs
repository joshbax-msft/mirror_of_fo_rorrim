﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;

namespace win2d_speech_recognition {
    class FloatingAnimatedString {
        private static Random r = new Random(DateTime.Now.Millisecond);
        private Vector2 _position;
        private List<FloatingAnimatedCharacter> Characters = new List<FloatingAnimatedCharacter>();
        private int loopCount;
        private int _width { get { return Characters.Sum(x => x.Width); } }
        private int _velocityX;

        public FloatingAnimatedString(CanvasDevice device, string str) {
            byte opacity = (byte)(10 + r.Next(10));
            foreach (char c in str) {
                if (r.Next(2) == 0) {
                    Characters.Add(new FloatingAnimatedCharacter(device, c, Color.FromArgb(opacity, AnimatedString.DarkColor.R, AnimatedString.DarkColor.G, AnimatedString.DarkColor.B)));
                }
                else {
                    Characters.Add(new FloatingAnimatedCharacter(device, c, Color.FromArgb(opacity, AnimatedString.LightColor.R, AnimatedString.LightColor.G, AnimatedString.LightColor.B)));
                }

            }

            switch (r.Next(2)) {
                case 0:
                    _position = new Vector2(1920, r.Next(900));
                    _velocityX = -(1 + r.Next(2));
                    break;
                case 1:
                    _position = new Vector2(-_width, r.Next(900));
                    _velocityX = 1 + r.Next(2);
                    break;
            }

        }

        public bool IsOutOfBounds { get { return _position.X > 2000 || _position.X < -_width - 50; } }

        public void Draw(CanvasAnimatedDrawEventArgs args) {
            int x = (int)_position.X;
            for (int i = 0; i < Characters.Count; i++) {
                int y = (int)(_position.Y + 200 * Math.Sin(i * .20 + loopCount * 0.01));
                Characters[i].Draw(args, new Vector2(x, y));
                x += Characters[i].Width;
            }
        }

        public void Update(CanvasAnimatedUpdateEventArgs args) {
            _position.X += _velocityX;
            loopCount++;
        }
    }
}
