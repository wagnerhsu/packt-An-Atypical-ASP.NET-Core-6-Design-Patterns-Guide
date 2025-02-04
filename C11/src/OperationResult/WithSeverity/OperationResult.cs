﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;

namespace OperationResult.WithSeverity
{
    public record class OperationResult
    {
        public OperationResult() { }
        public OperationResult(params OperationResultMessage[] errors)
        {
            Messages = errors.ToImmutableList();
        }

        public bool Succeeded => !HasErrors();
        public int? Value { get; init; }

        public ImmutableList<OperationResultMessage> Messages { get; init; }
        public bool HasErrors()
        {
            return FindErrors().Count() > 0;
        }

        private IEnumerable<OperationResultMessage> FindErrors()
            => Messages?.Where(x => x.Severity == OperationResultSeverity.Error);
    }

    public record class OperationResultMessage
    {
        public OperationResultMessage(string message, OperationResultSeverity severity)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Severity = severity;
        }

        public string Message { get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OperationResultSeverity Severity { get; }
    }

    public enum OperationResultSeverity
    {
        Information = 0,
        Warning = 1,
        Error = 2
    }
}
