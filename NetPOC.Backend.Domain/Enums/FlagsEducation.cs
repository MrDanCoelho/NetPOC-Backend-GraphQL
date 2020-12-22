using System;
using System.ComponentModel;

namespace NetPOC.Backend.Domain.Enums
{
    [Flags]
    public enum FlagEducation
    {
        Infantil = 1,
        Fundamental = 2,
        Medio = 3,
        Superior = 4
    }
}