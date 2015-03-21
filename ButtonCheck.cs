using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mono
{
    static class ButtonCheck
    {
        private static KeyboardState _oldState;
        private static KeyboardState _newState;

        private static GamePadState _newGamePadState;
        private static GamePadState _oldGamePadState;

        static public State Enter { get; set; }
        static public State Esc { get; set; }
        static public State Space { get; set; }
        static public State Shoot { get; set; }
        static public State Left { get; set; }
        static public State Right { get; set; }
        static public State S { get; set; }
        static public State I { get; set; }
        static public State L { get; set; }
        public static void Update()
        {
            Enter = State.False;
            Esc = State.False;
            Space = State.False;
            Shoot = State.False;
            Left = State.False;
            Right = State.False;
            S = State.False;
            I = State.False;
            L = State.False;

            _newGamePadState = GamePad.GetState(PlayerIndex.One);

            if (_newGamePadState.IsConnected)
            {
                if (_newGamePadState.DPad.Left == ButtonState.Pressed)
                    Left = State.Down;
                else if (_oldGamePadState.DPad.Left == ButtonState.Pressed)
                    Left = State.Up;

                if (_newGamePadState.DPad.Right == ButtonState.Pressed)
                    Right = State.Down;
                else if (_oldGamePadState.DPad.Right == ButtonState.Pressed)
                    Right = State.Up;

                if (_newGamePadState.IsButtonDown(Buttons.X) && !_oldGamePadState.IsButtonDown(Buttons.X))
                    Shoot = State.Down;
                else if (_oldGamePadState.IsButtonDown(Buttons.X))
                    Shoot = State.Up;

                if (_newGamePadState.IsButtonDown(Buttons.Y))
                    Space = State.Down;

                if (_newGamePadState.IsButtonDown(Buttons.A) && !_oldGamePadState.IsButtonDown(Buttons.A))
                    Enter = State.Down;
                else if (_oldGamePadState.IsButtonDown(Buttons.A))
                    Enter = State.Up;

                if (_newGamePadState.IsButtonDown(Buttons.B) && !_oldGamePadState.IsButtonDown(Buttons.B))
                    Esc = State.Down;

                if (_newGamePadState.IsButtonDown(Buttons.RightShoulder) && !_oldGamePadState.IsButtonDown(Buttons.RightShoulder))
                    I = State.Down;
                else if (_oldGamePadState.IsButtonDown(Buttons.RightShoulder))
                    I = State.Up;

               if (_newGamePadState.IsButtonDown(Buttons.LeftShoulder) && !_oldGamePadState.IsButtonDown(Buttons.LeftShoulder))
                   S = State.Down;
                else if (_oldGamePadState.IsButtonDown(Buttons.LeftShoulder))
                   S = State.Up;
               if (_newGamePadState.IsButtonDown(Buttons.Back) && !_oldGamePadState.IsButtonDown(Buttons.Back))
                   L = State.Down;
               else if (_oldGamePadState.IsButtonDown(Buttons.Back))
                   L = State.Up;

                if (_newGamePadState.Buttons.GetHashCode() != 0 || _oldGamePadState.Buttons.GetHashCode() != 0)
                    UpdateKey.Update();

                _oldGamePadState = _newGamePadState;
            }

            else
            {
                _newState = Keyboard.GetState();

                if (_newState.IsKeyDown(Keys.Left))
                    Left = State.Down;
                else if (_oldState.IsKeyDown(Keys.Left))
                    Left = State.Up;

                if (_newState.IsKeyDown(Keys.Right))
                    Right = State.Down;

                else if (_oldState.IsKeyDown(Keys.Right))
                    Right = State.Up;

                if ((_newState.IsKeyDown(Keys.LeftControl) || _newState.IsKeyDown(Keys.RightControl)) &&
                    !_oldState.IsKeyDown(Keys.LeftControl) && !_oldState.IsKeyDown(Keys.RightControl))
                    Shoot = State.Down;

                else if (_oldState.IsKeyDown(Keys.LeftControl) || _oldState.IsKeyDown(Keys.RightControl))
                    Shoot = State.Up;

                if (_newState.IsKeyDown(Keys.Space))
                    Space = State.Down;
                else
                    Space = State.Up;

                if (_newState.IsKeyDown(Keys.Escape) && !_oldState.IsKeyDown(Keys.Escape))
                    Esc = State.Down;

                if (_newState.IsKeyDown(Keys.Enter) && !_oldState.IsKeyDown(Keys.Enter))
                    Enter = State.Down;

                else if (_oldState.IsKeyDown(Keys.Enter))
                    Enter = State.Up;

                if (_newState.IsKeyDown(Keys.S) && !_oldState.IsKeyDown(Keys.S))
                    S = State.Down;

                else if (_oldState.IsKeyDown(Keys.S))
                    S = State.Up;

                if (_newState.IsKeyDown(Keys.I) && !_oldState.IsKeyDown(Keys.I))
                    I = State.Down;

                else if (_oldState.IsKeyDown(Keys.I))
                    I = State.Up;
                
                if (_newState.IsKeyDown(Keys.L) && !_oldState.IsKeyDown(Keys.L))
                    L = State.Down;

                else if (_oldState.IsKeyDown(Keys.L))
                    L = State.Up;

                if (_newState.GetPressedKeys().Length != 0 || _oldState.GetPressedKeys().Length != 0)
                {
                    UpdateKey.Update();
                }

                _oldState = _newState;
            }
        }
    }

    enum State
    {
        Up, Down, False
    }
}
