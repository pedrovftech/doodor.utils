using System;

namespace Doodor.Utils.ExceptionHandling
{
    public static class Exceptions
    {
        public static DoodorUtilitiesException DoodorUtilEx(string message) =>
            new DoodorUtilitiesException(message);


        public static DoodorApplicationException DoodorAppEx(string message) =>
            new DoodorApplicationException(message);


        public static ArgumentNullException ArgNullEx(string argName) =>
            new ArgumentNullException(argName);


        public static ArgumentException ArgEx(string argName, string message, Exception innerException) =>
            new ArgumentException(message, argName, innerException);


        public static ArgumentException ArgEx(string argName, string message) =>
            new ArgumentException(message, argName);


        public static string ArgMustBeProvidedMessageEx(string argName) =>
            $"'{argName}' deve ser provido.";


        public static ArgumentException ArgMustBeProvidedEx(string argName) =>
            ArgEx(argName, ArgMustBeProvidedMessageEx(argName));


        public static string ArgCannotBeEmptyMessage(string argName) =>
            $"'{argName}' não pode ser vazio.";


        public static ArgumentException ArgCannotBeEmptyEx(string argName) =>
            new ArgumentException(ArgCannotBeEmptyMessage(argName));
    }
}