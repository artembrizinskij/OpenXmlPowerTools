﻿using System.Collections.Generic;
using System.Linq;

namespace OpenXmlPowerTools
{
    public static class SpreadsheetMLUtil
    {
        public static string GetCellType(string value)
        {
            if (value.Any(c => !char.IsDigit(c) && c != '.'))
            {
                return "str";
            }

            return null;
        }

        public static string IntToColumnId(int i)
        {
            if (i >= 0 && i <= 25)
            {
                return ((char)('A' + i)).ToString();
            }

            if (i >= 26 && i <= 701)
            {
                var v = i - 26;
                var h = v / 26;
                var l = v % 26;
                return ((char)('A' + h)).ToString() + ((char)('A' + l)).ToString();
            }
            // 17576
            if (i >= 702 && i <= 18277)
            {
                var v = i - 702;
                var h = v / 676;
                var r = v % 676;
                var m = r / 26;
                var l = r % 26;
                return ((char)('A' + h)).ToString() +
                    ((char)('A' + m)).ToString() +
                    ((char)('A' + l)).ToString();
            }
            throw new ColumnReferenceOutOfRange(i.ToString());
        }

        private static int CharToInt(char c)
        {
            return c - 'A';
        }

        public static int ColumnIdToInt(string cid)
        {
            if (cid.Length == 1)
            {
                return CharToInt(cid[0]);
            }

            if (cid.Length == 2)
            {
                return CharToInt(cid[0]) * 26 + CharToInt(cid[1]) + 26;
            }
            if (cid.Length == 3)
            {
                return CharToInt(cid[0]) * 676 + CharToInt(cid[1]) * 26 + CharToInt(cid[2]) + 702;
            }
            throw new ColumnReferenceOutOfRange(cid);
        }

        public static IEnumerable<string> ColumnIDs()
        {
            for (var c = (int)'A'; c <= 'Z'; ++c)
            {
                yield return ((char)c).ToString();
            }

            for (var c1 = (int)'A'; c1 <= 'Z'; ++c1)
            {
                for (var c2 = (int)'A'; c2 <= 'Z'; ++c2)
                {
                    yield return ((char)c1).ToString() + ((char)c2).ToString();
                }
            }

            for (var d1 = (int)'A'; d1 <= 'Z'; ++d1)
            {
                for (var d2 = (int)'A'; d2 <= 'Z'; ++d2)
                {
                    for (var d3 = (int)'A'; d3 <= 'Z'; ++d3)
                    {
                        yield return ((char)d1).ToString() + ((char)d2).ToString() + ((char)d3).ToString();
                    }
                }
            }
        }

        public static string ColumnIdOf(string cellReference)
        {
            var columnIdOf = cellReference.Split('0', '1', '2', '3', '4', '5', '6', '7', '8', '9').First();
            return columnIdOf;
        }
    }
}