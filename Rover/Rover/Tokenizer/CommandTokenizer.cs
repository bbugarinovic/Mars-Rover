using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MarsRoverExercise.Tokenizer
{
    public enum TokenType
    {
        Plateau,
        Landing,
        Instructions,
        RoverName,
        StringValue,
        Number,
        SequenceTerminator,
        Invalid
    }

    public class CommandTokenizer : ITokenizer
    {
        private List<TokenDefinition> _tokenDefinitions;

        public CommandTokenizer()
        {
            _tokenDefinitions = new List<TokenDefinition>();

            _tokenDefinitions.Add(new TokenDefinition(TokenType.Plateau, "^Plateau:"));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Landing, "^Landing:"));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Instructions, "^Instructions:"));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.RoverName, "^Rover\\d+"));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.StringValue, "^[a-zA-Z]*$"));
            _tokenDefinitions.Add(new TokenDefinition(TokenType.Number, "^\\d+"));
        }

        public IEnumerable<DslToken> Tokenize(string lqlText)
        {
            var tokens = new List<DslToken>();

            string remainingText = lqlText;

            while (!string.IsNullOrWhiteSpace(remainingText))
            {
                var match = FindMatch(remainingText);
                if (match.IsMatch)
                {
                    tokens.Add(new DslToken(match.TokenType, match.Value));
                    remainingText = match.RemainingText;
                }
                else
                {
                    if (IsWhitespace(remainingText))
                    {
                        remainingText = remainingText.Substring(1);
                    }
                    else
                    {
                        var invalidTokenMatch = CreateInvalidTokenMatch(remainingText);
                        tokens.Add(new DslToken(invalidTokenMatch.TokenType, invalidTokenMatch.Value));
                        remainingText = invalidTokenMatch.RemainingText;
                    }
                }
            }

            tokens.Add(new DslToken(TokenType.SequenceTerminator, string.Empty));

            return tokens;
        }

        private TokenMatch FindMatch(string lqlText)
        {
            foreach (var tokenDefinition in _tokenDefinitions)
            {
                var match = tokenDefinition.Match(lqlText);
                if (match.IsMatch)
                    return match;
            }

            return new TokenMatch() { IsMatch = false };
        }

        private bool IsWhitespace(string lqlText)
        {
            return Regex.IsMatch(lqlText, "^\\s+");
        }

        private TokenMatch CreateInvalidTokenMatch(string lqlText)
        {
            var match = Regex.Match(lqlText, "(^\\S+\\s)|^\\S+");
            if (match.Success)
            {
                return new TokenMatch()
                {
                    IsMatch = true,
                    RemainingText = lqlText.Substring(match.Length),
                    TokenType = TokenType.Invalid,
                    Value = match.Value.Trim()
                };
            }

            throw new DslParserException("Failed to generate invalid token");
        }
    }
}
