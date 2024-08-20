using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CS_Snake_Game
{
    public partial class Form1 : Form
    {
        private List<circle> snake = new List<circle>();
        private circle food = new circle();
        int maxWidth;
        int maxHeight;

        int score;
        int highScore;

        Random rand = new Random();

        bool goLeft,goRight,goUp,goDown;

        
        public Form1()
        {
            InitializeComponent();
            new settings();
        }
        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left && settings.direction != "right")
            {
                goLeft = true;
            }
            if(e.KeyCode == Keys.Right && settings.direction != "left")
            {
                goRight = true;
            }
            if(e.KeyCode == Keys.Up && settings.direction != "down")
            {
                goUp = true;
            }
            if(e.KeyCode == Keys.Down && settings.direction != "up")
            {
                goDown = true;
            }
        }
        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            restartGame();
        }
       
        private void timer_Tick(object sender, EventArgs e)
        {
           //setting the direction.

            if(goLeft == true)
            {
                settings.direction = "left";
            }
            if(goRight == true)
            {
                settings.direction = "right";
            }
            if(goUp == true)
            {
                settings.direction = "up";
            }
            if(goDown == true)
            {
                settings.direction = "down";
            }
            //end of direction.

            for(int i = snake.Count - 1;i>= 0; i--)
            {
                if(i == 0)
                {
                    switch(settings.direction)
                    {
                        case "left":
                            {
                                snake[i].X--;
                                break;
                            }
                        case "right":
                            {
                                snake[i].X++;
                                break;
                            }
                        case "up":
                            {
                                snake[i].Y--;
                                break;
                            }
                        case "down":
                            {
                                snake[i].Y++;
                                break;
                            }
                    }

                    if(snake[i].X < 0)
                    {
                        snake[i].X = maxWidth;
                    }
                    if(snake[i].X > maxWidth)
                    {
                        snake[i].X = 0;
                    }
                    if(snake[i].Y < 0)
                    {
                        snake[i].Y = maxHeight;
                    }
                    if(snake[i].Y > maxHeight)
                    {
                        snake[i].Y = 0;
                    }

                    if(snake[i].X == food.X && snake[i].Y == food.Y)
                    {
                        eatFood();
                    }

                    for(int j = 1;j < snake.Count;j++)
                    {
                        if(snake[i].X == snake[j].X && snake[i].Y == snake[j].Y)
                        {
                            gameOver();
                        }
                    }


                }
                else
                {
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }
            }
            picCanvas.Invalidate();

        }
        private void updatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            Brush snakeColor;

            for(int i = 0; i < snake.Count;i++)
            {
                if(i == 0)
                {
                    snakeColor = Brushes.Black;
                }
                else
                {
                    snakeColor = Brushes.DarkGreen;
                }

                canvas.FillEllipse(snakeColor, new Rectangle
                    (
                        snake[i].X * settings.width,
                        snake[i].Y * settings.hight,
                        settings.width,settings.hight
                    ));
            }

            canvas.FillEllipse(Brushes.DarkRed, new Rectangle
                (
                food.X * settings.width,
                food.Y * settings.hight,
                settings.width, settings.hight
                ));



        }
        private void restartGame()
        {
            maxWidth = picCanvas.Width / settings.width - 1;
            maxHeight = picCanvas.Height / settings.hight - 1;

            snake.Clear();

            btnStart.Enabled = false;
            

            score = 0;
            txtScore.Text = "score: " + score;

            circle head = new circle { X = 10, Y = 5 };
            snake.Add(head);//adding the head part of the snake to the snake list.

            for(int i = 0; i < 8; i++)
            {
                circle body = new circle();
                snake.Add(body);
            }

            food = new circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight)};

            timer.Start();
        }
        private void eatFood()
        {
            score += 1;
            txtScore.Text = "score: " + score;

            circle body = new circle
            {
                X = snake[snake.Count - 1].X,
                Y = snake[snake.Count - 1].Y,
            };

            snake.Add(body);

            food = new circle { X = rand.Next(2, maxWidth), Y = rand.Next(2, maxHeight) };

            timer.Interval--;

        }
        private void gameOver()
        {
            timer.Stop();

           
            btnStart.Enabled = true;

            if(score > highScore)
            {
                highScore = score;
                txtHighScore.Text = "high score: " + Environment.NewLine + highScore;
                txtHighScore.ForeColor = Color.Maroon;
                txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
            }




        }
    }
}
