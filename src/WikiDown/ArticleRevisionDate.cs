﻿using System;
using System.Globalization;

namespace WikiDown
{
    public class ArticleRevisionDate
    {
        public const string FormattedDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public const string IdDateTimeFormat = "yyyyMMdd-HHmmss-fffffff";

        public static ArticleRevisionDate Empty = new ArticleRevisionDate((DateTime?)null);

        private const DateTimeStyles DateTimeStyle = DateTimeStyles.None;

        private static readonly IFormatProvider FormatProvider = CultureInfo.InvariantCulture;

        public ArticleRevisionDate(DateTime? dateTime)
        {
            this.DateTime = dateTime ?? DateTime.MinValue;
        }

        public ArticleRevisionDate(string dateTime)
            : this(TryParse(dateTime))
        {
        }

        public string DateTimeId
        {
            get
            {
                return (this.DateTime > DateTime.MinValue) ? this.DateTime.ToString(IdDateTimeFormat) : null;
            }
        }

        public string DateTimeFormatted
        {
            get
            {
                return (this.DateTime > DateTime.MinValue) ? this.DateTime.ToString(FormattedDateTimeFormat) : null;
            }
        }

        public DateTime DateTime { get; private set; }

        public bool HasValue
        {
            get
            {
                return this.DateTime > DateTime.MinValue;
            }
        }

        public override string ToString()
        {
            return this.DateTimeId ?? string.Empty;
        }

        private static DateTime? TryParse(string dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }

            var idDateTime = TryParseId(dateTime);
            if (idDateTime.HasValue)
            {
                return idDateTime.Value;
            }

            DateTime result;
            return (DateTime.TryParse(dateTime, out result)) ? result : (DateTime?)null;
        }

        private static DateTime? TryParseId(string dateTime)
        {
            DateTime result;
            return (TryParseExact(dateTime, out result)) ? result : (DateTime?)null;
        }

        private static bool TryParseExact(string s, out DateTime result)
        {
            return DateTime.TryParseExact(s, IdDateTimeFormat, FormatProvider, DateTimeStyle, out result);
        }

        public static implicit operator ArticleRevisionDate(DateTime? dateTime)
        {
            return new ArticleRevisionDate(dateTime);
        }

        public static implicit operator ArticleRevisionDate(string dateTime)
        {
            return new ArticleRevisionDate(dateTime);
        }

        public static implicit operator DateTime?(ArticleRevisionDate dateTimeFormatted)
        {
            return (dateTimeFormatted != null && dateTimeFormatted.HasValue)
                       ? dateTimeFormatted.DateTime
                       : (DateTime?)null;
        }
    }
}