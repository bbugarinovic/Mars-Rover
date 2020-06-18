using System.Collections.Generic;

namespace MarsRoverExercise.Tokenizer
{
    public interface ITokenizer
    {
        IEnumerable<DslToken> Tokenize(string queryDsl);
    }
}
