﻿using Exiled.API.Interfaces;

namespace AntiCheatFree
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;
    }
}