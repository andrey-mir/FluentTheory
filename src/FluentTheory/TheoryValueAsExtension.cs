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
using System.Globalization;

namespace FluentTheory
{
	/// <summary>
	/// Extensions to convert theory clauses.
	/// </summary>
	public static class TheoryValueAsExtension
	{
		/// <summary>
		/// Creates <see cref="T:FluentTheory.TheoryClause`1"/> to convert previous clause value from type <see cref="T:System.String"/> into <see cref="T:System.DateTime"/>.
		/// </summary>
		/// <param name="clauseToConvert">Clause to convert.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/>.</returns>
		public static TheoryClause<DateTime> AsDateTime(this TheoryClause<string> clauseToConvert)
		{
			return new TheoryClause<DateTime>(() => DateTime.Parse(clauseToConvert.Value)) { Previous = clauseToConvert };
		}

		/// <summary>
		/// Creates <see cref="T:FluentTheory.TheoryClause`1"/> to convert previous clause value from type <see cref="T:System.String"/> into <see cref="T:System.DateTime"/>.
		/// </summary>
		/// <param name="clauseToConvert">Clause that be converted.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about string value of converted clause.</param>
		/// <param name="dateTimeStyles">A bitwise combination of <see cref="T:System.Globalization.DateTimeStyles"/> enumeration values that indicates the permitted format for value of converted clause.
		/// A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None"/>.</param>
		/// <param name="formats">Array of allowed formats.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/></returns>
		public static TheoryClause<DateTime> AsDateTime(this TheoryClause<string> clauseToConvert, IFormatProvider formatProvider, DateTimeStyles dateTimeStyles = DateTimeStyles.None, params string[] formats)
		{
			Func<DateTime> valueExpression = () =>
			{
				DateTime value;
				if (formats == null)
				{
					if (DateTime.TryParse(clauseToConvert.Value, formatProvider, dateTimeStyles, out value))
					{
						throw new FormatException("Cannot parse string " + clauseToConvert.Value + " to DateTime");
					}
				}
				if (!DateTime.TryParseExact(clauseToConvert.Value, formats, formatProvider, dateTimeStyles, out value))
				{
					throw new FormatException("Cannot parse string " + clauseToConvert.Value + " to DateTime");
				}
				return value;
			};

			return new TheoryClause<DateTime>(valueExpression) { Previous = clauseToConvert };
		}

		/// <summary>
		/// Creates <see cref="T:FluentTheory.TheoryClause`1"/> to convert previous clause value from type <see cref="T:System.String"/> into <see cref="T:System.Decimal"/>.
		/// </summary>
		/// <param name="clauseToConvert">Clause that be converted.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about string value of converted clause.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in the value of converted clause.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/></returns>
		public static TheoryClause<decimal> AsDecimal(this TheoryClause<string> clauseToConvert, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Number)
		{
			Func<decimal> valueExpression;
			if (formatProvider == null)
			{
				valueExpression = () => decimal.Parse(clauseToConvert.Value, numberStyles);
			}
			else
			{
				valueExpression = () => decimal.Parse(clauseToConvert.Value, numberStyles, formatProvider);
			}

			return new TheoryClause<decimal>(valueExpression) { Previous = clauseToConvert };
		}

		/// <summary>
		/// Creates <see cref="T:FluentTheory.TheoryClause`1"/> to convert previous clause value from type <see cref="T:System.String"/> into <see cref="T:System.Int32"/>.
		/// </summary>
		/// <param name="clauseToConvert">Clause that be converted.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about string value of converted clause.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in the value of converted clause.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/></returns>
		public static TheoryClause<int> AsInt(this TheoryClause<string> clauseToConvert, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Integer)
		{
			if (formatProvider == null)
			{
				return new TheoryClause<int>(() => int.Parse(clauseToConvert.Value, numberStyles)) { Previous = clauseToConvert };
			}
			return new TheoryClause<int>(() => int.Parse(clauseToConvert.Value, numberStyles, formatProvider)) { Previous = clauseToConvert };
		}

		/// <summary>
		/// Creates <see cref="T:FluentTheory.TheoryClause`1"/> to convert previous clause value from type <see cref="T:System.String"/> into <see cref="T:System.Int64"/>.
		/// </summary>
		/// <param name="clauseToConvert">Clause that be converted.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about string value of converted clause.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in the value of converted clause.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/></returns>
		public static TheoryClause<long> AsLong(this TheoryClause<string> clauseToConvert, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Integer)
		{
			if (formatProvider == null)
			{
				return new TheoryClause<long>(() => long.Parse(clauseToConvert.Value, numberStyles)) { Previous = clauseToConvert };
			}
			return new TheoryClause<long>(() => long.Parse(clauseToConvert.Value, numberStyles, formatProvider)) { Previous = clauseToConvert };
		}

		/// <summary>
		/// Creates <see cref="T:FluentTheory.TheoryClause`1"/> to convert previous clause value from type <see cref="T:System.String"/> into <see cref="T:System.Single"/>.
		/// </summary>
		/// <param name="clauseToConvert">Clause that be converted.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about string value of converted clause.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in the value of converted clause.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/></returns>
		public static TheoryClause<float> AsFloat(this TheoryClause<string> clauseToConvert, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Float)
		{
			if (formatProvider == null)
			{
				return new TheoryClause<float>(() => float.Parse(clauseToConvert.Value, numberStyles)) { Previous = clauseToConvert };
			}
			return new TheoryClause<float>(() => float.Parse(clauseToConvert.Value, numberStyles, formatProvider)) { Previous = clauseToConvert };
		}

		/// <summary>
		/// Creates <see cref="T:FluentTheory.TheoryClause`1"/> to convert previous clause value from type <see cref="T:System.String"/> into <see cref="T:System.Double"/>.
		/// </summary>
		/// <param name="clauseToConvert">Clause that be converted.</param>
		/// <param name="formatProvider">An object that supplies culture-specific format information about string value of converted clause.</param>
		/// <param name="numberStyles">A bitwise combination of <see cref="T:System.Globalization.NumberStyles"/> values that indicates the style elements that can be present in the value of converted clause.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/></returns>
		public static TheoryClause<double> AsDouble(this TheoryClause<string> clauseToConvert, IFormatProvider formatProvider = null, NumberStyles numberStyles = NumberStyles.Float | NumberStyles.AllowThousands)
		{
			if (formatProvider == null)
			{
				return new TheoryClause<double>(() => double.Parse(clauseToConvert.Value, numberStyles)) { Previous = clauseToConvert };
			}
			return new TheoryClause<double>(() => double.Parse(clauseToConvert.Value, numberStyles, formatProvider)) { Previous = clauseToConvert };
		}

		/// <summary>
		/// Creates <see cref="T:FluentTheory.TheoryClause`1"/> to convert previous clause value from type <see cref="T:System.String"/> into <see cref="T:System.Boolean"/>.
		/// </summary>
		/// <param name="clauseToConvert">Clause that be converted.</param>
		/// <returns>New instance of <see cref="T:FluentTheory.TheoryClause`1"/></returns>
		public static TheoryClause<bool> AsBool(this TheoryClause<string> clauseToConvert)
		{
			return new TheoryClause<bool>(() => bool.Parse(clauseToConvert.Value)) { Previous = clauseToConvert };
		}
	}
}