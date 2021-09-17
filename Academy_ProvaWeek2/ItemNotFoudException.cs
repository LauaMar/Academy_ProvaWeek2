using System;
using System.Runtime.Serialization;

namespace Academy_ProvaWeek2
{
    [Serializable]
    internal class ItemNotFoudException : Exception
    {
        public string IdNotFound { get; set; }
        public ItemNotFoudException()
        {
        }

        public ItemNotFoudException(string message) : base(message)
        {
        }

        public ItemNotFoudException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ItemNotFoudException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}