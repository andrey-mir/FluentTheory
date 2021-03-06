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
using System.Collections.Generic;

namespace FluentTheory
{
	/// <summary>
	/// Theory we want to evaluate.
	/// </summary>
	public class Theory
	{
		#region Properties
		private readonly List<Hypothesis> _hypotheses;

		private readonly Dictionary<string, Hypothesis> _namedHypotheses;

		/// <summary>
		/// Returns hypothesis by name.
		/// </summary>
		/// <param name="hypothesisName">hypothesis name</param>
		/// <returns>Returns hypothesis if hypothesis under the given name is found, null otherwise.</returns>
		public Hypothesis this[string hypothesisName]
		{
			get
			{
				Hypothesis hypothesis;
				_namedHypotheses.TryGetValue(hypothesisName, out hypothesis);
				return hypothesis;
			}
		}
		#endregion Properties


		#region Constructors
		/// <summary>
		/// Creates new instance.
		/// </summary>
		public Theory()
		{
			_hypotheses = new List<Hypothesis>();
			_namedHypotheses = new Dictionary<string, Hypothesis>();
		}
		#endregion Constructors


		#region Interface
		/// <summary>
		/// Evaluates theory.
		/// </summary>
		/// <returns>True if theory is proved by evaluation, otherwise false.</returns>
		public bool Evaluate()
		{
			bool result = true;
			foreach (Hypothesis hypothesis in _hypotheses)
			{
				result = (hypothesis.Evaluate() != false) && result;
			}
			return result;
		}

		/// <summary>
		/// Adds new hypothesis into theory.
		/// </summary>
		/// <param name="hypothesisName">Name for new hypothesis.</param>
		/// <returns>New hypothesis.</returns>
		public Hypothesis Hypothesis(string hypothesisName = null)
		{
			var hypothesis = new Hypothesis(this);
			hypothesis.Name = hypothesisName;
			_hypotheses.Add(hypothesis);

			if (!String.IsNullOrEmpty(hypothesisName))
			{
				if (_namedHypotheses.ContainsKey(hypothesisName))
				{
					throw new ArgumentException("Failed to add hypothesis: hypothesis with hypothesisName:" + hypothesisName + " already exists.");
				}
				_namedHypotheses.Add(hypothesisName, hypothesis);
			}

			return hypothesis;
		}

		/// <summary>
		/// Adds new hypothesis into theory for given supposition.
		/// </summary>
		/// <param name="supposition">Supposition for new hypothesis.</param>
		/// <returns>New hypothesis.</returns>
		public Hypothesis Suppose(Func<bool> supposition)
		{
			return Suppose(null, supposition);
		}

		/// <summary>
		/// Adds new named hypothesis into theory for given supposition.
		/// </summary>
		/// <param name="hypothesisName">Name for new hypothesis.</param>
		/// <param name="supposition">Supposition for new hypothesis</param>
		/// <returns>New hypothesis</returns>
		public Hypothesis Suppose(string hypothesisName, Func<bool> supposition)
		{
			return Hypothesis(hypothesisName).Suppose(supposition);
		}

		/// <summary>
		/// Adds new hypothesis for given supposition based on <see cref="T:FluentTheory.TheoryClause`1>" />.
		/// </summary>
		/// <typeparam name="TValue">Theory clause type.</typeparam>
		/// <param name="supposition">Supposition for new hypothesis.</param>
		/// <returns>New hypothesis.</returns>
		public Hypothesis Suppose<TValue>(Func<TheoryClause<TValue>> supposition)
		{
			return Suppose(null, supposition);
		}

		/// <summary>
		/// Adds new named hypothesis for given supposition based on <see cref="T:FluentTheory.TheoryClause`1>" />.
		/// </summary>
		/// <typeparam name="TValue">Theory clause type.</typeparam>
		/// <param name="hypothesisName">Name for new hypothesis.</param>
		/// <param name="supposition">Supposition for new hypothesis.</param>
		/// <returns>New hypothesis.</returns>
		/// <returns>New hypothesis.</returns>
		public Hypothesis Suppose<TValue>(string hypothesisName, Func<TheoryClause<TValue>> supposition)
		{
			return Hypothesis(hypothesisName).Suppose(supposition);
		}
		#endregion Interface
	}
}