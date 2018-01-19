namespace Weixin.Netcore.Core.Authentication
{
    /// <summary>
    /// 验证
    /// </summary>
    public class Verifyer
    {
        #region .ctor
        private readonly SignatureGenerater _generater;

        public Verifyer(SignatureGenerater generater)
        {
            _generater = generater;
        }
        #endregion

        /// <summary>
        /// 签名有效性验证
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="strings">生成签名的字符串</param>
        /// <returns></returns>
        public bool VerifySignature(string signature, params string[] strings)
        {
            string sign = _generater.GenerateSignature(strings);
            return sign == signature;
        }
    }
}
