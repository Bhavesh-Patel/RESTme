﻿namespace OElite
{
    using System;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using System.IO;
    using System.Text;
    using System.Linq;

    /// <summary>
    /// Contains extension method for the string class, used with the route parser of the
    /// <see cref="ComplexRoute"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// A regular expression used to manipulate parameterized route segments.
        /// </summary>
        /// <value>A <see cref="Regex"/> object.</value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Regex ParameterExpression =
            new Regex(@"\{(?<name>[A-Z0-9]*)\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Extracts the name of a parameter from a segment.
        /// </summary>
        /// <param name="segment">The segment to extract the name from.</param>
        /// <returns>A string containing the name of the parameter.</returns>
        /// <exception cref="FormatException"></exception>
        public static string GetParameterName(this string segment)
        {
            Match nameMatch =
                ParameterExpression.Match(segment);

            if (nameMatch.Success)
            {
                return nameMatch.Groups["name"].Value;
            }

            throw new FormatException("The specified segment does not contain any valid parameters.");
        }
        public static bool IsASCII(this string s)
        {
            return s.All(c => c < 127);
        }
        public static bool IsNullOrEmpty(this string segment)
        {
            return string.IsNullOrEmpty(segment);
        }
        public static bool IsNotNullOrEmpty(this string segment)
        {
            return !string.IsNullOrEmpty(segment);
        }
        public static string MD5Encrypt(this string value)
        {
            return EncryptHelper.MD5Encrypt(value);
        }
        /// <summary>
        /// Checks if a segement contains any parameters.
        /// </summary>
        /// <param name="segment">The segment to check for parameters.</param>
        /// <returns>true if the segment contains a parameter; otherwise false.</returns>
        /// <remarks>A parameter is defined as a string which is surrounded by a pair of curly brackets.</remarks>
        /// <exception cref="ArgumentException">The provided value for the segment parameter was null or empty.</exception>
        public static bool IsParameterized(this string segment)
        {
            Match parameterMatch =
                ParameterExpression.Match(segment);

            return parameterMatch.Success;
        }
        public static string RemoveSpecialCharactersAndWhiteSpace(this string segment, bool removeDot = true, bool removeUnderScore = true, bool removeNumbers = false)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in segment)
            {
                if ((!removeNumbers && c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (!removeDot && c == '.') || (!removeUnderScore && c == '_'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        public static T JsonDeserialize<T>(this string value)
        {
            return StringUtils.JsonDeserialize<T>(value);
        }


    }
}