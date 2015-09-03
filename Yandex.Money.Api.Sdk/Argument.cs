using System;

namespace Yandex.Money.Api
{
	/// <summary>
	/// Contains various necessary argument checks (Design-by-Contract).
	/// </summary>
	public static class Argument
	{
		/// <summary>
		/// Ensures that argument is not null.
		/// </summary>
		/// <param name="value">Pass argument you need to check.</param>
		/// <param name="message">Error message for the exception.</param>
		/// <exception cref="ArgumentNullException"><paramref name="value" /> is <c>null</c>.</exception>
		[AssertionMethod]
		public static void NotNull(
			[AssertionCondition(AssertionConditionType.IS_NOT_NULL)] object value,
			[NotNull] string message
			)
		{
			if (null == value)
			{
				throw new ArgumentNullException(message);
			}
		}

		/// <summary>
		/// Ensures that string argument is not null and is not empty.
		/// </summary>
		/// <param name="value">Pass argument you need to check.</param>
		/// <param name="message">Error message for the exception.</param>
		/// <exception cref="ArgumentException"><paramref name="value" /> is <c>null</c>.</exception>
		[AssertionMethod]
		public static void NotNullOrEmpty(
			[AssertionCondition(AssertionConditionType.IS_NOT_NULL)]  string value,
			[NotNull] string message
			)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(message);
			}
		}

		/// <summary>
		/// Perform custom check on the argument.
		/// </summary>
		/// <param name="condition">Condition to check.</param>
		/// <param name="errorMessage">Error message for the exception.</param>
		[AssertionMethod]
		public static void Require(
			[AssertionCondition(AssertionConditionType.IS_TRUE)] bool condition,
			[NotNull] string errorMessage)
		{
			if (!condition)
			{
				throw new ArgumentException(errorMessage);
			}
		}
	}
}