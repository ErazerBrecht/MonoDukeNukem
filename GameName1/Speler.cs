using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace Mono
{
    class Speler : MoveGameObject
    {
        //Textures
        private static Texture2D _picRight;
        private static Texture2D _picLeft;
        private static Texture2D _picWalkLeft;
        private static Texture2D _picWalkRight;
        private static Texture2D _picJumpLeft;
        private static Texture2D _picJumpRight;
        private static Texture2D _picEmpty;
        private static Texture2D _picDeath;

        //Sounds
        private static SoundEffect _soundJump;
        private static SoundEffect _soundLand;
        private static SoundEffect _soundDeath;
        private static SoundEffect _soundShoot;
        private static SoundEffect _soundHit;

        //Private Rectangles
        private Rectangle _rectangleStand;
        private Rectangle _rectangleWalk;
        private Rectangle _rectangleJump;
        private Rectangle _rectangleDie;

        //Public Rectangles
        public Rectangle RectangleCollisionHead;
        public Rectangle RectangleCollisionFeet;

        private List<Leven> _levens;

        public int LevensInt
        {
            get { return _levens.Count; }
        }

        //Score
        private Score _score;
        private int _scoreInt;
        public int ScoreInt
        {
            get { return _scoreInt; }
            set                                             //Only change _score when it changes! Performanter!
            {
                _scoreInt = value;
                _score.ScoreString = "";

                for (int i = 0; i < 7 - Convert.ToString(_scoreInt).Length; i++)
                {
                    _score.ScoreString += "0";
                }

                _score.ScoreString += Convert.ToString(_scoreInt);
            }
        }

        private bool _left, _right, _shoot, _jump, _oldLand = false;
        public bool Land { get; set; }
        public bool Hit { get; set; }
        public bool Death { get; set; }
        public bool BounceVertical { get; set; }
        public bool BounceHorizontal { get; set; }
        public Richting BounceHorizontalRichting { get; set; }

        private Richting _richting;

        private BulletPlayer _bullet;
        private int _reload = 0;
        private int _teller = 0;

        private float _jumpHeight;
        private float _gravity = 200;

        public Speler()
        {
            Positie = new Vector2(0, 0);        //Sets in level!
            TextureActive = _picRight;
            _rectangleStand = new Rectangle(0, 0, _picRight.Width, _picRight.Height);
            _rectangleWalk = new Rectangle(0, 0, _picWalkRight.Width / 4, _picRight.Height);
            _rectangleJump = new Rectangle(0, 0, _picJumpRight.Width / 2, _picJumpRight.Height);
            _rectangleDie = new Rectangle(0, 0, _picDeath.Width / 3, _picDeath.Height);
            RectangleActive = _rectangleStand;

            RectangleCollisionFeet = new Rectangle(LocationX + 10, LocationY - 70, 25, 12);             //RectangleCollisionFeet is a litte bit under the player to make sure there is collision!
            RectangleCollisionHead = new Rectangle(LocationX + 6, LocationY, 25, 5);
            RectangleCollision = new Rectangle(LocationX, LocationY, _picRight.Width - 40, 70);

            UpdateKey.UpdateEvent += UpdateKey_UpdateEvent;

            _score = new Score(550, 10);
            ScoreInt = 0;
            _levens = new List<Leven>();

            //Toevoegen van levens! Hier kunnen we ook het aantal aanpassen
            for (int i = 0; i < 5; i++)
            {
                _levens.Add(new Leven(30 * i + 15, 30));
            }   
        }

        public Speler(int posX, int posY) : this()
        {
            Positie.X = posX;
            Positie.Y = posY;
            _levens.Clear();
        }

        void UpdateKey_UpdateEvent()
        {
            if (!Level.Pauze)
            {
                if (ButtonCheck.Left == State.Down)
                    ButtonLeftDown();
                else if (ButtonCheck.Left == State.Up)
                    ButtonLeftUp();

                if (ButtonCheck.Right == State.Down)
                    ButtonRightDown();
                else if (ButtonCheck.Right == State.Up)
                    ButtonRightUp();

                if (ButtonCheck.Shoot == State.Down)
                    ButtonShootDown();
                else if (ButtonCheck.Shoot == State.Up)
                    ButtonShootUp();

                if (ButtonCheck.Space == State.Down && !_jump && Land)
                    ButtonJumpDown();
            }
        }


        public override void Update(GameTime g)
        {
            Ticks += g.ElapsedGameTime.Milliseconds;

            #region Land
            
            if (!Land)
                _oldLand = false;       //_oldLand is een variabele die zorgt dat er niet constant in de functionaliteit hieronder wordt gegaan!

            else if (!_oldLand)
            {
                //Deel van de "death animation"
                if (Die == true)
                {
                    RectangleActive.X = 160;
                    Death = true;
                }

                else
                {
                    //Als je Land en je bent niet dood moet de Stilstaan texture gebruikt worden
                    if (_richting == Richting.Rechts)
                        TextureActive = _picRight;
                    else
                        TextureActive = _picLeft;

                    RectangleActive.X = 0;

                    //Als je aan het springen bent en je land moet de speler terug stilstaan! Jumpheigt moet ook positief zijn anders zijn we nog naar boven aan het springen!
                    if (_jump && _jumpHeight > 0)
                    {
                        _soundLand.Play();
                        RectangleActive = _rectangleStand;
                        _jump = false;
                    }

                    if (_gravity != 200)  //Dan ben je aan het vallen        
                        _soundLand.Play();

                    //_soundLand.Play() kan niet constant gespeeld worden want indien je voeten een blok raken zal het dan ook spelen dit niet de bedoeling!

                    //Als je land moet de zwaartekracht gereset worden, zodanig indien je terug valt het terug begint te dalen! 
                    _gravity = 200;
                }

                _oldLand = true;
            }
            #endregion

            #region UpdateFunctionality
            if (Die == false)
            {
                if (_reload > 0 && Ticks >= 66)
                    _reload--;                      //Reload timer!               

                #region Zwaartekracht
                if (!Land && !_jump)
                {
                    RectangleActive = _rectangleJump;

                    if (_richting == Richting.Rechts)
                        TextureActive = _picJumpRight;
                    else
                        TextureActive = _picJumpLeft;

                    RectangleActive.X = 80;             //val sprite
                    Positie.Y += _gravity * (float)g.ElapsedGameTime.TotalSeconds; ;
                     _gravity += 300 * (float)g.ElapsedGameTime.TotalSeconds;

                }
                #endregion

                #region Wandelen
                if ((_left || _right) && (_teller % 2 == 0) && (!_jump && Land))
                {
                    if (Ticks >= 66)
                    {
                        _rectangleWalk.X += 80;
                    }

                    if (_rectangleWalk.X >= 320)
                        _rectangleWalk.X = 0;

                    RectangleActive = _rectangleWalk;
                }

                if (_left)
                {
                    //Als de speler botst mag deze niet meer opschuiven in de richting dat hij botst.
                    if (!(BounceHorizontal && BounceHorizontalRichting == Richting.Links))
                        LocationX -= Convert.ToInt32(200 * g.ElapsedGameTime.TotalSeconds);

                    if (_jump || !Land)
                        TextureActive = _picJumpLeft;
                    else
                        TextureActive = _picWalkLeft;
                }

                if (_right)
                {
                    //Als de speler botst mag deze niet meer opschuiven in de richting dat hij botst.
                    if (!(BounceHorizontal && BounceHorizontalRichting == Richting.Rechts))
                        LocationX += Convert.ToInt32(200 * g.ElapsedGameTime.TotalSeconds);

                    if (_jump || !Land)
                        TextureActive = _picJumpRight;
                    else
                        TextureActive = _picWalkRight;
                }

                #endregion

                #region Springen
                if (_jump)
                {
                    Land = false;
                    if (BounceVertical == true)
                    {
                        BounceVertical = false;
                        _jump = false;
                    }

                    else
                    {
                        if (_jumpHeight == -290)     //1ste keer!
                            _soundJump.Play();

                        else if (_jumpHeight > 0)        //Is de Speler aan het vallen?
                            RectangleActive.X = 80;

                        Positie.Y += _jumpHeight * (float)g.ElapsedGameTime.TotalSeconds;
                        _jumpHeight += 300 * (float)g.ElapsedGameTime.TotalSeconds;
                    }

                }
                #endregion

                #region Schieten
                if (_shoot)
                {
                    if (_richting == Richting.Rechts)
                        _bullet = new BulletPlayer(LocationX + RectangleActive.Width - 20, LocationY + RectangleActive.Height / 2 + 5, Richting.Rechts);      //+5 Omdat geweer niet in het midden van speler zit!
                    else
                        _bullet = new BulletPlayer(LocationX + 20, LocationY + RectangleActive.Height / 2 + 5, Richting.Links);
                    _soundShoot.Play();
                }
                #endregion

                #region GeraaktWorden
                if (Hit && Ticks >= 66)
                {
                    if (_teller == 0)
                    {
                        if (_levens.Count > 0)
                        {
                            _soundHit.Play();
                            _levens.RemoveAt(_levens.Count - 1);
                        }
                    }

                    if (_levens.Count == 0)         //Dood gaan
                    {
                        Console.WriteLine("DEATH");
                        _soundDeath.Play();
                        _jump = true;
                        LocationY -= 1;             //ZORGEN DAT ER ZEKER GEEN COLLISION MEER IS MET DE GROND!!!
                        _jumpHeight = -60;
                        Die = true;
                        TextureActive = _picDeath;
                        RectangleActive = _rectangleDie;
                    }

                    else
                        _teller++;

                    if (_teller > 25)
                    {
                        _teller = 0;
                        Hit = false;
                    }
                }
                #endregion

                #region FrameIndependant
                if (Ticks >= 66)
                    Ticks = 0;
                #endregion

            }
            #endregion      //Update

            #region DeathAnimation
            else
            {
                RectangleCollision.Width = 0;
                RectangleCollision.Height = 0;
                if (_jumpHeight > 0 && Land == false)
                {
                    RectangleActive.X = 80;
                }

                if (!Land)
                {
                    Positie.Y += _jumpHeight * (float)g.ElapsedGameTime.TotalSeconds;
                    _jumpHeight += 300 * (float)g.ElapsedGameTime.TotalSeconds;
                }
            }
            #endregion

            UpdateCollisionRectangles();
        }

        private void ButtonJumpDown()
        {
            _jump = true;
            RectangleActive = _rectangleJump;

            if (_richting == Richting.Rechts)
                TextureActive = _picJumpRight;
            else
                TextureActive = _picJumpLeft;

            _jumpHeight = -290;
        }

        private void ButtonShootUp()
        {
            _shoot = false;
        }

        private void ButtonShootDown()
        {
            if (_shoot == true)
                _shoot = false;

            else if (_reload < 1)
            {
                _shoot = true;
                _reload = 5;        //Settings reload time (Smaller time -> More bullets / seconds)
            }
        }

        private void ButtonRightUp()
        {
            _right = false;
            if (!_jump)
            {
                RectangleActive = _rectangleStand;
                TextureActive = _picRight;
            }
        }

        private void ButtonRightDown()
        {
            _right = true;
            _richting = Richting.Rechts;
        }

        private void ButtonLeftUp()
        {
            _left = false;
            if (!_jump)
            {
                RectangleActive = _rectangleStand;
                TextureActive = _picLeft;
            }
        }

        private void ButtonLeftDown()
        {
            _left = true;
            _richting = Richting.Links;
        }

        public override void UpdateCollisionRectangles()
        {
            RectangleCollisionFeet.Y = LocationY + 70;
            RectangleCollisionFeet.X = LocationX + 28;     

            RectangleCollisionHead.Y = LocationY;
            RectangleCollisionHead.X = LocationX + 29;

            RectangleCollision.Y = LocationY;
            RectangleCollision.X = LocationX + 20;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_teller % 2 != 0)
            {
                spriteBatch.Draw(_picEmpty, Positie, RectangleActive, Color.White);
            }

            else
            {
                base.Draw(spriteBatch);
            }

            foreach (Leven f in _levens)
            {
                f.Draw(spriteBatch);
            }

            _score.Draw(spriteBatch);
        }

        public void AddLive()
        {
            _levens.Add(new Leven(30 * _levens.Count + 15, 30));
        }


        public static void LoadContent(ContentManager content)
        {
            _picRight = content.Load<Texture2D>("HeroRight");
            _picLeft = content.Load<Texture2D>("HeroLeft");
            _picWalkRight = content.Load<Texture2D>("WalkRight");
            _picWalkLeft = content.Load<Texture2D>("WalkLeft");
            _picJumpRight = content.Load<Texture2D>("JumpRight");
            _picJumpLeft = content.Load<Texture2D>("JumpLeft");
            _picEmpty = content.Load<Texture2D>("Empty");
            _picDeath = content.Load<Texture2D>("Death");

            _soundJump = content.Load<SoundEffect>("Jump");
            _soundLand = content.Load<SoundEffect>("Land");
            _soundDeath = content.Load<SoundEffect>("DeathSound");
            _soundShoot = content.Load<SoundEffect>("Shoot");
            _soundHit = content.Load<SoundEffect>("Hit");
        }

    }

}
