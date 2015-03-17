using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BlockSlideCore.Analysis;
using BlockSlideCore.Engine;
using BlockSlideCore.Entities;
using BlockSlideCore.Levels;

namespace BlockSlideGUI
{
    public partial class LevelPanel : UserControl
    {
        public Level Level { get; private set; }

        public int TileSize { get; set; }

        public Form ParentForm { get; set; }

        private int mLevelNumber;

        private readonly Bitmap mPlayerSprite;

        public LevelPanel()
        {
            InitializeComponent();
            mLevelNumber = 1;
            SetupLevel();
            try
            {
                mPlayerSprite = new Bitmap("Resources/awesomeface.png");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Failed to load the player sprite: {0}", ex.Message));
            }
        }

        public void SetupLevel()
        {
            if (ParentForm != null)
            {
                ParentForm.Text = RandomLevelMode
                    ? "BlockSlide - Random Level"
                    : string.Format("BlockSlide - Level {0}", mLevelNumber);
            }
            Level = RandomLevelMode
                ? new Level(new Random().Next(5000), new RandomLevelBuilder(18, 10))
                : new Level(mLevelNumber);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var brushMap = new Dictionary<TileType, Brush>
            {
                {TileType.Floor, new SolidBrush(Color.AliceBlue)},
                {TileType.Wall, new SolidBrush(Color.MidnightBlue)},
                {TileType.Start, new SolidBrush(Color.Lime)},
                {TileType.Finish, new SolidBrush(Color.Red)},
            };
            Level.LevelGrid.ForEach((x, y, value) =>
                e.Graphics.FillRectangle(brushMap[value],
                    x*TileSize, y*TileSize, TileSize, TileSize));

            DrawPlayer(e.Graphics);
            if (ShowBestPath)
            {
                DrawBestPath(e.Graphics);
            }
        }

        private void DrawBestPath(Graphics graphics)
        {
            var graphNode = new GraphBuilder().BuildGraph(Level);
            var bestPath = new ShortestPathFinder().CalculateShortestPathInformation(graphNode,
                Level.StartLocation).GetPath(Level.FinishLocation);
            bestPath.Insert(0, Level.StartLocation);
            var bestLinePen = new Pen(Color.Magenta);
            for (var i = 0; i < bestPath.Count - 1; i++)
            {
                var item = bestPath[i];
                var nextItem = bestPath[i + 1];
                graphics.DrawLine(bestLinePen, item.X*TileSize + TileSize/2, item.Y*TileSize + TileSize/2,
                    nextItem.X*TileSize + TileSize/2, nextItem.Y*TileSize + TileSize/2);
            }
        }

        private void DrawPlayer(Graphics graphics)
        {
            if (mPlayerSprite != null)
            {
                graphics.DrawImage(mPlayerSprite,
                    Level.PlayerLocation.X*TileSize, Level.PlayerLocation.Y*TileSize,
                    TileSize, TileSize);
            }
            else
            {
                var playerBodyBrush = new SolidBrush(Color.Yellow);
                graphics.FillEllipse(playerBodyBrush,
                    Level.PlayerLocation.X*TileSize, Level.PlayerLocation.Y*TileSize,
                    TileSize, TileSize);
            }
        }

        private void LevelPanel_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'a':
                    Level.Move(Direction.Left);
                    break;
                case 's':
                    Level.Move(Direction.Down);
                    break;
                case 'd':
                    Level.Move(Direction.Right);
                    break;
                case 'w':
                    Level.Move(Direction.Up);
                    break;
                case 'r':
                    SetupLevel();
                    break;
                case 'n':
                    NextLevel();
                    break;
                case 'p':
                    Level.ResetPlayerLocation();
                    break;
                case 'b':
                    PreviousLevel();
                    break;
                case 'h':
                    ShowBestPath = !ShowBestPath;
                    break;
                case 'y':
                    RandomLevelMode = !RandomLevelMode;
                    SetupLevel();
                    break;
            }
            GameLoop();
        }

        
        private void GameLoop()
        {
            if (Level.FinishLocation.Equals(Level.PlayerLocation))
            {
                if (mLevelNumber == 100)
                {
                    MessageBox.Show("You win! Resetting to level 1.");
                    mLevelNumber = 1;
                    SetupLevel();
                }
                else
                {
                    NextLevel();
                }
            }
            Refresh();
        }

        private void NextLevel()
        {
            if (mLevelNumber < 100)
            {
                mLevelNumber++;
                SetupLevel();
            }
        }

        private void PreviousLevel()
        {
            if (mLevelNumber > 1)
            {
                mLevelNumber--;
                SetupLevel();
            }
        }

        private bool RandomLevelMode { get; set; }

        private bool ShowBestPath { get; set; }
    }
}
