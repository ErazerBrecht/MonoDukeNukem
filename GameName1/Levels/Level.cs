using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Mono
{
    abstract class Level : GameLoop
    {
        static public bool Pauze { get; set; }          //static because whole game needs to know if it's pauze!

        private Background _background = new Background(0, 0);
        protected Speler _speler;
        
        //Lijst voor tiles
        private List<GameObject> tileObjecten = new List<GameObject>();
        //Lijst voor alle kogels en vijanden
        protected List<Bullet> _bulletList;
        protected List<Enemy> _enemyList = new List<Enemy>();
        protected List<RemoveGameObject> _bonusObjecten = new List<RemoveGameObject>();

        protected bool PreviousLoad;

        private Text _text;
        private Door _endDoor;
        private bool _collisionEndDoor, _enter;
        //End list --> Lijst waarin staat welke sleutels er nodig zijn om het level te beeindigen!
        protected List<Sleutels> _endList = new List<Sleutels>();
        //Key list --> Lijst waarin staat welke sleutes de speler al heeft opgeraapt!
        protected List<Sleutels> _keyList = new List<Sleutels>();

        //Level array! + Methode om Levelarray aan te maken in children!
        protected int[,] intTileArray;
        abstract protected void CreateTileArray();

        public int LevelWidth
        {
            get { return (intTileArray.GetUpperBound(1) + 1) * 32; }
        }

        public int LevelHeight
        {
            get { return (intTileArray.GetUpperBound(0) + 1) * 32; }
        }

        protected virtual void CreateWorld()
        {
            UpdateKey.UpdateEvent += UpdateKey_UpdateEvent;
            Pauze = false;

            Bullet.BulletList.Clear();              //Delete exsisting bullets (previous level...)
            _bulletList = Bullet.BulletList;

            #region BuildWorld
            for (int x = 0; x < 43; x++)
            {
                for (int y = 0; y < 104; y++)
                {
                    switch (intTileArray[x, y])
                    {
                        case 1:
                            tileObjecten.Add(new PlatformGround(y * 32, (x + 1) * 32));
                            break;
                        case 2:
                            tileObjecten.Add(new PlatformLeft(y * 32, (x + 1) * 32));
                            break;
                        case 3:
                            tileObjecten.Add(new PlatformMiddle(y * 32, (x + 1) * 32));
                            break;
                        case 4:
                            tileObjecten.Add(new PlatformRight(y * 32, (x + 1) * 32));
                            break;
                        case 5:
                            tileObjecten.Add(new Box(y * 32, (x + 1) * 32));
                            break;
                        case 6:
                            tileObjecten.Add(new Barrel(y * 32, (x + 1) * 32));
                            break;
                        case 7:
                            tileObjecten.Add(new SpikesUp(y * 32, (x + 1) * 32));
                            break;
                        case 8:
                            tileObjecten.Add(new SpikesDown(y * 32, (x + 1) * 32));
                            break;
                        case 9:
                            tileObjecten.Add(new PlatformGroundReverse(y * 32, (x + 1) * 32));
                            break;
                        case 10:
                            tileObjecten.Add(new Flamethrower(y * 32, (x + 1) * 32));
                            break;
                        case 15:
                            _endDoor = new Door(y * 32, (x + 1) * 32);
                            break;
                        case -1:
                            tileObjecten.Add(new Collisionbox(y * 32, (x + 1) * 32));
                            break;

                    }

                    if (!PreviousLoad)
                    {
                        switch (intTileArray[x, y])
                        {
                            case 11:
                                _bonusObjecten.Add(new Floppy(y*32, (x + 1)*32));
                                break;
                            case 12:
                                _bonusObjecten.Add(new Cola(y*32, (x + 1)*32));
                                break;
                            case 13:
                                _bonusObjecten.Add(new KeyRed(y*32, (x + 1)*32));
                                break;
                            case 14:
                                _bonusObjecten.Add(new KeyBlue(y*32, (x + 1)*32));
                                break;
                            case -2:
                                _enemyList.Add(new Robot(y*32, (x + 1)*32));
                                break;
                            case -3:
                                _enemyList.Add(new MonsterRight(y*32, (x + 1)*32));
                                break;
                            case -4:
                                _enemyList.Add(new MonsterLeft(y*32, (x + 1)*32));
                                break;
                            case -5:
                                _enemyList.Add(new Tank(y*32, (x + 1)*32));
                                break;
                            case -6:
                                _enemyList.Add(new FireRobot(y*32, (x + 1)*32));
                                break;
                            case -7:
                                _enemyList.Add(new Mine(y*32, (x + 1)*32));
                                break;
                        }
                    }
                }
            }

            #endregion
        }

        private void Save()
        {
            FileStream fs = new FileStream(_FILENAME, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(this.GetType().Name);
            sw.WriteLine(_speler.ScoreInt);
            sw.WriteLine(_speler.LevensInt);
            sw.WriteLine(_speler.LocationX);
            sw.WriteLine(_speler.LocationY);

                foreach (GameObject g in this._enemyList)
                {
                    {
                        sw.WriteLine(g);
                    }
                }
                foreach (GameObject g in this._bonusObjecten)
                {
                    {
                        sw.WriteLine(g);
                    }
                }


            sw.Close();
            fs.Close();
        }

        void UpdateKey_UpdateEvent()
        {
            if (ButtonCheck.Enter == State.Down)
                ButtonEnterDown();
            else if (ButtonCheck.Enter == State.Up)
                ButtonEnterUp();

            if (ButtonCheck.Esc == State.Down)
                ButtonPause();

            if(ButtonCheck.S == State.Down)
                Save();
        }

        public override GameLoop Update(GameTime g)
        {

            #region Death (Spel opnieuw opstarten)
            if (_speler.Death)
            {
                if (!Pauze)
                {
                    _text = new Text((int)Screen.Positie.X + 300, (int)Screen.Positie.Y + 200)
                    {
                        Print = "You are DEAD!!!\nMission failed"
                    };
                    Pauze = true;
                }

                else if (_enter)
                    return new Begin(true);
            }
            #endregion

            else if (_collisionEndDoor && _enter)
            {
                _enter = false;

                if (_endList.Count == 0)
                {
                    if (this is Level1)
                        return new Level2(_speler);
                    if (this is Level2)
                        return new Begin(true);
                    //return new Level3(_speler);
                    //Voorbeeld voor als er extra Levels zouden bijkomen!
                }

                else if (!Pauze)
                {
                    _text = new Text((int)Screen.Positie.X + 200, (int)Screen.Positie.Y + 75)
                    {
                        Print = "You don't have all the keys!\nAccess Denied!!!"
                    };
                    Pauze = true;
                }

                else
                {
                    Pauze = false;
                    _text = null;
                }

            }

            if (!Pauze)
            {
                _speler.Update(g);
                Screen.Update(_speler.LocationX, _speler.LocationY);

                foreach (Enemy e in _enemyList)
                {
                    e.Update(g);
                }

                foreach (Bullet b in _bulletList)
                {
                    b.Update(g);
                }

                CheckCollision();

            }
            return this;

        }

        private void ButtonPause()
        {
            Pauze = !Pauze;
        }

        private void ButtonEnterUp()
        {
            _enter = false;
        }

        private void ButtonEnterDown()
        {
            _enter = true;
        }

        private void CheckCollision()
        {
            _speler.Land = false;
            _speler.BounceHorizontal = false;
            _collisionEndDoor = false;

            if (_speler.RectangleCollision.Intersects(_endDoor.RectangleCollision))
                _collisionEndDoor = true;

            #region CollisionTiles
            foreach (GameObject g in tileObjecten)
            {
                if (g.Damage)
                {
                    if (_speler.RectangleCollision.Intersects(g.RectangleCollision))
                        _speler.Hit = true;
                }

                else if (!(g is Collisionbox))
                {
                    //Botsen met hoofd
                    if (_speler.RectangleCollisionHead.Intersects(g.RectangleCollision) && !_speler.Land)
                    {
                        _speler.BounceVertical = true; //Speler is tegen een tile gebotst!
                        _speler.UpdateCollisionRectangles();
                    }

                        //Horizontale collision --> Botsen tegen tile
                    else if (_speler.RectangleCollision.Intersects(g.RectangleCollision))
                    {
                        if (_speler.LocationX < g.RectangleCollision.X)
                        {
                            _speler.BounceHorizontalRichting = MoveGameObject.Richting.Rechts;
                            //_speler.LocationX = g.RectangleCollision.X - _speler.RectangleCollision.Width;        //Optie om speler niet in blok te laten gaan, lost bug op van landen in tile.
                        }

                        else
                        {
                            _speler.BounceHorizontalRichting = MoveGameObject.Richting.Links;
                            //_speler.LocationX = g.RectangleCollision.X + g.RectangleCollision.Width - 20;
                        }

                        _speler.BounceHorizontal = true;
                        _speler.UpdateCollisionRectangles();
                    }

                    //Verticale collsion --> Landen
                    if (_speler.RectangleCollisionFeet.Intersects(g.RectangleCollision))
                    {
                        _speler.Land = true;
                        _speler.LocationY = g.LocationY;
                        _speler.UpdateCollisionRectangles();
                    }
                }

                #region Collision met kogels

                    foreach (Bullet f in _bulletList)
                    {
                        if (!(g is Collisionbox) && f.RectangleCollision.Intersects(g.RectangleCollision))
                            //Kogels moeten niet collisionen met collisionboxen (TODO: ZOEK BETERE IMPLEMENTATIE)
                        {
                            f.Explode = true;
                        }

                        if (!(f is BulletPlayer) && f.RectangleCollision.Intersects(_speler.RectangleCollision))
                            //Geen friendly fire t.o.v. je eigen, anders kun je je zelf raken
                        {
                            _speler.Hit = true;
                            f.Explode = true;
                        }
                    

                    #endregion
                }

                #region Collision met Enemies!
                foreach (Enemy e in _enemyList)
                {
                    if (e.RectangleCollision.Intersects(g.RectangleCollision))
                    {
                        e.Direction *= -1;

                        if (e.BewegingsRichting == Enemy.Beweging.Horizontaal)
                        {
                            if (e.LocationX < g.RectangleCollision.X)
                                //Zorgt ervoor dat Enemy niet in de tile terecht komt!
                                e.LocationX = g.RectangleCollision.X - e.RectangleCollision.Width;
                            else
                                e.LocationX = g.RectangleCollision.X + g.RectangleCollision.Width;
                        }

                        e.UpdateCollisionRectangles();      //Update collision rectangle zodanig dat deze klopt! (Indien niet gaat de collision rectangle voor een stuk de tile in en kan dit incorrect collision maken met de speler)
                    }

                #endregion

                }
            }
            #endregion

            #region Collision between PLAYER/ENEMY & ENEMY/BULLET
            foreach (Enemy e in _enemyList)
            {
                if (_speler.RectangleCollision.Intersects(e.RectangleCollision) && _speler.Hit == false)
                    _speler.Hit = true;

                foreach (Bullet t in _bulletList)
                {
                    //Er is geen friendly fire, enemies kunnen geen enemies neerschieten! Enkel speler kogels kunnen enemies doodden!
                    if (t is BulletPlayer)
                    {
                        if (t.RectangleCollision.Intersects(e.RectangleCollision))
                        {
                            t.Remove = true;
                            if (!(e is Mine))
                            {
                                _speler.ScoreInt += 100;
                                e.Die = true;
                            }
                        }
                    }
                }
            }


            #endregion

            foreach (RemoveGameObject r in _bonusObjecten)
            {

                if (_speler.RectangleCollision.Intersects(r.RectangleCollision))
                {
                    _speler.ScoreInt += 200;
                    r.Remove = true;

                    if (r is Cola)
                        _speler.AddLive();

                    else if (_endList.Count > 0)        //Vanaf deze waarde nul is moet er niet meer gekeken worden alle sleutes zijn opgeraakt!
                    {
                        if (r is KeyBlue)
                            _keyList.Add(Sleutels.Blue);
                        else if (r is KeyRed)
                            _keyList.Add(Sleutels.Red);
                    }
                }
            }

            #region RemovingItems
            //Remove items if they are dead/unnecessary
            for (int x = _enemyList.Count - 1; x >= 0; x--)
            {
                if (_enemyList[x].Remove)
                    _enemyList.RemoveAt(x);
            }

            for (int i = _bulletList.Count - 1; i >= 0; i--)
            {
                if (_bulletList[i].LocationX < 0 || _bulletList[i].LocationX > LevelWidth || _bulletList[i].LocationY > LevelHeight)
                    _bulletList.RemoveAt(i);
                else if (_bulletList[i].Remove)
                    _bulletList.RemoveAt(i);
            }

            for (int x = _bonusObjecten.Count - 1; x >= 0; x--)
            {
                if (_bonusObjecten[x].Remove)
                    _bonusObjecten.RemoveAt(x);
            }

            //Verwijderen van sleutes uit de endList!
            //Deze lijst bevat welke sleutels je nodig hebt om door te kunnen naar het volgende level!
            //Als deze lijst een Count heeft van nul kun je naar het volgende level!!!
            for (int x = _endList.Count - 1; x >= 0; x--)
            {
                if (_keyList.Contains(_endList[x]))
                {
                    _endList.RemoveAt(x);
                }
            }

            #endregion

        }

        public override void Teken(SpriteBatch spriteBatch)
        {
            //BUG!!!! Problemen op AMD systemen!
            //Als het gepauzeerd wordt moet er ook niet meer getekent worden! Beter voor de performantie!!!
            //if (!_pauze)
            //{
            _background.Draw(spriteBatch);
            _endDoor.Draw(spriteBatch);

            foreach (GameObject g in tileObjecten)
            {
                g.Draw(spriteBatch);
                //PrimitiveDrawing.DrawRectangle(spriteBatch, g.RectangleCollision, Color.White);
            }


            foreach (RemoveGameObject r in _bonusObjecten)
            {
                r.Draw(spriteBatch);
            }

            foreach (Enemy g in _enemyList)
            {
                g.Draw(spriteBatch);
                //PrimitiveDrawing.DrawRectangle(spriteBatch, g.RectangleCollision, Color.White);
            }

            foreach (MoveGameObject t in _bulletList)
            {
                t.Draw(spriteBatch);
            }

            _speler.Draw(spriteBatch);
            //PrimitiveDrawing.DrawRectangle(spriteBatch, _speler.RectangleCollisionHead, Color.Red);
            //PrimitiveDrawing.DrawRectangle(spriteBatch, _speler.RectangleCollisionFeet, Color.Purple);
            //PrimitiveDrawing.DrawRectangle(spriteBatch, _speler.RectangleCollision, Color.Orange);
            //}

            if (_text != null)
                _text.Draw(spriteBatch);

        }



    }

    enum Sleutels
    {
        Red,
        Blue
    }
}
