namespace Weixin.Netcore.Core.DebugSetting
{
    public class DebugMode : IDebugMode
    {
        private readonly bool _isDebug = false;

        public DebugMode(bool isDebug)
        {
            _isDebug = isDebug;
        }

        public bool IsDebug
        {
            get
            {
                return _isDebug;
            }
        }
    }
}
