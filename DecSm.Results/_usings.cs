global using System.Collections.Immutable;
global using System.Diagnostics.CodeAnalysis;
global using System.Reflection;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using DecSm.Results.Abstraction;
global using DecSm.Results.Domain;
global using DecSm.Results.Domain.Errors;
global using DecSm.Results.Implementation.Reasons;
global using DecSm.Results.Implementation.Results;
global using JetBrains.Annotations;

#if NET8_0_OR_GREATER
global using System.Runtime.CompilerServices;

#else
global using System.Runtime.Serialization;
#endif
