using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunWithValueAndRefType
{
    class Program
    {
        static void Main(string[] args)
        {
            //1:
            //ValueTypeContainRefType();

            //2:
            RefPassByValue();
            Console.ReadLine();
        }

        #region RefPassByValueAndRef
        static void RefPassByValue()
        {
            ShapeInfo info1 = new ShapeInfo("info1");
            info1.Display();
            RefPassByValueModify(info1);
            info1.Display();
            RefPassByRefModify(ref info1);
            info1.Display();
        }
        static void RefPassByValueModify(ShapeInfo info)
        {
            info.InfoString = "infoString_modified";
            info = new ShapeInfo("info_modified!");
        }
        static void RefPassByRefModify(ref ShapeInfo info)
        {
            info.InfoString = "infoString_modified";
            info = new ShapeInfo("info_modified!");
            //info.InfoString = "infoString_modified";
        }
        #endregion
       
        #region ValueTypeContainRefType
        static void ValueTypeContainRefType()
        {

            //Create first rectangle
            Rectangle r1 = new Rectangle("info1", 1, 1, 1, 1);
            r1.Display();
            Rectangle r2 = new Rectangle("info2", 2, 2, 2, 2);
            r2.Display();

            //assign:
            r1.SInfo = r2.SInfo;
            r2.SInfo.InfoString = "info2_modify";

            r1.Display(); r2.Display();
        }
        #endregion
        
    }

    class ShapeInfo
    {
        public string InfoString { get; set; }
        public ShapeInfo(string info)
        {
            InfoString = info;
        }
        public void Display()
        {
            Console.WriteLine("info is {0}", InfoString);
        }
    }

    struct Rectangle
    {
        public ShapeInfo SInfo;
        public int Left;
        public int Right;
        public int Top;
        public int Bottom;

        public Rectangle(string info, int left, int right, int top, int bottom)
        {
            Left = left; Right = right; Top = top; Bottom = bottom;
            SInfo = new ShapeInfo(info);
        }

        public void Display()
        {
            Console.WriteLine("info is {0},Left is {1},right is {2}", SInfo.InfoString, Left, Right);
        }
    }
}