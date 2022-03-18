using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveFunction
{
    internal class Program
    {
        /******************************************************************************/
        // フィールド変数

        private const int boardSize = 9;

        // 碁盤表示用文字配列      空点=0,黒石=1,白石=2,盤外=3,連=4,ダメ=5
        private static Char[] chr = { '・', '○', '●', '？', '◎', '×' };

        // 確認用サンプル盤面配列
        private static int[,] array = new int[boardSize + 2, boardSize + 2]
        {
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 2, 3, },
            { 3, 0, 1, 1, 0, 0, 0, 0, 0, 2, 3, },
            { 3, 0, 0, 1, 1, 0, 2, 1, 0, 0, 3, },
            { 3, 0, 0, 0, 0, 2, 1, 2, 0, 0, 3, },
            { 3, 0, 2, 1, 1, 2, 1, 1, 2, 0, 3, },
            { 3, 0, 0, 0, 1, 0, 2, 1, 2, 0, 3, },
            { 3, 0, 0, 2, 2, 0, 2, 2, 0, 0, 3, },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, },
            { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, },
            { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, },


        };

        //チェック用盤面配列
        private static int[,] checkArray = new int[boardSize + 2, boardSize + 2];


        /******************************************************************************/
        /// <summary>
        /// 座標(x,y)のcolor石の空点(ダメ)数を返す再帰的メソッド。
        /// 最終的に連(color石の集合)の空点(ダメ)数が返される。
        /// 盤面配列aryは、連を数値4でマークし、連の空点を数値5でマークする。
        /// 
        /// </summary>
        /// <param name="ary">盤面の2次元配列</param>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="color">連の石の色</param>
        /// <param name="count">戻り値：空点の数を再帰的にカウントする</param>
        /// <returns>count</returns>
        private static int GetNumDaMe(ref int[,] ary, int x, int y, int color, int count)
        {
            //再帰しない条件の処理
            if (x < 1 || x > boardSize) return count;   //ｘが範囲外、何もせずカウントだけ返す
            if (y < 1 || y > boardSize) return count;   //ｙが範囲外、何もせずカウントだけ返す
            if (ary[y, x] >= 4) return count;           // 座標がチェック済 4or5 なら、何もせずカウントだけ返す
            if (ary[y, x] != color) return count;       // 座標に連の石が無いなら、何もせずカウントだけ返す

            //カウント処理   座標の隣が空点なら、チェック済みマーク 5 を入れ、カウント＋１
            if (ary[y, x - 1] == 0) { ary[y, x - 1] = 5; count++; } //左隣
            if (ary[y, x + 1] == 0) { ary[y, x + 1] = 5; count++; } //右隣
            if (ary[y - 1, x] == 0) { ary[y - 1, x] = 5; count++; } //上隣
            if (ary[y + 1, x] == 0) { ary[y + 1, x] = 5; count++; } //下隣
            ary[y, x] = 4;          // 座標をチェック済みマークとして、4 を入れる

            //再帰呼び出し   隣の要素をチェックする為に、再帰呼び出し
            count = GetNumDaMe(ref ary, x - 1, y, color, count);    //左隣
            count = GetNumDaMe(ref ary, x + 1, y, color, count);    //右隣
            count = GetNumDaMe(ref ary, x, y - 1, color, count);    //上隣
            count = GetNumDaMe(ref ary, x, y + 1, color, count);    //下隣
            return count;
        }


        /******************************************************************************/
        // メイン

        static void Main(string[] args)
        {
            Array.Copy(array, checkArray, array.Length); // チェック用配列 checkArray に array をコピー
            int ix = 3;                 // チェックを開始するx座標
            int iy = 2;                 // チェックを開始するy座標
            int color = checkArray[iy, ix];  // チェック対象の連の石の色
            int count = 0;              // 関数の結果を入れる用
            DispGoban(checkArray);

            count = GetNumDaMe(ref checkArray, ix, iy, color, count);
            Console.WriteLine($"({ix},{iy})の連の周囲の空点の数は、{count} です。");
            DispGoban(checkArray);
            Console.ReadLine();
        }


        /******************************************************************************/
        // 碁盤の表示

        static void DispGoban(int[,] array)
        {
            var zstr = "   1 2 3 4 5 6 7 8 910111213141516171819 ".Substring(0, boardSize * 2 + 2);
            Console.WriteLine(zstr);
            for (int i = 1; i <= boardSize; i++)
            {
                Console.Write($"{i,2:d}");
                for (int j = 1; j <= boardSize; j++)
                {
                    Console.Write($"{chr[array[i, j]]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
