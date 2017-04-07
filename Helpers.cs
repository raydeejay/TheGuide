﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Discord;

namespace TheGuide
{
	public static class Helpers
	{
		public static class Colors
		{
			public static Color Red => new Color(1f, 0f, 0f);
			public static Color Green => new Color(0f, 1f, 0f);
			public static Color Blue => new Color(0f, 0f, 1f);
			public static Color Yellow => new Color(1f, 1f, 0f);
			public static Color SoftRed => new Color((byte)242, 152, 140);
			public static Color SoftGreen => new Color((byte)184, 242, 140);
			public static Color SoftYellow => new Color((byte)242, 235, 140);

			public static Color GetLatencyColor(float latency) =>
				latency >= 500
					? SoftRed
					: latency >= 250
						? SoftYellow
						: SoftGreen;
		}

		public static string Truncate(this string value, int length) =>
			value?.Substring(0, Math.Min(value.Length, value.Length - length));

		public static string PrettyPrint(this IEnumerable<string> list) =>
			string.Join(", ", list.Select(v => $"`{v}`"));

		public static string RemoveWhitespace(this string input) =>
			new string(input.ToCharArray()
				.Where(c => !char.IsWhiteSpace(c))
				.ToArray());

		public static string Cap(this string value, int length) =>
			value?.Substring(0, Math.Abs(Math.Min(value.Length, length)));

		public static bool ICEquals(this string source, string comparison) =>
			string.Equals(source, comparison, StringComparison.OrdinalIgnoreCase);

		public static bool ICStartsWith(this string source, string comparison) =>
			source.StartsWith(comparison, StringComparison.OrdinalIgnoreCase);

		public static string GenFullName(string username, string discriminator) =>
			$"{username}#{discriminator}";

		public static string GenFullName(string username, ulong discriminator) =>
			$"{username}#{discriminator}";

		public static string GenFullName(this IUser user) =>
			GenFullName(user.Username, user.Discriminator);

		public static string GetUptime() =>
			(DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");

		public static string GetHeapSize() =>
			Math.Round(GC.GetTotalMemory(true) / (1024.0d * 1024.0d), 2).ToString(CultureInfo.InvariantCulture);

		private static readonly DateTime UnixEpoch =
			new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static long GetCurrentUnixTimestampMillis() =>
			(long)GetCurrentUnixTimespan().TotalMilliseconds;

		public static DateTime DateTimeFromUnixTimestampMillis(long millis) =>
			UnixEpoch.AddMilliseconds(millis);

		public static long GetCurrentUnixTimestampSeconds() =>
			(long)GetCurrentUnixTimespan().TotalSeconds;

		public static TimeSpan GetCurrentUnixTimespan() =>
			DateTime.UtcNow - UnixEpoch;

		public static DateTime DateTimeFromUnixTimestampSeconds(long seconds) =>
			UnixEpoch.AddSeconds(seconds);
	}
}
