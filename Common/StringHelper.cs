﻿using System.Linq;

namespace Common
{
    public static class StringHelper
    {
        public static bool StrIsNotNullOrWhiteSpace(params string[] stringsArray) =>
            stringsArray.All(stroke => !string.IsNullOrWhiteSpace(stroke));
    }
}
