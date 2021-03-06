﻿#region License
// Copyright 2013 Andrey Mir
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion License

using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FluentTheory
{
	/// <summary>
	/// Provides helper methods to work with <see cref="T:FluentTheory.TheoryClause`1"/> and commmon .NET framework classes.
	/// </summary>
	public abstract class TheoryClause
	{
		#region Properties
		internal TheoryClause Previous { get; set; }
		#endregion Properties


		#region Interface
		internal abstract bool Evaluate();
		#endregion Interface


		#region Factory methods
		/// <summary>
		/// Creates new inastance of <see cref="T:FluentTheory.TheoryClause`1"/>.
		/// </summary>
		/// <typeparam name="TValue">Related value type.</typeparam>
		/// <param name="value">Related value.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/>.</returns>
		public static TheoryClause<TValue> Create<TValue>(TValue value)
		{
			return new TheoryClause<TValue>(value);
		}
		#endregion Factory methods


		#region Is methods
		/// <summary>
		/// Checks if given triple is a valide date.
		/// </summary>
		/// <param name="day">Day in range [1..31].</param>
		/// <param name="month">Month in range [1..12].</param>
		/// <param name="year">Year.</param>
		/// <returns>True if given triple (day, month year) is a valid date. Otherwise false.</returns>
		public static bool IsDate(int day, int month, int year)
		{
			DateTime? dateTime = null;
			try
			{
				dateTime = new DateTime(year, month, day);
			}
			catch (ArgumentOutOfRangeException)
			{ }

			return dateTime.HasValue;
		}

		/// <summary>
		/// Checks if string is a vaide <see cref="T:System.DateTime"/>.
		/// </summary>
		/// <param name="dateTimeString">String to be checked.</param>
		/// <returns>True, if <paramref name="dateTimeString"/> is a valide <see cref="T:System.DateTime"/>, otherwise false.</returns>
		public static bool IsDateTime(string dateTimeString)
		{
			if (String.IsNullOrEmpty(dateTimeString.Trim()))
			{
				return false;
			}

			DateTime value;
			return DateTime.TryParse(dateTimeString, out value);
		}

		/// <summary>
		/// Checks if string is a vaide <see cref="T:System.DateTime"/>.
		/// </summary>
		/// <param name="dateTimeString">String to be checked.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about <paramref name="dateTimeString"/>.</param>
		/// <param name="dateTimeStyles">A bitwise combination of <see cref="T:System.Globalization.DateTimeStyles"/> enumeration values that indicates the permitted format of <paramref name="dateTimeString"/>.
		/// A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None"/>.</param>
		/// <param name="formats">Array of allowed formats.</param>
		/// <returns>True, if <paramref name="dateTimeString"/> is a valide <see cref="T:System.DateTime"/>, otherwise false.</returns>
		public static bool IsDateTime(string dateTimeString, IFormatProvider formatProvider, DateTimeStyles dateTimeStyles,
			params string[] formats)
		{
			if (String.IsNullOrEmpty(dateTimeString.Trim()))
			{
				return false;
			}

			DateTime value;
			if (formats == null)
			{
				return DateTime.TryParse(dateTimeString, formatProvider, dateTimeStyles, out value);
			}
			return DateTime.TryParseExact(dateTimeString, formats, formatProvider, dateTimeStyles, out value);
		}

		/// <summary>
		/// Checks if gives string is a valide <see cref="T:System.Decimal"/>.
		/// </summary>
		/// <param name="decimalString">String to be checked.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about <paramref name="decimalString"/>.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in <paramref name="decimalString"/>.</param>
		/// <returns>True, if <paramref name="decimalString"/> is a valide <see cref="T:System.Decimal"/>, otherwise false.</returns>
		public static bool IsDecimal(string decimalString, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Number)
		{
			if (String.IsNullOrEmpty(decimalString.Trim()))
			{
				return false;
			}

			decimal value;
			if (formatProvider == null)
			{
				return decimal.TryParse(decimalString, out value);
			}
			return decimal.TryParse(decimalString, numberStyles, formatProvider, out value);
		}

		/// <summary>
		/// Checks if given string is a valide <see cref="T:System.Int32"/>.
		/// </summary>
		/// <param name="intString">String to be checked.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about <paramref name="intString"/>.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in <paramref name="intString"/>.</param>
		/// <returns>True, if <paramref name="intString"/> is a valide <see cref="T:System.Int32"/>, otherwise false.</returns>
		public static bool IsInt(string intString, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Integer)
		{
			if (String.IsNullOrEmpty(intString.Trim()))
			{
				return false;
			}

			int value;
			if (formatProvider == null)
			{
				return int.TryParse(intString, out value);
			}
			return int.TryParse(intString, numberStyles, formatProvider, out value);
		}

		/// <summary>
		/// Checks if given string is a valide <see cref="T:System.Int64"/>.
		/// </summary>
		/// <param name="longString">String to be checked.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about <paramref name="longString"/>.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in <paramref name="longString"/>.</param>
		/// <returns>True, if <paramref name="longString"/> is a valide <see cref="T:System.Int64"/>, otherwise false.</returns>
		public static bool IsLong(string longString, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Integer)
		{
			if (String.IsNullOrEmpty(longString.Trim()))
			{
				return false;
			}

			long value;
			if (formatProvider == null)
			{
				return long.TryParse(longString, out value);
			}
			return long.TryParse(longString, numberStyles, formatProvider, out value);
		}

		/// <summary>
		/// Checks if given string is a valide <see cref="T:System.Single"/>.
		/// </summary>
		/// <param name="floatString">String to be checked.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about <paramref name="floatString"/>.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in <paramref name="floatString"/>.</param>
		/// <returns>True, if <paramref name="floatString"/> is a valide <see cref="T:System.Single"/>, otherwise false.</returns>
		public static bool IsFloat(string floatString, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Float)
		{
			if (String.IsNullOrEmpty(floatString.Trim()))
			{
				return false;
			}

			float value;
			if (formatProvider == null)
			{
				return float.TryParse(floatString, out value);
			}
			return float.TryParse(floatString, numberStyles, formatProvider, out value);
		}

		/// <summary>
		/// Checks if given string is a valide <see cref="T:System.Double"/>.
		/// </summary>
		/// <param name="doubleString">String to be checked.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about <paramref name="doubleString"/>.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in <paramref name="doubleString"/>.</param>
		/// <returns>True, if <paramref name="doubleString"/> is a valide <see cref="T:System.Double"/>, otherwise false.</returns>
		public static bool IsDouble(string doubleString, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Float | NumberStyles.AllowThousands)
		{
			if (String.IsNullOrEmpty(doubleString.Trim()))
			{
				return false;
			}

			double value;
			if (formatProvider == null)
			{
				return double.TryParse(doubleString, out value);
			}
			return double.TryParse(doubleString, numberStyles, formatProvider, out value);
		}

		/// <summary>
		/// Checks if given string is a valide <see cref="T:System.Boolean"/>.
		/// </summary>
		/// <param name="boolString">String to be checked.</param>
		/// <returns>True, if <paramref name="boolString"/> is a valide <see cref="T:System.Boolean"/>, otherwise false.</returns>
		public static bool IsBool(string boolString)
		{
			if (String.IsNullOrEmpty(boolString.Trim()))
			{
				return false;
			}

			bool value;
			return bool.TryParse(boolString, out value);
		}

		/// <summary>
		/// Checks if gives string is a valide email defined by regex: http://msdn.microsoft.com/en-us/library/01escwtf.aspx.
		/// </summary>
		/// <param name="emailString">String to be checked.</param>
		/// <returns>True, if <paramref name="emailString"/> is a valide email address.</returns>
		public static bool IsEmail(string emailString)
		{
			if (String.IsNullOrEmpty(emailString.Trim()))
			{
				return false;
			}

			//Regex is taken from here: http://msdn.microsoft.com/en-us/library/01escwtf.aspx
			bool ret = Regex.IsMatch(emailString,
				@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
				RegexOptions.IgnoreCase);

			return ret;
		}
		#endregion Is methods
	}

	/// <summary>
	/// Builds up an expression regarding given value and it is used a part of <see cref="T:FluentTheory.Hypothesis"/>.
	/// </summary>
	/// <typeparam name="TValue">Type of related value.</typeparam>
	public class TheoryClause<TValue> : TheoryClause
	{
		#region Properties
		private Func<TValue, bool> ClauseExpression { get; set; }

		/// <summary>
		/// Related value.
		/// </summary>
		private Func<TValue> _valueExpression;
		public TValue Value
		{
			get
			{
				return _valueExpression == null ? default(TValue) : _valueExpression();
			}
			set { _valueExpression = () => value; }
		}

		/// <summary>
		/// Is clause already evaluated?
		/// </summary>
		public bool IsEvaluated { get; set; }
		#endregion Properties


		#region Constructors
		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="value">Related value.</param>
		/// <param name="clauseExpression">Clause expression to be evaluated.</param>
		public TheoryClause(TValue value, Func<TValue, bool> clauseExpression = null)
		{
			Value = value;
			ClauseExpression = clauseExpression;
		}

		/// <summary>
		/// Creates new instance.
		/// </summary>
		/// <param name="valueExpression">Expression returns related value.</param>
		/// <param name="clauseExpression">Clause expression to be evaluated.</param>
		public TheoryClause(Func<TValue> valueExpression, Func<TValue, bool> clauseExpression = null)
		{
			_valueExpression = valueExpression;
			ClauseExpression = clauseExpression;
		}
		#endregion Constructors


		#region Interface
		internal override bool Evaluate()
		{
			if (IsEvaluated)
			{
				throw new EvaluateException("Clause was already evaluated.");
			}

			IsEvaluated = true;

			if (ClauseExpression != null)
			{
				bool ret = ClauseExpression(Value);

				//We have only conjunctions therefore evaluation order is irrelevant.
				if (Previous != null)
				{
					ret = ret && Previous.Evaluate();
				}
				return ret;
			}

			return true;
		}

		/// <summary>
		/// Creates new <see cref="T:FluentTheory.TheoryClause`1"/> to convert related value from type TValue into TAsValue.
		/// </summary>
		/// <typeparam name="TValue">Related value type before conversion.</typeparam>
		/// <typeparam name="TAsValue">Converted value type.</typeparam>
		/// <param name="valueFunc">Expression to convert value.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/>.</returns>
		public TheoryClause<TAsValue> As<TAsValue>(Func<TValue, TAsValue> valueFunc)
		{
			var ret = new TheoryClause<TAsValue>(valueFunc(Value));
			Previous = ret;
			return ret;
		}
		
		/// <summary>
		/// Creates new <see cref="T:FluentTheory.TheoryClause`1"/> for given expression.
		/// </summary>
		/// <typeparam name="TValue">Related value type.</typeparam>
		/// <param name="clauseExpression">Expression for new clause.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/>.</returns>
		public TheoryClause<TValue> Is(Func<TValue, bool> clauseExpression)
		{
			var ret = new TheoryClause<TValue>(Value) { ClauseExpression = clauseExpression };
			Previous = ret;
			return ret;
		}
		#endregion Interface
	}
}