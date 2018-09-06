﻿using System;
using System.Collections.Generic;
using Manatee.Json.Internal;
using Manatee.Json.Serialization;

namespace Manatee.Json.Schema
{
	public class ConstKeyword : IJsonSchemaKeyword, IEquatable<ConstKeyword>
	{
		public string Name => "const";
		public virtual JsonSchemaVersion SupportedVersions { get; } = JsonSchemaVersion.Draft07 | JsonSchemaVersion.Draft08;
		public int ValidationSequence => 1;

		public JsonValue Value { get; private set; }

		public ConstKeyword() { }
		public ConstKeyword(JsonValue value)
		{
			Value = value;
		}

		public SchemaValidationResults Validate(SchemaValidationContext context)
		{
			return context.Instance == Value
				       ? SchemaValidationResults.Valid
				       : new SchemaValidationResults(Name, SchemaErrorMessages.Const.ResolveTokens(new Dictionary<string, object>
					{
						["expected"] = Value,
						["value"] = context.Instance
					   }));
		}
		public void FromJson(JsonValue json, JsonSerializer serializer)
		{
			Value = json;
		}
		public JsonValue ToJson(JsonSerializer serializer)
		{
			return Value;
		}
		public bool Equals(ConstKeyword other)
		{
			if (other is null) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(Value, other.Value);
		}
		public bool Equals(IJsonSchemaKeyword other)
		{
			return Equals(other as ConstKeyword);
		}
		public override bool Equals(object obj)
		{
			return Equals(obj as ConstKeyword);
		}
		public override int GetHashCode()
		{
			return (Value != null ? Value.GetHashCode() : 0);
		}
	}
}