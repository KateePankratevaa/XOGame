using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XO_Game_Pankrateva
{
    public class XOField
    {
        private const int rowCount = 3, colCount = 3;
        private int[,] rawValues = new int[rowCount, colCount];
        //последний ход нолика
        private XOElement lastturn = XOElement.Circle;
        private XOElement winner = XOElement.None;

        public enum XOElement { None = 0, Cross = 1, Circle = -1 }
        private XOElement reverseElement(XOElement element)
        {
            return (XOElement) (-(int) element);
        }

        public bool GameOver
        {
            get
            {
                return winner != XOElement.None;            }
        }

        public bool CanTurn(int row,int col)
        {
            return (XOElement)  rawValues[row, col] == XOElement.None && !GameOver;
        }

        public bool TryTurn(int row, int col)
        {
           if(!CanTurn(row,col))
            {
                return false;
            }

            rawValues[row, col] = (int) reverseElement(lastturn);
            lastturn = (XOElement) rawValues[row, col];
            Winner = checkWinner();
            return true;
        }
      

        public char[,] Field
        {
            get
            {
                char[,] result = new char[rowCount, colCount];
                for(int i=0;i<rowCount;i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if ((XOElement)  rawValues[i, j] == XOElement.Cross)
                            result[i, j] = 'X';
                        else if ((XOElement)  rawValues[i, j] == XOElement.Circle)
                            result[i, j] = 'O';
                        else
                            result[i, j] = '-';

                    }

                }
                return result;
            }
        }

        public XOElement Winner
        {
            get
            {
                return winner;
            }

            private set
            {
                winner = value;
            }
        }

        private XOElement checkRows ()
        {
            for (int i = 0; i < rowCount; i++)
            {
                int sum = 0;
                for (int j = 0; j < colCount; j++)
                {
                    sum += (int) rawValues[i, j];
                }
                if (sum == 3)
                {
                    return XOElement.Cross;
                }
                if (sum == -3)
                {
                    return XOElement.Circle;
                }
            }
            return XOElement.None;
        }
        private XOElement checkWinner()
        {
            var winner = checkRows();
            if (winner!=XOElement.None)
            {
                return winner;
            }
            return XOElement.None;
        }
    }
}
