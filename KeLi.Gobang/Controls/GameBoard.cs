using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using KeLi.Gobang.Models;
using KeLi.Gobang.Properties;

namespace KeLi.Gobang.Controls
{
    public class GameBoard : Panel
    {
        private const int SIDE_LENGTH = (GAME_SIZE - 1) * BLOCK_SIZE + OFFSET * 2;

        private const int GAME_SIZE = 15;

        private const int BLOCK_SIZE = 30;

        private const int CHESSMAN_SIZE = 26;

        private const int OFFSET = 50;

        private const int DISTANCE_SQUARE = 100;

        private const int TEXT_SIZE = 10;

        private const int _black = 1;

        private const int _white = -1;

        private int _hSum;

        private bool _isStart;

        private int _lSum;

        private int[,] _map = new int[GAME_SIZE, GAME_SIZE];

        private int _rSum;

        private int _vSum;

        public GameBoard()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            UpdateStyles();
            SetAdaptiveSize();
            DrawGameBoard();
        }

        public virtual event EventHandler<GameOverEventArgs> OnGameOver;

        private void SetAdaptiveSize()
        {
            Size = new Size(SIDE_LENGTH, SIDE_LENGTH);
        }

        private bool IsGameOver(Point point, int color)
        {
            _hSum = 1;

            for (var i = point.X - 1; i >= 0; i--)
            {
                var item = new Point(i, point.Y);

                if (!IsSamePlayer(item, color))
                    break;

                _hSum++;
            }

            for (var i = point.X + 1; i <= GAME_SIZE; i++)
            {
                var item = new Point(i, point.Y);

                if (!IsSamePlayer(item, color))
                    break;

                _hSum++;
            }

            _vSum = 1;

            for (var i = point.Y - 1; i >= 0; i--)
            {
                var item = new Point(point.X, i);

                if (!IsSamePlayer(item, color))
                    break;

                _vSum++;
            }

            for (var i = point.Y + 1; i <= GAME_SIZE; i++)
            {
                var item = new Point(point.X, i);

                if (!IsSamePlayer(item, color))
                    break;

                _vSum++;
            }

            _lSum = 1;

            for (int i = point.X - 1, j = point.Y - 1; i >= 0 && j >= 0; i--, j--)
            {
                var item = new Point(i, j);

                if (!IsSamePlayer(item, color))
                    break;

                _lSum++;
            }

            for (int i = point.X + 1, j = point.Y + 1; i <= GAME_SIZE && j <= GAME_SIZE; i++, j++)
            {
                var item = new Point(i, j);

                if (!IsSamePlayer(item, color))
                    break;

                _lSum++;
            }

            _rSum = 1;

            for (int i = point.X - 1, j = point.Y + 1; i >= 0 && j <= GAME_SIZE; i--, j++)
            {
                var item = new Point(i, j);

                if (!IsSamePlayer(item, color))
                    break;

                _rSum++;
            }

            for (int i = point.X + 1, j = point.Y - 1; i <= GAME_SIZE && j >= 0; i++, j--)
            {
                var item = new Point(i, j);

                if (!IsSamePlayer(item, color))
                    break;

                _rSum++;
            }

            return _hSum >= 5 || _vSum >= 5 || _lSum >= 5 || _rSum >= 5;
        }

        private void AddChessman(Point point, int color)
        {
            _map[point.X, point.Y] = color;

            var font = new Font(FontFamily.GenericMonospace, TEXT_SIZE, FontStyle.Bold);
            var image = color == 1 ? Resources.Black : Resources.White;

            var format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            var screenPoint = ToScreenPoint(point.X, point.Y);

            using (var g = Graphics.FromImage(BackgroundImage))
            {
                screenPoint.X -= CHESSMAN_SIZE / 2;
                screenPoint.Y -= CHESSMAN_SIZE / 2;

                g.DrawImage(image, screenPoint.X + 2, screenPoint.Y + 2, CHESSMAN_SIZE, CHESSMAN_SIZE);

                var chessmanCount = _map.Cast<int>().Count(w => w != 0);
                var hRectangle = new Rectangle(screenPoint.X, screenPoint.Y, BLOCK_SIZE, BLOCK_SIZE);

                if (color == _black)
                    g.DrawString(chessmanCount.ToString(), font, new SolidBrush(Color.White), hRectangle, format);

                else
                    g.DrawString(chessmanCount.ToString(), font, new SolidBrush(Color.Black), hRectangle, format);
            }

            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!_isStart || e.Button != MouseButtons.Left)
            {
                base.OnMouseClick(e);

                return;
            }

            var screenPoint = new Point(e.X, e.Y);

            if (!IsValid(screenPoint, out var playerPoint))
            {
                base.OnMouseClick(e);

                return;
            }

            AddChessman(playerPoint, _black);

            if (IsGameOver(playerPoint, _black))
            {
                OnGameOver?.Invoke(this, new GameOverEventArgs(_black));
                _isStart = false;
            }

            else
            {
                var computerPoint = ComputeNextPoint();

                AddChessman(computerPoint, _white);

                if (IsGameOver(computerPoint, _white))
                {
                    OnGameOver?.Invoke(this, new GameOverEventArgs(_white));
                    _isStart = false;
                }
            }

            base.OnMouseClick(e);
        }

        public void StartGame()
        {
            _isStart = true;
        }

        public void RestartGame()
        {
            DrawGameBoard();

            _map = new int[GAME_SIZE + 1, GAME_SIZE + 1];
            _isStart = true;
        }

        private void DrawGameBoard()
        {
            var image = new Bitmap(SIDE_LENGTH, SIDE_LENGTH);

            using (var graphics = Graphics.FromImage(image))
            {
                DrawGrids(graphics);
                DrawCircles(graphics);
                DrawGridTitles(graphics);
            }

            BackgroundImage = image;
        }

        private void DrawGrids(Graphics g)
        {
            g.FillRectangle(Brushes.BurlyWood, new Rectangle(0, 0, SIDE_LENGTH, SIDE_LENGTH));

            var pen = new Pen(Color.Black, 1);

            for (var i = 0; i < GAME_SIZE; i++)
            {
                var hStartPoint = ToScreenPoint(0, i);
                var hEndPoint = ToScreenPoint(GAME_SIZE - 1, i);

                g.DrawLine(pen, hStartPoint, hEndPoint);

                var vStartPoint = ToScreenPoint(i, 0);
                var vEndPoint = ToScreenPoint(i, GAME_SIZE - 1);

                g.DrawLine(pen, vStartPoint, vEndPoint);
            }
        }

        private void DrawCircles(Graphics g)
        {
            var brush = new SolidBrush(Color.Black);
            var p1 = ToScreenPoint(3, 3);
            var p2 = ToScreenPoint(12, 3);
            var p3 = ToScreenPoint(3, 12);
            var p4 = ToScreenPoint(12, 12);

            g.FillEllipse(brush, p1.X - 3, p1.Y - 3, 6, 6);
            g.FillEllipse(brush, p2.X - 3, p2.Y - 3, 6, 6);
            g.FillEllipse(brush, p3.X - 3, p3.Y - 3, 6, 6);
            g.FillEllipse(brush, p4.X - 3, p4.Y - 3, 6, 6);
        }

        private void DrawGridTitles(Graphics g)
        {
            var font = new Font(FontFamily.GenericMonospace, TEXT_SIZE, FontStyle.Bold);
            var brush = new SolidBrush(Color.Black);

            var format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            for (var i = 0; i < GAME_SIZE; i++)
            {
                var hPoint = ToScreenPoint(i, 0);
                var vPoint = ToScreenPoint(0, i);
                var hRectangle = new Rectangle(hPoint.X - BLOCK_SIZE / 2, hPoint.Y - BLOCK_SIZE - CHESSMAN_SIZE / 2, BLOCK_SIZE, BLOCK_SIZE);
                var vRectangle = new Rectangle(vPoint.X - BLOCK_SIZE - CHESSMAN_SIZE / 2, vPoint.Y - BLOCK_SIZE / 2, BLOCK_SIZE, BLOCK_SIZE);

                g.DrawString((i + 1).ToString(), font, brush, hRectangle, format);
                g.DrawString((i + 1).ToString(), font, brush, vRectangle, format);
            }
        }

        private Point ToScreenPoint(int xIndex, int yIndex)
        {
            return new Point(xIndex * BLOCK_SIZE + OFFSET, yIndex * BLOCK_SIZE + OFFSET);
        }

        private bool CrossBorder(Point point)
        {
            return point.X < 0 || point.X >= GAME_SIZE || point.Y < 0 || point.Y >= GAME_SIZE;
        }

        private bool IsSamePlayer(Point point, int color)
        {
            return !CrossBorder(point) && _map[point.X,point.Y] == color;
        }

        private bool IsEmpty(Point point)
        {
            return !CrossBorder(point) && _map[point.Y, point.X] == 0;
        }

        private bool IsValid(Point screenPoint, out Point boardPoint)
        {
            int ComputeDistanceSquare(Point current, Point another)
            {
                var deltaX = current.X - another.X;
                var deltaY = current.Y - another.Y;

                return deltaX * deltaX + deltaY * deltaY;
            }

            boardPoint = default;

            for (var i = 0; i <= GAME_SIZE; i++)
            {
                for (var j = 0; j <= GAME_SIZE; j++)
                {
                    var currentPoint = ToScreenPoint(i, j);
                    var distance = ComputeDistanceSquare(currentPoint, screenPoint);

                    if (distance <= DISTANCE_SQUARE)
                    {
                        if (_map[i, j] != 0)
                            return false;

                        boardPoint.X = i;
                        boardPoint.Y = j;

                        return true;
                    }
                }
            }

            return false;
        }

        private Point ComputeNextPoint()
        {
            var whiteMap = new int[GAME_SIZE, GAME_SIZE];
            var blackMap = new int[GAME_SIZE, GAME_SIZE];

            for (var i = 0; i < GAME_SIZE; i++)
            {
                for (var j = 0; j < GAME_SIZE; j++)
                {
                    if (_map[i, j] != 0)
                        continue;

                    var point = new Point(i, j);

                    whiteMap[i, j] = Evaluate(point, _white);
                    blackMap[i, j] = Evaluate(point, _black);
                }
            }

            var whitePoint = GetBestPoint(whiteMap);
            var blackPoint = GetBestPoint(blackMap);

            if (whitePoint != blackPoint)
                return blackMap[blackPoint.X, blackPoint.Y] > whiteMap[whitePoint.X, whitePoint.Y] * 3 ? blackPoint : whitePoint;

            return whitePoint;
        }

        private Point GetBestPoint(int[,] map)
        {
            var result = new Point();

            for (var i = 0; i < GAME_SIZE; i++)
            {
                for (var j = 0; j < GAME_SIZE; j++)
                {
                    if (map[i, j] <= map[result.X, result.Y])
                        continue;

                    result.X = i;
                    result.Y = j;
                }
            }

            return result;
        }

        private int Evaluate(Point point, int color)
        {
            var result = (int)GetSituationType(point, SituationDirection.Horizontal, color);

            result += (int)GetSituationType(point, SituationDirection.Vertical, color);
            result += (int)GetSituationType(point, SituationDirection.Left, color);
            result += (int)GetSituationType(point, SituationDirection.Right, color);

            return result;
        }

        private SituationType GetSituationType(Point point, SituationDirection direction, int color)
        {
            var result = SituationType.Other;

            for (var i = 0; i > -5; i--)
            {
                Point p1;
                Point p2;
                Point p3;
                Point p4;
                Point p5;

                switch (direction)
                {
                    case SituationDirection.Vertical:
                        p1 = new Point(point.X, point.Y + i + 0);
                        p2 = new Point(point.X, point.Y + i + 1);
                        p3 = new Point(point.X, point.Y + i + 2);
                        p4 = new Point(point.X, point.Y + i + 3);
                        p5 = new Point(point.X, point.Y + i + 4);

                        break;
                    case SituationDirection.Horizontal:
                        p1 = new Point(point.X + i + 0, point.Y);
                        p2 = new Point(point.X + i + 1, point.Y);
                        p3 = new Point(point.X + i + 2, point.Y);
                        p4 = new Point(point.X + i + 3, point.Y);
                        p5 = new Point(point.X + i + 4, point.Y);

                        break;
                    case SituationDirection.Left:
                        p1 = new Point(point.X + i + 0, point.Y + i + 0);
                        p2 = new Point(point.X + i + 1, point.Y + i + 1);
                        p3 = new Point(point.X + i + 2, point.Y + i + 2);
                        p4 = new Point(point.X + i + 3, point.Y + i + 3);
                        p5 = new Point(point.X + i + 4, point.Y + i + 4);

                        break;
                    case SituationDirection.Right:
                        p1 = new Point(point.X + i + 0, point.Y - (i + 0));
                        p2 = new Point(point.X + i + 1, point.Y - (i + 1));
                        p3 = new Point(point.X + i + 2, point.Y - (i + 2));
                        p4 = new Point(point.X + i + 3, point.Y - (i + 3));
                        p5 = new Point(point.X + i + 4, point.Y - (i + 4));

                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }

                var currentType = GetSituationType(p1, p2, p3, p4, p5, color);

                if ((int)currentType > (int)result)
                    result = currentType;
            }

            return result;
        }

        private SituationType GetSituationType(Point p1, Point p2, Point p3, Point p4, Point p5, int color)
        {
            var oneSide = IsEmpty(p1) && !IsEmpty(p5) || IsEmpty(p5) && !IsEmpty(p1);

            // Be 5.
            {
                if (IsSamePlayer(p1, color) && IsSamePlayer(p2, color) && IsSamePlayer(p3, color) && IsSamePlayer(p4, color))
                    return SituationType.BeFive;
            }

            // Active 4 or Rush 4
            {
                if (IsEmpty(p1) && IsSamePlayer(p2, color) && IsSamePlayer(p3, color) && IsSamePlayer(p4, color) && IsEmpty(p5))
                    return SituationType.ActiveFour;

                if (IsEmpty(p1) && IsSamePlayer(p2, color) && IsSamePlayer(p3, color) && IsSamePlayer(p4, color))
                    return SituationType.RushFour;

                if (IsSamePlayer(p1, color) && IsEmpty(p2) && IsSamePlayer(p3, color) && IsSamePlayer(p4, color))
                    return SituationType.RushFour;

                if (IsSamePlayer(p1, color) && IsSamePlayer(p2, color) && IsEmpty(p3) && IsSamePlayer(p4, color))
                    return SituationType.RushFour;

                if (IsSamePlayer(p1, color) && IsSamePlayer(p2, color) && IsSamePlayer(p3, color) && IsEmpty(p4))
                    return SituationType.RushFour;
            }

            // Active 3 or Sleep 3
            {
                if (IsEmpty(p1) && IsEmpty(p2) && IsSamePlayer(p3, color) && IsSamePlayer(p4, color) && IsEmpty(p5))
                    return SituationType.ActiveThree;

                if (IsEmpty(p1) && IsSamePlayer(p2, color) && IsEmpty(p3) && IsSamePlayer(p4, color) && IsEmpty(p5))
                    return SituationType.ActiveThree;

                if (IsEmpty(p1) && IsSamePlayer(p2, color) && IsSamePlayer(p3, color) && IsEmpty(p4) && IsEmpty(p5))
                    return SituationType.ActiveThree;

                if (IsEmpty(p2) && IsSamePlayer(p3, color) && IsSamePlayer(p4, color) && oneSide)
                    return SituationType.SleepThree;

                if (IsSamePlayer(p2, color) && IsEmpty(p3) && IsSamePlayer(p4, color) && oneSide)
                    return SituationType.SleepThree;

                if (IsSamePlayer(p2, color) && IsSamePlayer(p3, color) && IsEmpty(p4) && oneSide)
                    return SituationType.SleepThree;
            }

            // Active 2 or Sleep 2
            {
                if (IsEmpty(p1) && IsSamePlayer(p2, color) && IsEmpty(p3) && IsEmpty(p4) && IsEmpty(p5))
                    return SituationType.ActiveTwo;

                if (IsEmpty(p1) && IsEmpty(p2) && IsSamePlayer(p3, color) && IsEmpty(p4) && IsEmpty(p5))
                    return SituationType.ActiveTwo;

                if (IsEmpty(p1) && IsEmpty(p2) && IsEmpty(p3) && IsSamePlayer(p4, color) && IsEmpty(p5))
                    return SituationType.ActiveTwo;

                if (IsSamePlayer(p2, color) && IsEmpty(p3) && IsEmpty(p4) && oneSide)
                    return SituationType.SleepTwo;

                if (IsEmpty(p2) && IsSamePlayer(p3, color) && IsEmpty(p4) && oneSide)
                    return SituationType.SleepTwo;

                if (IsEmpty(p2) && IsEmpty(p3) && IsSamePlayer(p4, color) && oneSide)
                    return SituationType.SleepTwo;
            }

            return SituationType.Other;
        }
    }
}