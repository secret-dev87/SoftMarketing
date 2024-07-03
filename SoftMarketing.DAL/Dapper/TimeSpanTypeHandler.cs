using Dapper;
using System;
using System.Data;

namespace SoftMarketing.DAL.Dapper
{
    public class TimeSpanTypeHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override TimeSpan Parse(object value)
        {
            TimeSpan result;

            if (value.GetType() == typeof(short))
            {
                result = new TimeSpan(0, (short)value, 0);
            }
            else
            {
                result = (TimeSpan)value;
            }

            return result;
        }

        public override void SetValue(IDbDataParameter parameter, TimeSpan value)
        {
            parameter.Value = value;
        }
    }
}