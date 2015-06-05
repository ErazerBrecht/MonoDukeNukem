using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono
{
    class Begin : GameLoop
    {
        private static List<Texture2D> _picAnimatie = new List<Texture2D>();

        private Texture2D _textureActive;
        private Vector2 _positie;

        private Rectangle _rectangleActive;

        private int _ticks;
        private bool _anim;

        private bool _enter;
        private bool _esc;
        private bool _i;
        private bool _s;
        private bool _l;

        private bool _skipIntro;

        private KeyboardState _keyState;
        private KeyboardState _oldState;

        private GamePadState _newGamePadState;
        private GamePadState _oldGamePadState;

        public Begin(bool skipIntro = false)
        {
            _positie = new Vector2(0, 0);
            _rectangleActive = new Rectangle(0, 0, 800, 600);
            _skipIntro = skipIntro;

            UpdateKey.UpdateEvent += UpdateKey_UpdateEvent;

            //Als _skipIntro true is moet de animatie overgeslagen worden
            //Dit is het geval indien je dood gaat. Dan moet het beginscherm terug geladen worden.
            //Maar moet de animatie (loading screen) niet terug getoond worden!
            if (!_skipIntro)
            {
                _anim = true;
                _textureActive = _picAnimatie[0];
            }
            else
            {
                //Manueel forceren dat de enter key als ingedrukt was, deze was echter ingedrukt in het Level!
                _oldState = new KeyboardState(Keys.Enter);
                _textureActive = _picAnimatie[2];
            }
        }

        void UpdateKey_UpdateEvent()
        {
            if (!_anim)
            {
                if (ButtonCheck.Enter == State.Down)
                    _enter = true;
                else if (ButtonCheck.Enter == State.Up)
                    _enter = false;

                else if (ButtonCheck.Esc == State.Down)
                    _esc = true;

                else if (ButtonCheck.I == State.Down)
                    _i = true;
                else if (ButtonCheck.I == State.Up)
                    _i = false;

                else if (ButtonCheck.S == State.Down)
                    _s = true;
                else if (ButtonCheck.S == State.Up)
                    _s = false;

                else if (ButtonCheck.L == State.Down)
                    _l = true;
                else if (ButtonCheck.L == State.Up)
                    _l = false;
            }

        }

        public override GameLoop Update(GameTime g)
        {
            Screen.Update(_textureActive.Width / 2, _textureActive.Height / 2);     //Center the screen!

            _ticks += g.ElapsedGameTime.Milliseconds;

            if (_anim == true)
            {
                if (_ticks > 2000)
                    _textureActive = _picAnimatie[1];
                if (_ticks > 4000)
                {
                    _textureActive = _picAnimatie[2];
                    _anim = false;
                }
            }

            #region UpdateTexture
            if (_enter && _textureActive == _picAnimatie[2])
            {
                _textureActive = _picAnimatie[3];
                _enter = false;
            }

            else if (_enter && _textureActive == _picAnimatie[5])
            {
                _textureActive = _picAnimatie[6];
                _enter = false;
            }

            else if (_enter && _textureActive == _picAnimatie[6])
            {
                _enter = false;
                return new Level1(new Speler());
            }

            else if (_esc)
            {
                if (_textureActive == _picAnimatie[3])
                    _textureActive = _picAnimatie[2];
                else if (_textureActive == _picAnimatie[4])
                    _textureActive = _picAnimatie[3];

                _esc = false;
            }

            else if (_i && _textureActive == _picAnimatie[3])
            {
                _textureActive = _picAnimatie[4];
                _i = false;
            }

            else if (_s && _textureActive == _picAnimatie[3])
            {
                _s = false;
                _textureActive = _picAnimatie[5];
            }
            else if (_l && _textureActive == _picAnimatie[3])
            {
                _l = false;
                FileStream inFile = new FileStream(_FILENAME, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);
                string recordIn;
                string[] fields;
                List<Enemy> tempEnemy = new List<Enemy>();
                List<RemoveGameObject> tempBonus = new List<RemoveGameObject>();
                List<Sleutels> tempSleutels = new List<Sleutels>();
                bool blue = false;
                bool red = false;
               
                string Level = reader.ReadLine();
                int ScoreSpeler = Convert.ToInt32(reader.ReadLine());
                int Levens = Convert.ToInt32(reader.ReadLine());
                int PosX = Convert.ToInt32(reader.ReadLine());
                int PosY = Convert.ToInt32(reader.ReadLine());

                Speler tempSpeler = new Speler(PosX, PosY);
                tempSpeler.ScoreInt = ScoreSpeler;
                
                for (int i = 0; i < Levens; i++)
                {
                    tempSpeler.AddLive();
                }
               
                recordIn = reader.ReadLine();
                while (recordIn != null)
                {
                    fields = recordIn.Split(';');

                    if (fields[0] == "Robot")
                    {
                        tempEnemy.Add(new Robot((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2])));
                    }
                    else if (fields[0] == "MonsterRight")
                    {
                        tempEnemy.Add(new MonsterRight((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2])));
                    }
                    else if (fields[0] == "MonsterLeft")
                    {
                        tempEnemy.Add(new MonsterLeft((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2])));
                    }
                    else if (fields[0] == "Mine")
                    {
                        tempEnemy.Add(new Mine((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2])));
                    }
                    else if (fields[0] == "Tank")
                    {
                        tempEnemy.Add(new Tank((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2]), (int)Convert.ToDouble(fields[3])));
                    }
                    else if (fields[0] == "FireRobot")
                    {
                        tempEnemy.Add(new FireRobot((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2]), (int)Convert.ToDouble(fields[3])));
                    }
                    else if (fields[0] == "Floppy")
                    {
                        tempBonus.Add(new Floppy((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2])));
                    }
                    else if (fields[0] == "Cola")
                    {
                        tempBonus.Add(new Cola((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2])));
                    }
                    else if (fields[0] == "KeyRed")
                    {
                        tempBonus.Add(new KeyRed((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2])));
                    }
                    else if (fields[0] == "KeyBlue")
                    {
                        tempBonus.Add(new KeyBlue((int)Convert.ToDouble(fields[1]), (int)Convert.ToDouble(fields[2])));
                    }

                    recordIn = reader.ReadLine();
                }

                reader.Close();
                inFile.Close();

                foreach (RemoveGameObject r in tempBonus)
                {
                    if (r is KeyBlue)
                        blue = true;
                    else if (r is KeyRed)
                        red = true;
                }

                if (!red)
                    tempSleutels.Add(Sleutels.Red);
                if(!blue)
                    tempSleutels.Add(Sleutels.Blue);

                if (Level == "Level1")
                    return new Level1(tempSpeler, tempEnemy, tempBonus, tempSleutels);
                else
                    return new Level2(tempSpeler, tempEnemy, tempBonus, tempSleutels);


            }

            #endregion

            return this;
        }

        public override void Teken(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureActive, _positie, _rectangleActive, Color.White);
        }

        public static void LoadContent(ContentManager content)
        {
            _picAnimatie.Add(content.Load<Texture2D>("Begin1"));
            _picAnimatie.Add(content.Load<Texture2D>("Begin2"));
            _picAnimatie.Add(content.Load<Texture2D>("Begin3"));
            _picAnimatie.Add(content.Load<Texture2D>("Begin4"));
            _picAnimatie.Add(content.Load<Texture2D>("Begin5"));
            _picAnimatie.Add(content.Load<Texture2D>("Begin6"));
            _picAnimatie.Add(content.Load<Texture2D>("Begin7"));
        }

    }
}
