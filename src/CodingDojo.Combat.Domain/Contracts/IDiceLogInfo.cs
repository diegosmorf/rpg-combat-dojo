﻿namespace CodingDojo.Combat.Domain.Contracts
{
    public interface IDiceLogInfo
    {
        int ActorValue { get; set; }
        int TargetValue { get; set; }
    }
}