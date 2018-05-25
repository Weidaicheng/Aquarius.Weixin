namespace Aquarius.Weixin.Core.Authentication
{
    /// <summary>
    /// 验证
    /// </summary>
    public class Verifyer
    {
        /// <summary>
        /// 签名有效性验证
        /// </summary>
        /// <param name="signature">签名</param>
        /// <param name="strings">生成签名的字符串</param>
        /// <returns></returns>
        public bool VerifySignature(string signature, params string[] strings)
        {
            string sign = SignatureGenerater.GenerateSignature(strings);
            return sign == signature;
        }
    }
}
