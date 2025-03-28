﻿using System;
using System.Buffers;

namespace Common.Api
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class CustomDisplayStatusAttribute : Attribute
    {
        public string Name { get; }
        public bool Status { get; }

        public CustomDisplayStatusAttribute(string name, bool status = false)
        {
            Name = name;
            Status = status;
        }
    }
    public static class OperationStatusExtensions
    {
        public static string GetCustomDisplayName(this ApiResultStatusCode status)
        {
            var field = status.GetType().GetField(status.ToString());
            var attribute = (CustomDisplayStatusAttribute)Attribute.GetCustomAttribute(field, typeof(CustomDisplayStatusAttribute));
            return attribute?.Name ?? status.ToString();
        }

        public static bool GetCustomStatus(this Enum status)
        {
            var field = status.GetType().GetField(status.ToString());
            var attribute = (CustomDisplayStatusAttribute)Attribute.GetCustomAttribute(field, typeof(CustomDisplayStatusAttribute));
            return attribute?.Status ?? false;
        }
    }
}
