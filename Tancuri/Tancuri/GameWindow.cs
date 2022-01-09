using System.Drawing;
using System.Windows.Forms;

namespace Tancuri
{
    public partial class GameWindow : Form
    {
        private Tank[] tanks;
        private KeyboardController player1Controller;
        private KeyboardController player2Controller;
        private Map map;

        public GameWindow()
        {
            InitializeComponent();

            // Estalblish the map (only once)
            map = new Map("Maps\\map.txt");
            CollisionHandler.map = map;
            
            RefreshGame(true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            {
                canvas_Paint(this, e);
            }
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            {
                UpdateObjects();
            }
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            // Create graphics
            Graphics g = canvas.CreateGraphics();

            // Draw background
            g.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), new Size(canvas.Width, canvas.Height)));

            //g.DrawLine(Pens.Black, new Point(50, 50), new Point(canvas.Height, canvas.Width));
            map.Paint(g);

            ObjectHandler.Paint(g);
        }

        private void UpdateObjects()
        {
            if (GameWinConditionsHandler.GameIsOn)
            {
                labelWinMessage.Visible = false;

                // Update objects
                ObjectHandler.Update();

                // Check win conditions
                if(player1Controller.ControlledTank.Health <= 0)
                {
                    GameWinConditionsHandler.GameIsOn = false;
                    GameWinConditionsHandler.WinningPlayer = "Player 2";
                }
                if (player2Controller.ControlledTank.Health <= 0)
                {
                    GameWinConditionsHandler.GameIsOn = false;
                    GameWinConditionsHandler.WinningPlayer = "Player 1";
                }

                // For computer controlled players
                player1Controller.Update();
                player2Controller.Update();

                // Update screen labels
                labelPalyer1Ammo.Text = "Ammunition:\n" + player1Controller.ControlledTank.Ammunition;
                labelPlayer1Health.Text = "Health:\n" + player1Controller.ControlledTank.Health;

                labelPlayer2Ammo.Text = "Ammunition:\n" + player2Controller.ControlledTank.Ammunition;
                labelPlayer2Health.Text = "Health:\n" + player2Controller.ControlledTank.Health;
            }
            else
            {
                labelWinMessage.Visible = true;
                timerReload.Enabled = false;
                timerFrame.Enabled = false;
                labelWinMessage.Text = GameWinConditionsHandler.WinningPlayer + " Win!";
            }

        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (GameWinConditionsHandler.GameIsOn)
            {
                // Evaluate the player controller
                player1Controller.KeyDown(e);
                player2Controller.KeyDown(e);
            }
        }

        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (GameWinConditionsHandler.GameIsOn)
            {
                // Evaluate the player controller
                player1Controller.KeyUp(e);
                player2Controller.KeyUp(e);
            }
        }



        private void timerFrame_Tick(object sender, System.EventArgs e)
        {
            // Redraw the scene
            Invalidate();
        }

        private void RefreshGame(bool againstComputer)
        {
            
            // Create the tanks
            tanks = new Tank[]{
                new Tank(new Point(Map.MAP_SIZE/2 - map.TileWidth, map.TileHeight*1)),
                new Tank(new Point(Map.MAP_SIZE/2 - map.TileWidth, map.TileHeight*8))
            };
            tanks[0].TankBrush = Brushes.Blue;
            tanks[1].TankBrush = Brushes.Red;


            // Add to ObjectHandler
            ObjectHandler.DestroyAll();
            ObjectHandler.player1Tank = tanks[0];
            ObjectHandler.player2Tank = tanks[1];
            foreach (Tank tank in tanks)
                ObjectHandler.AllObjects.Enqueue(tank);

            // Attach controllers to tanks
            player1Controller = new Player1Controller(tanks[0]);
            if (againstComputer)
                player2Controller = new ComputerController(tanks[1]);
            else
                player2Controller = new Player2Controller(tanks[1]);

            // Initialize the timer
            timerFrame.Enabled = true;
            timerReload.Enabled = true;

            // Start game
            GameWinConditionsHandler.GameIsOn = true;

            // Focus
            Focus();
        }

        private void buttonRestart_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Against Computer", "Against other Player", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                RefreshGame(true);
            }
            else
            {
                RefreshGame(false);
            }
            // Focus
            Focus();
        }

        private void timerReload_Tick(object sender, System.EventArgs e)
        {
            // Reload 1 ammunotion foreach player
            player1Controller.ControlledTank.Ammunition++;
            player2Controller.ControlledTank.Ammunition++;
        }
    }
}
