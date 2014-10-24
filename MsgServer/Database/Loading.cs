// * Created by Jean-Philippe Boivin
// * Copyright © 2010
// * Logik. Project

using System;

namespace COServer
{
    public partial class Database
    {
        private class Loading
        {
            private static Int32 Next = -1;
            private static String[] Array = new String[] { "|", "/", "-", "\\" };

            public static String NextChar()
            {
                Next++;
                if (Next > 3)
                    Next = 0;
                return Array[Next];
            }
        }
    }
}
