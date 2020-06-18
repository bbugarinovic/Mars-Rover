using System;

namespace MarsRoverExercise.Tokenizer
{
    [Serializable]
    public class DslParserException : Exception
    {
        public DslParserException(string message)
            : base(message)
        {

        }
    }
}
