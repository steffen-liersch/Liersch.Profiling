// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Critical Code Smell", "S1215:\"GC.Collect\" should not be called", Justification = "The function must be called in order to create the same starting condition for each test.", Scope = "member", Target = "~M:Liersch.Profiling.MeasuringTools.PerformGarbageCollection")]
[assembly: SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out")]
