using Exiled.API.Interfaces;

namespace AntiCheat_Free_EdrenBaton
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}