using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ChapterOne
{
    /// <summary>
    /// 连连看游戏
    /// </summary>
    public class LinkUpGame
    {
        ///问题描述
        ///1.14 连连看游戏
        ///1.如何用简单的计算模型来描述这个问题
        ///2.怎么判断两个图形是否可以相消？
        ///3.怎样求出相同形状的图形之间最短路径（绕过格子数最少，路径经过的格子最少）
        ///4.如何确定死锁状态？如何通过设计算法来去除死锁状态？
        ///

        ///20100211完成:消去
        ///

        private int _picKind = 3;//图像种类数量
        private int _size = 5;
        private ElementCollection _elements = new ElementCollection();

        public void Start()
        {
            DrawPixel(_elements);
            Play();
        }

        private void Play()
        {
            string line;
            Console.WriteLine("Enter one or more lines of text (press CTRL+Z to exit):");
            Console.WriteLine();
            do
            {
                line = Console.ReadLine();
                if (line != null)
                {
                    //1,2 3,4
                    Coordinate co1 = new Coordinate(Convert.ToInt32(line[0].ToString()), Convert.ToInt32(line[2].ToString()));
                    Coordinate co2 = new Coordinate(Convert.ToInt32(line[4].ToString()), Convert.ToInt32(line[6].ToString()));
                    var ele1 = _elements.Find(co1.XPosition, co1.YPosition);
                    var ele2 = _elements.Find(co2.XPosition, co2.YPosition);

                    if (Eliminate(ele1, ele2))
                    {
                        ele1.Status = ElementStatus.Eliminated;
                        ele2.Status = ElementStatus.Eliminated;
                        DrawPixel(_elements);
                    }
                    else
                        Console.WriteLine("false");
                }
            } while (line != null);
           
        }

        public LinkUpGame(int picKind, int size)
        {
            _picKind = picKind;
            _size = size;
            GenerateElements(picKind, size);
        }

        private void GenerateElements(int kindCount, int size)
        {
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int value = random.Next(0, kindCount);
                    var element = new Element();
                    element.XPosition = j;
                    element.YPosition = i;
                    element.Status = ElementStatus.Normal;
                    element.Position = new Coordinate(j, i);
                    element.Id = i * size + j;
                    element.Name = value.ToString();
                    _elements.Add(element);
                }
            }
        }

        private void DrawPixel(ElementCollection elements)
        {
            int i = 0;
            foreach (var item in elements)
            {
                i++;
                var element = item.Value as Element;
                if (element != null)
                {
                    Console.Write("{0} ", element.Status == ElementStatus.Normal ? element.Name : "x");
                }

                if (i % _size == 0)
                {
                    Console.WriteLine(System.Environment.NewLine);
                }
            }
        }

        private bool Eliminate(Element ele1, Element ele2)//消掉
        {
            if (ele1.Name == ele2.Name)
            {
                //从Ele1出发，寻找直线到ele2
                //查找不用拐弯的：
                var line0 = FindStraightLineElement(new Coordinate(ele1.XPosition, ele1.YPosition), true, true, true, true);

                foreach (var line in line0)
                {
                    if (line.End.XPosition == ele2.XPosition
                        && line.End.YPosition == ele2.YPosition)
                    {
                        return true;
                    }
                }

                //搜索拐一次弯的：
                var line1 = new List<StraightLine>();
                foreach (var line in line0)
                {
                    var element = _elements.Find(line.End.XPosition,line.End.YPosition);
                    if (element == null || element.Status == ElementStatus.Eliminated)
                    {
                        switch (line.FromDirection)
                        {
                            case Direction.Up:
                                line1.AddRange(FindStraightLineElement(line.End, false, true, true, true));
                                break;
                            case Direction.Down:
                                line1.AddRange(FindStraightLineElement(line.End, true, false, true, true));
                                break;
                            case Direction.Left:
                                line1.AddRange(FindStraightLineElement(line.End, true, true, false, true));
                                break;
                            case Direction.Right:
                                line1.AddRange(FindStraightLineElement(line.End, true, true, true, false));
                                break;
                            default:
                                break;
                        }
                    }
                }

                foreach (var line in line1)
                {
                    if (line.End.YPosition == ele2.YPosition && line.End.XPosition == ele2.XPosition)
                    {
                        return true;
                    }
                }

                //搜索转两个弯的：
                var line2 = new List<StraightLine>();
                foreach (var line in line1)
                {
                    var element = _elements.Find(line.End.XPosition, line.End.YPosition);
                    if (element == null || element.Status == ElementStatus.Eliminated)
                    {
                        switch (line.FromDirection)
                        {
                            case Direction.Up:
                                line2.AddRange(FindStraightLineElement(line.End, false, true, true, true));
                                break;
                            case Direction.Down:
                                line2.AddRange(FindStraightLineElement(line.End, true, false, true, true));
                                break;
                            case Direction.Left:
                                line2.AddRange(FindStraightLineElement(line.End, true, true, false, true));
                                break;
                            case Direction.Right:
                                line2.AddRange(FindStraightLineElement(line.End, true, true, true, false));
                                break;
                            default:
                                break;
                        }
                    }
                }

                foreach (var line in line2)
                {
                    if (line.End.YPosition == ele2.YPosition && line.End.XPosition == ele2.XPosition)
                    {
                        return true;
                    }
                }

                return false;
            }

            return false;
        }
        
        /// <summary>
        /// 找到直线能够到达的点
        /// </summary>
        /// <param name="element"></param>
        /// <param name="up"></param>
        /// <param name="down"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private List<StraightLine> FindStraightLineElement(Coordinate startPosition,bool up,bool down,bool left,bool right)
        {
            List<StraightLine> result = new List<StraightLine>();

            bool upFlag = up;
            bool downFlag = down;
            bool leftFlag = left;
            bool rightFlag = right;
        
            var rightIndex = startPosition.XPosition;
            var leftIndex = startPosition.XPosition;
            var upIndex = startPosition.YPosition;
            var downIndex = startPosition.YPosition;

            #region X方向查找
            while (rightFlag || leftFlag)
            {
                rightIndex++;
                leftIndex--;

                if (rightFlag)
                {
                    if (rightIndex < _size)
                    {
                        var line = new StraightLine();
                        line.Start = startPosition;
                        line.End = new Coordinate(rightIndex, startPosition.YPosition);
                        line.FromDirection = Direction.Right;
                        result.Add(line);

                        var ele = _elements.Find(rightIndex, startPosition.YPosition);

                        if (ele != null && ele.Status == ElementStatus.Normal)
                            rightFlag = false;
                    }
                    else if (rightIndex == _size)
                    {
                        var line = new StraightLine();
                        line.Start = startPosition;
                        line.End = new Coordinate(rightIndex, startPosition.YPosition);
                        line.FromDirection = Direction.Right;
                        result.Add(line);

                        rightFlag = false;
                    }
                    else
                        rightFlag = false;
                }

                if (leftFlag)
                {
                    if (leftIndex > -1)
                    {
                        var line = new StraightLine();
                        line.Start = startPosition;
                        line.End = new Coordinate(leftIndex, startPosition.YPosition);
                        line.FromDirection = Direction.Left;
                        result.Add(line);

                        var ele = _elements.Find(leftIndex, startPosition.YPosition);
                        if (ele != null && ele.Status == ElementStatus.Normal)
                            leftFlag = false;
                    }
                    else if (leftIndex == -1)
                    {
                        var line = new StraightLine();
                        line.Start = startPosition;
                        line.End = new Coordinate(leftIndex, startPosition.YPosition);
                        line.FromDirection = Direction.Left;
                        result.Add(line);

                        leftFlag = false;
                    }
                    else
                        leftFlag = false;
                }
            }
            #endregion

            #region Y
            while (upFlag || downFlag)
            {
                upIndex++;
                downIndex--;

                if (upFlag)
                {
                    if (upIndex < _size)
                    {
                        var line = new StraightLine();
                        line.Start = startPosition;
                        line.End = new Coordinate(startPosition.XPosition, upIndex);
                        line.FromDirection = Direction.Up;
                        result.Add(line);

                        var ele = _elements.Find(startPosition.XPosition, upIndex);
                        if (ele != null && ele.Status == ElementStatus.Normal)
                            upFlag = false;
                    }
                    else if (upIndex == _size)
                    {
                        var line = new StraightLine();
                        line.Start = startPosition;
                        line.End = new Coordinate(startPosition.XPosition, upIndex);
                        line.FromDirection = Direction.Up;
                        result.Add(line);
                        upFlag = false;
                    }
                    else
                        upFlag = false;
                }

                if (downFlag)
                {
                    if (downIndex > -1)
                    {
                        var line = new StraightLine();
                        line.Start = startPosition;
                        line.End = new Coordinate(startPosition.XPosition, downIndex);
                        line.FromDirection = Direction.Down;
                        result.Add(line);

                        var ele = _elements.Find(startPosition.XPosition, downIndex);
                        if (ele != null && ele.Status == ElementStatus.Normal)
                            downFlag = false;
                    }
                    else if (downIndex == -1)
                    {
                        var line = new StraightLine();
                        line.Start = startPosition;
                        line.End = new Coordinate(startPosition.XPosition, downIndex);
                        line.FromDirection = Direction.Down;
                        result.Add(line);

                        downFlag = false;
                    }
                    else
                        downFlag = false;
                }
            }
            #endregion

            return result;
        }

        private void MakeSureNoLock(List<Element> elements)
        {
            throw new NotImplementedException();
        }

        public class ElementCollection : Martrix
        {
            public Element Find(int x, int y)
            {
                return base.Get(x, y) as Element;
            }

            public void Add(Element element)
            {
                base.Add(element.XPosition, element.YPosition, element);
            }
        }

        public class StraightLine
        {
            public StraightLine() { }

            public StraightLine(Coordinate start, Coordinate end, Direction fromDirection)
            {
                Start = start;
                End = end;
                FromDirection = fromDirection;
            }
            public Coordinate Start { get; set; }
            public Coordinate End { get; set; }
            public Direction FromDirection { get; set; }
        }

        public class Coordinate
        {
            public Coordinate(int x, int y)
            {
                XPosition = x;
                YPosition = y;
            }

            public int XPosition { get; set; }
            public int YPosition { get; set; }
        }

        [Flags]
        public enum Direction
        {
            Up = 1,
            Down = 2,
            Left = 4,
            Right = 8,
        }

        public class Element
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public Coordinate Position { get; set; }
            public int XPosition { get; set; }
            public int YPosition { get; set; }
            public ElementStatus Status { get; set; }
        }

        public enum ElementStatus
        {
            Normal,
            Eliminated,
        }

        public class Martrix : Dictionary<string,object>
        {
            private string MakeKey(int x,int y)
            {
                return x.ToString() + "-" + y.ToString();
            }

            public void Add(int x, int y,object value)
            {
                base.Add(MakeKey(x, y), value);
            }

            public Object Get(int x, int y)
            {
                object val = new object();
                base.TryGetValue(MakeKey(x, y), out val);
                return val;
            }

            public void Set(int x, int y, object value)
            {
                base.Add(MakeKey(x, y), value);
            }
           
        }
    }
}
