﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverExercise.Tokenizer
{
    public class DslToken
    {
        public DslToken(TokenType tokenType)
        {
            TokenType = tokenType;
            Value = string.Empty;
        }

        public DslToken(TokenType tokenType, string value)
        {
            TokenType = tokenType;
            Value = value;
        }

        public TokenType TokenType { get; set; }
        public string Value { get; set; }

        public DslToken Clone()
        {
            return new DslToken(TokenType, Value);
        }
    }
}
